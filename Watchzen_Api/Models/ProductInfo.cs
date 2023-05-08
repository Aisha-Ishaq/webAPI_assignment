using System.ComponentModel.DataAnnotations;

namespace Watchzen_Api.Models
{
    public class ProductInfo
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
    }
}
