

namespace ProyectSteam2.Models.DTOs
{
    public class GamCatDTO
    {
        public string? NameG { get; set; }
        public decimal? Price { get; set; }
        public string? Creator { get; set; }
        public string? ImgGame { get; set; }
        public string? Descript { get; set; }
        public List<string>? NameCat { get; set; }
    }
}