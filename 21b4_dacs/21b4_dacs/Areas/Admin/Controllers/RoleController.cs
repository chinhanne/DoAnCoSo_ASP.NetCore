using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class RoleController : Controller
    {
      
        public IActionResult Index()
        {
            
            return View();
        }
    }
}
