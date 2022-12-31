namespace CoursePractise.Models.ViewModels
{
    public class CartUserInqueryViewModel
    {
        public CartUserInqueryViewModel()
        {
            ProductList = new List<Product>();
        }

        public ApplicationUser ApplicationUser { get; set; }

        public IList<Product> ProductList { get; set; }
    }
}
