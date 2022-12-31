namespace CoursePractise.Models.ViewModels
{
    public class DetailsViewModel
    {

        public DetailsViewModel()
        {
            Product = new Product();
        }
        public Product Product { get; set; }

        public bool ExistInCart { get; set; }
    }
}
