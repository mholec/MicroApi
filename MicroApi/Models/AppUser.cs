using System.ComponentModel.DataAnnotations;

namespace MicroApi.Models
{
    public class AppUser
    {
        [Required]
        public string Name { get; set; }
    }
}