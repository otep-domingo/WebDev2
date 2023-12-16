using System.ComponentModel.DataAnnotations;

namespace WebDev2.Models
{
    
    public class Account
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
