using ProyectSteam2.Models.DTOs;

namespace ProyectSteam2.Models
{
    public class GamCatVM
    {
        public IEnumerable<GamCatDTO> cons_gam { get; set; }
        public List<Category> categs { get; set; }
    }
}