using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectSteam2.Models;
using ProyectSteam2.Models.DTOs;


namespace ProyectSteam2.Controllers
{
    [Authorize]
    public class MainController : Controller
    {
        #region ConexiónDB
        private readonly SteamContext _context;

        public MainController(SteamContext context)
        {
            _context = context;
        }
        #endregion ConexiónDB


        #region Index pagina Steam

        [HttpGet]
        public async Task<IActionResult> Index(string search)
        {

            //Asigna una variable con el nombre y con ese nombre se busca la ID del usuario
            var username = User.Claims.Where(x => x.Type == ClaimTypes.Name).Select(x => x.Value).SingleOrDefault();

            var idUs = await _context.Users.Where(g => g.NameU == username).FirstOrDefaultAsync();

            //Busqueda de los datos del juego con la tabla muchos a muchos CategGames
            var cons_gam = (from info in _context.Games.Where(g => !_context.GamesUsers.Any(x => x.IdU == idUs.IdU && x.IdG == g.IdG))
                            join info2 in _context.CategGames on info.IdG equals info2.IdGame
                            join info3 in _context.Categorys on info2.IdCateg equals info3.IdCat
                            select new { info.NameG, info.Price, info.Creator, info.ImgGame, info.Descript, info3.NameCat })
                            .GroupBy(x => x.NameG)
                            .Select(g => new GamCatDTO
                                {
                                    NameG = g.Key,
                                    Price = g.FirstOrDefault().Price,
                                    Creator = g.FirstOrDefault().Creator,
                                    ImgGame = g.FirstOrDefault().ImgGame,
                                    Descript = g.FirstOrDefault().Descript,
                                    NameCat = g.Select(x => x.NameCat).ToList()
                                }).AsEnumerable();
            
            //Busqueda de todas las categorias que hay
            var categs = _context.Categorys.ToList();


            //Barra de busqueda tanto para el nombre del juego como la categoria(La primera letra se coloca en mayuscula para evitar problemas)
            if (!String.IsNullOrEmpty(search))
            {
                cons_gam = cons_gam.Where(c => c.NameG.Contains(search) || c.NameCat.Any(d => d.Contains(search)));
            }

            //Se guardan las variables en el ViewModel
            var VM = new GamCatVM
            {
                cons_gam = cons_gam,
                categs = categs
            };

            return View(VM);
        }

        #endregion
    

        #region Compra juego

        [HttpPost]
        public async Task<IActionResult> Buy(string NameGame, string idUser)
        {
            // Busca la ID del juego segun el nombre dado en el cshtml
            Game? game = await _context.Games.Where(g => 
                            g.NameG == NameGame)
                            .FirstOrDefaultAsync();

            //busca la ID del usuario segun la encontrada con la autenticación del cshtml
            User? user = await _context.Users.Where(g => 
                            g.NameU == idUser)
                            .FirstOrDefaultAsync();

            if(_context.GamesUsers.Any(g => g.IdU == user.IdU && g.IdG == game.IdG))
            {
                // Si ya existe un registro, muestra un mensaje de error
                ViewData["MensajeError"] = "Ya realizó la compra de este juego";
            }
            else
            {
                // Si no existe un registro, agrega uno nuevo
                GamesUser? gameUser = new()
                { 
                    IdU = user.IdU,
                    IdG = game.IdG
                };
                await _context.GamesUsers.AddAsync(gameUser);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        #endregion


        #region Mostrar juegos comprados

        public IActionResult Games()
        {
            var gameuser = (from info in _context.Games
                            join info2 in _context.GamesUsers on info.IdG equals info2.IdG
                            join info3 in _context.Users on info2.IdU equals info3.IdU
                            select new{info.NameG, info.Creator, info.ImgGame, info.DateR}).GroupBy(x => x.NameG)
                            .Select(g => new GamUseDTO
                            {
                                NameG = g.Key,
                                Creator = g.FirstOrDefault().Creator,
                                ImgGame = g.FirstOrDefault().ImgGame,
                                DateR = g.FirstOrDefault().DateR
                            }).AsEnumerable();

            return View(gameuser);
        }

        #endregion
    }
}