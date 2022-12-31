using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoursePractise.Models.ViewModels
{
    public class ProductViewModel
    {

        public Product Product { get; set; }

        public IEnumerable<SelectListItem> CategorySelectList { get; set; }

        public IEnumerable<SelectListItem> CustomPageSelectList { get; set; }

        
    }
}
