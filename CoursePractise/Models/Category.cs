using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CoursePractise.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [DisplayName("Category Name")]
        [Required]
        public string CategoryName { get; set; }
        [Required]

        [Range(1, int.MaxValue, ErrorMessage = "Display order must be greater than 0")]
        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }
    }
}
