using System.ComponentModel.DataAnnotations;

namespace VetAppDal
{
    public class Animal
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Species { get; set; }
        public string? Breed { get; set; }
        public DateTime BirthDate  { get; set; }
        public string? Color { get; set; }
        public double Size { get; set; }
        public double Weight { get; set; }
        public DateTime FirstVisit { get; set; }
        public DateTime LastVisit { get; set; }
        [Required]
        public string Owner { get; set; }
        [Required]
        public string? Contact { get; set; }
    }
}