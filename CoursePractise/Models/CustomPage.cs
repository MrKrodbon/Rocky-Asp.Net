using System.ComponentModel.DataAnnotations;

namespace CoursePractise.Models
{
    public class CustomPage
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ? CustomName { get; set; }



    }
}
