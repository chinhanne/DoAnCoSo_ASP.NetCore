using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChiTietHoaDonController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ChiTietHoaDonController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int Id)
        {
            IEnumerable<ChiTietHoaDon> CTHD = _context.ChiTietHoaDon.Include("HoaDon")
            .Include("SanPham").Where(cthd => cthd.HoaDonId == Id).ToList();
            return View(CTHD);
        }
    }
}
