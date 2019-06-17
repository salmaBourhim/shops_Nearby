using System.ComponentModel.DataAnnotations;

namespace projectTest.ViewModels
{
    public class ShopOutputModel
    {
        [Key]
        public int ShopId { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double Distance { get; set; }
    }
}