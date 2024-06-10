using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Route("Customer/SanPham")]
    public class SanPhamController : Controller
    {
        private readonly ILogger<SanPhamController> _logger;
        private readonly ApplicationDbContext _db;
        public SanPhamController(ILogger<SanPhamController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        [Route("Index")]
        public IActionResult Index()
        {
            IEnumerable<SanPham> sanpham = _db.SanPham.Include(sp => sp.TheLoaiLv2).Include(sp => sp.TheLoaiLv2.TheLoai).ToList();
           
            return View(sanpham);


        }
        [Route("Details/{id}")]
        public IActionResult Details(int id)
        {
            GioHang giohang = new GioHang()
            {
                SanPhamId = id,
                SanPham = _db.SanPham.Include("TheLoaiLv2").FirstOrDefault(sp => sp.Id == id),
                Quantity = 1
            };
            ViewBag.SanPham = _db.SanPham.FirstOrDefault(x => x.Id == id);
            return View("Details");
        }

        public IActionResult TimKiem(string priceRange)
        {
            
            IEnumerable<SanPham> sanpham = _db.SanPham.Include(sp => sp.TheLoaiLv2).ToList();
            switch (priceRange)
            {
                case "0-50000":
                    sanpham = _db.SanPham.Where(p => p.PriceSale >= 0 && p.PriceSale < 50000);
                    ViewBag.TitleTK = "Kết quả tìm kiếm theo giá tiền: Dưới 50,000đ";
                    break;
                case "50000-200000":
                    sanpham = _db.SanPham.Where(p => p.PriceSale >= 50000 && p.PriceSale < 200000);
                    ViewBag.TitleTK = "Kết quả tìm kiếm theo giá tiền: Từ 50,000đ - 200,000đ";
                    break;
                case "200000-500000":
                    sanpham = _db.SanPham.Where(p => p.PriceSale >= 200000 && p.PriceSale < 500000);
                    ViewBag.TitleTK = "Kết quả tìm kiếm theo giá tiền: Từ 200,000đ - 500,000đ";
                    break;
                case "500000":
                    sanpham = _db.SanPham.Where(p => p.PriceSale >= 500000);
                    ViewBag.TitleTK = "Kết quả tìm kiếm theo giá tiền: Trên 500,000đ";
                    break;
                default:
                    ViewBag.TitleTK = "Kết quả tìm kiếm theo giá tiền: Không có sản phẩm nào!";
                    break;
            }

            
            ViewBag.SanPhamTimKiem = sanpham.ToList();

            return View(sanpham);
        }
    }
}
