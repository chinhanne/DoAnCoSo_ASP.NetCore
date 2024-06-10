using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication1.Data;

namespace WebApplication1.ViewComponents
{
    public class CountGioHangViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        public CountGioHangViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.CountCart = 0;
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null)
            {
                return View();
            }
            var giohang = _db.GioHang.Where(p => p.ApplicationUserId == claim.Value).ToList();
            if (giohang != null)
            {
                ViewBag.CountCart = giohang.Count();
            }
            return View();
        }
    }
}
