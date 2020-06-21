using System.ComponentModel.DataAnnotations;

namespace bigSchool.Models
{
    public class Category
    {
        public byte Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
    }
}