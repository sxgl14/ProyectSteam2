using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectSteam2.Models;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace ProyectSteam2.Controllers
{
    // Controlador para registro e ingreso de cuenta
    public class LoginController : Controller
    {
        #region ConexiónDB
        private readonly SteamContext _context;

        public LoginController(SteamContext context)
        {
            _context = context;
        }
        #endregion ConexiónDB


        #region Start(Controlador del login)

        [HttpGet]
        public IActionResult Start()

        // Condicional de si el usuario está autenticado redirecciona al index
        {
            if (User.Identity!.IsAuthenticated) return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Start(LoginVM modelo)
        {

            //Query para verificar si el nombre de usuario se encuentra en la base de datos
            User? userf = await _context.Users
                            .Where(e => 
                            e.NameU == modelo.NameU || e.Mail == modelo.Mail && 
                            e.Pass == modelo.Pass)
                            .FirstOrDefaultAsync();

            if(userf == null)
            {
                ViewData["Mensaje"] = "El usuario o contraseña son incorrectos";
                return View();
            }

            //Autenticación de usuario
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userf.NameU),
                new Claim(ClaimTypes.Email, userf.Mail),
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
            );

            return RedirectToAction("Index", "Main");
        }

        #endregion Start


        #region Create(Controlador del registro)

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserVM modelo)
        {
            //Query para verificar si el usuario o el correo ya existe en la base de datos 
            User? userc = await _context.Users
                            .Where(e => 
                            e.NameU == modelo.NameU || e.Mail == modelo.Mail)
                            .FirstOrDefaultAsync();

            if(userc != null)
            {
                ViewData["Mensaje"] = "El usuario o correo ya se encuentra en nuestra base de datos";
                return View();
            }
            if(modelo.Pass != modelo.ConfirmPass)
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View();
            }
            User user = new()
            {
                NameU = modelo.NameU,
                Mail = modelo.Mail,
                Pass = modelo.Pass
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            if(user.NameU != null) return RedirectToAction("Start", "Login");

            ViewData["Mensaje"] = "Error, no se pudo crear la cuenta";
            return View();
        }

        #endregion Create

    }
}