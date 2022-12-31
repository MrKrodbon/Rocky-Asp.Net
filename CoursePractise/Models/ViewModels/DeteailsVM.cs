namespace CoursePractise.Models.ViewModels
{
    public class DeteilsVM
    {

        public DeteilsVM()
        {
            Product = new Product();
        }
        public Product Product { get; set; }

        public bool ExistInCart { get; set; }
    }
}
