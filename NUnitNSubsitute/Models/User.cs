using System.ComponentModel.DataAnnotations;

namespace Practise.Models
{
    public class User
    {
        [Required]
        [Range(1,Int32.MaxValue)]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }
    }
}
