using System.ComponentModel.DataAnnotations;

namespace WebDev2.Models
{
    public class Product
    {
        [Key]
        public int? idproducts { get; set; }
        public string? productname { get; set; }
        public string? category { get; set; }
        public double? price { get; set; }
        public DateTime? datetimeadded { get; set; }
        public string? description { get; set; }
    }
}
