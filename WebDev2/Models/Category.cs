using System.ComponentModel.DataAnnotations;

namespace WebDev2.Models
{
    public class Category
    {
        [Key]
        public int idcategories { get; set; }
        public string category { get; set; } 
    }
}
