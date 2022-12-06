using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoursePractise.Models.ViewModels
{
    public class ProductVM
    {

        public Product Product { get; set; }

        public IEnumerable<SelectListItem> CategorySelectList { get; set; }

        public IEnumerable<SelectListItem> CustomPageSelectList { get; set; }

        
    }
}
