using GuiShopping.Web.Models;
using GuiShopping.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GuiShopping.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IProductService _productService;

        private readonly ICartService _cartService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, ICartService cartService)
        {
            _logger = logger;
            _productService = productService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var products = await _productService.FindAllProducts(token);
            return View(products);
        }
        [Authorize]
        public async Task<IActionResult> Details(long id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var model = await _productService.FindProductsById(id, token);
            return View(model);
        }
        [HttpPost]
        [ActionName("Details")]
        [Authorize]

        public async Task<IActionResult> DetailsPost(ProductViewModel model)
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            CartViewModel cart = new()
            {
                CartHeader = new CartHeaderViewModel
                {
                    userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value
                }
            };
            CartDetailViewModel cartdetail = new CartDetailViewModel()
            {
                Count = model.Count,
                ProductId = model.Id,
                Product = await _productService.FindProductsById(model.Id, token)
            };

            List<CartDetailViewModel> cartdetails = new List<CartDetailViewModel>();
            cartdetails.Add(cartdetail);
            cart.CartDetails = cartdetails;

            var response = await _cartService.AddItemToCart(cart, token);

            if (response != null) return RedirectToAction(nameof(Index));
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public async Task<IActionResult> Login()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }
    }
}