using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Areas.Customer.Controllers
{
    public class ChiTietSanPhamController : Controller
    {
        [Area("Customer")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
