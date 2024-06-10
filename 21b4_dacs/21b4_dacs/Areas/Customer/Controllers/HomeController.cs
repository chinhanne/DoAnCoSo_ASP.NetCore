using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using WebApplication1.Data;
//using WebApplication1.Migrations;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
	[Area("Customer")]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ApplicationDbContext _db;
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly UserManager<IdentityUser> _userManager;
		public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
		{
			_logger = logger;
			_db = db;
			_signInManager = signInManager;
			_userManager = userManager;
		}
		
		public async Task<IActionResult> Index()
		{
			
			IdentityUser? user = await _userManager.GetUserAsync(User);
			if(user != null && await _userManager.IsInRoleAsync(user, "Admin"))
				return LocalRedirect("~/Admin/Admin/Index");
			

			IEnumerable<SanPham> sanpham = _db.SanPham.Include(sp => sp.TheLoaiLv2).ToList();
			return View(sanpham);
		}

	

		public IActionResult TimKiem(string tk, string cate1, string cate2)
		{
			IEnumerable<SanPham> sanpham = _db.SanPham.Include(sp => sp.TheLoaiLv2).Include(sp => sp.TheLoaiLv2.TheLoai).ToList();
			if (!String.IsNullOrEmpty(tk))
			{
				sanpham = _db.SanPham.Where(s => s.Name.Contains(tk)).ToList();
				ViewBag.TitleTK = "Kết quả tìm kiếm: " + tk;
			}
			else if (!String.IsNullOrEmpty(cate1))
			{
				var loai1 = _db.TheLoaiLv2.FirstOrDefault(p => p.Name == cate1);
				sanpham = _db.SanPham.Where(p => p.TheLoaiLv2Id == loai1.Id).ToList();
				ViewBag.TitleTK = "Kết quả tìm kiếm thể loại: " + cate1;
			}
			else if (!String.IsNullOrEmpty(cate2))
			{
				var loai2 = _db.theLoai.FirstOrDefault(p => p.Name == cate2);
				sanpham = _db.SanPham.Where(p => p.TheLoaiLv2.TheLoaiId == loai2.Id).ToList();
				ViewBag.TitleTK = "Kết quả tìm kiếm thể loại: " + cate2;
			}
            ViewBag.SanPhamTimKiem = sanpham;

			return View(sanpham);
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

		[HttpGet]
		public IActionResult Details(int sanphamId)
		{
			GioHang giohang = new GioHang()
			{
				SanPhamId = sanphamId,
				SanPham = _db.SanPham.Include("TheLoaiLv2").FirstOrDefault(sp => sp.Id == sanphamId),
				Quantity = 1
			};


			return View(giohang);
		}

		[HttpPost]
		[Authorize]

		public IActionResult Details(GioHang giohang)
		{

			var identity = (ClaimsIdentity)User.Identity;
			var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
			giohang.ApplicationUserId = claim.Value;

			var giohangdb = _db.GioHang.FirstOrDefault(sp => sp.SanPhamId == giohang.SanPhamId && sp.ApplicationUserId == giohang.ApplicationUserId);
			if (giohangdb == null)
			{
				_db.GioHang.Add(giohang);
			}
			else
			{
				giohangdb.Quantity += giohang.Quantity;
			}
			_db.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}