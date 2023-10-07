using System.ComponentModel.DataAnnotations;

namespace WebDev2.Models
{
    public class User
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserEmail { get; set; }
    }
}
