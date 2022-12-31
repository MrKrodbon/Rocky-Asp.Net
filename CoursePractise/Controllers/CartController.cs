using CoursePractise.Data;
using CoursePractise.Models;
using CoursePractise.Models.ViewModels;
using CoursePractise.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;

namespace CoursePractise.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmailSender _emailSender;

        private readonly Product _product = new();
        [BindProperty]
        public CartUserInqueryViewModel CartUserInqueryViewModel { get; set; }
        public CartController(ApplicationDbContext applicationDbContext, IWebHostEnvironment webHostEnvironment, IEmailSender emailSender)
        {
            _applicationDbContext = applicationDbContext;
            _webHostEnvironment = webHostEnvironment;
            _emailSender = emailSender;

        }
        public IActionResult Index()
        {
            List<ShoppingCart> shoppingCartsList = new();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart).Any())
            {
                shoppingCartsList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart);

            }
            List<int> productInCart = shoppingCartsList.Select(i => i.ProductId).ToList();



            IEnumerable<Product> productList = _applicationDbContext.Product.Where(p => productInCart.Contains(p.ID));
            return View(productList);
        }
        public IActionResult RemoveFromCart(int currentProductId)
        {

            List<ShoppingCart> shoppingCartsList = new();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart).Any())
            {
                shoppingCartsList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart);

            }

            shoppingCartsList.Remove(shoppingCartsList.FirstOrDefault(p => currentProductId == _product.ID));

            HttpContext.Session.Set(WebConstants.SessionCart, shoppingCartsList);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public IActionResult IndexPost()
        {
            
            return RedirectToAction(nameof(UserInquery));
        }

        public IActionResult UserInquery()
        {
            ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            List<ShoppingCart> shoppingCartsList = new();

            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstants.SessionCart).Any())
            {
                shoppingCartsList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstants.SessionCart);

            }
            List<int> productInCart = shoppingCartsList.Select(i => i.ProductId).ToList();



            IEnumerable<Product> productList = _applicationDbContext.Product.Where(p => productInCart.Contains(p.ID));

            CartUserInqueryViewModel = new CartUserInqueryViewModel()
            {
                ApplicationUser = _applicationDbContext.User.FirstOrDefault(u => u.Id == claim.Value),
                ProductList = productList.ToList()

            };

            return View(CartUserInqueryViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("UserInquery")]
        public async  Task<IActionResult> UserInqueryPost(CartUserInqueryViewModel cartUserInqueryViewModel)
        {
            var PathToTemplate = _webHostEnvironment.WebRootPath + Path.DirectorySeparatorChar.ToString() +
                "templates" + Path.DirectorySeparatorChar.ToString() +
                "Inquiry.html";
            var subject = "New Inquiry";
            string htmlBody = "";

            using (StreamReader streamReader = System.IO.File.OpenText(PathToTemplate))
            {
                htmlBody = streamReader.ReadToEnd();
            }

            StringBuilder productListStringBuilder = new StringBuilder();

            foreach (var product in CartUserInqueryViewModel.ProductList)
            {
                productListStringBuilder.Append($" - Name : {product.ProductName} <span style ='font-size:14px;'> (ID: {product.ID}) </span>)");

            }
            string messageBody = string.Format(htmlBody,
                CartUserInqueryViewModel.ApplicationUser.FullName,
                CartUserInqueryViewModel.ApplicationUser.Email,
                CartUserInqueryViewModel.ApplicationUser.PhoneNumber,
                productListStringBuilder.ToString()
                );

            await _emailSender.SendEmailAsync(WebConstants.EmailAdmin, subject,htmlBody);

            return RedirectToAction(nameof(UserInqueryConfirmation));
        }

        public IActionResult UserInqueryConfirmation()
        {
            HttpContext.Session.Clear();

            return View();
        }
    }
}
