using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ThanhToanGioHangController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
