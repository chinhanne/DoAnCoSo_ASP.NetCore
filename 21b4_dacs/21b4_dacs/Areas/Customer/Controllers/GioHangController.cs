using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Route("Customer/GioHang")]
    [Authorize]
    public class GioHangController : Controller
    {
        private readonly ApplicationDbContext _db;
        public GioHangController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var idenity = (ClaimsIdentity)User.Identity; // Claim đại diện cho thông tin người dùng
            var claim = idenity.FindFirst(ClaimTypes.NameIdentifier);

            GioHangViewModel giohang = new GioHangViewModel()
            {
                DsGioHang = _db.GioHang.Include("SanPham").Where(gh => gh.ApplicationUserId == claim.Value).ToList(),
				HoaDon= new HoaDon()

			};

            foreach (var item in giohang.DsGioHang)
            {
                item.ProductPrice = item.Quantity * item.SanPham.PriceSale;
                giohang.TotalPrice += item.ProductPrice;
            }
            return View(giohang);
        }
        [Route("Insert/{id}")]
        [Authorize]
        public async Task<IActionResult> Insert(int id)
        {
            var idenity = (ClaimsIdentity)User.Identity;
            var claim = idenity.FindFirst(ClaimTypes.NameIdentifier);
            var data = await _db.GioHang.FirstOrDefaultAsync(x => x.SanPhamId == id && x.ApplicationUserId == claim.Value);
            if (data == null)
            {
                var gioHang = new GioHang()
                {
                    Id = 0,
                    SanPhamId = id,
                    Quantity = 1,
                    ApplicationUserId = claim!.Value
                };
                _db.GioHang.Add(gioHang);
                await _db.SaveChangesAsync();
            }
            else
            {
                data.Quantity += 1;
                _db.Entry(data).State = EntityState.Modified; // đánh dấu rằng đối tượng data đã được thay đổi và cần được cập nhật.
                await _db.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
        [Route("Tang")]
        public IActionResult Tang(int giohangId)
        {
            var giohang = _db.GioHang.FirstOrDefault(gh => gh.Id == giohangId);
            giohang.Quantity += 1;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Route("Giam")]
        public IActionResult Giam(int giohangId)
        {
            var giohang = _db.GioHang.FirstOrDefault(gh => gh.Id == giohangId);
            giohang.Quantity -= 1;
            if (giohang.Quantity == 0)
            {
                _db.GioHang.Remove(giohang);
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Route("Xoa")]
        public IActionResult Xoa(int giohangId)
        {
            var giohang = _db.GioHang.FirstOrDefault(gh => gh.Id == giohangId);
            _db.GioHang.Remove(giohang);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
		[HttpGet]
		[Route("ThanhToan")]
		[Authorize]
		public async Task<IActionResult> ThanhToan(int giohangId)
		{
			var identity = (ClaimsIdentity)User.Identity;
			var claim = identity.FindFirst(ClaimTypes.NameIdentifier);


			GioHangViewModel giohang = new GioHangViewModel()
			{
				DsGioHang = _db.GioHang
				.Include("SanPham")
				.Where(gh => gh.ApplicationUserId == claim.Value)
				.ToList(),

				HoaDon = new HoaDon()

			};
			foreach (var item in giohang.DsGioHang)
			{
				item.ProductPrice = item.Quantity * item.SanPham.PriceSale;
				giohang.HoaDon.Total += item.ProductPrice;
			}

			giohang.HoaDon.ApplicationUserId = claim.Value;
			giohang.HoaDon.OrderDate = DateTime.Now;
			giohang.HoaDon.OrderStatus = "Da xac nhan";
			return View(giohang);
		}
		[HttpPost("ThanhToanSubmit")]
		[ValidateAntiForgeryToken]
		[Authorize]
		public IActionResult ThanhToan(GioHangViewModel giohang,HoaDon hoadon, string paymentMethod)
		{
			var identity = (ClaimsIdentity)User.Identity;
			var claim = identity.FindFirst(ClaimTypes.NameIdentifier);

			giohang.DsGioHang = _db.GioHang.Include(x => x.SanPham)
				.Where(gh => gh.ApplicationUserId == claim.Value).ToList();
			giohang.HoaDon.ApplicationUserId = claim.Value;
			giohang.HoaDon.OrderDate = DateTime.Now;
			giohang.HoaDon.OrderStatus = "Đang xác nhận";
            if (paymentMethod == "ship-cod")
            {
                giohang.HoaDon.Paymentmethod = "Thanh toán khi nhận hàng";
            }
            //chưa làm
            //else if (paymentMethod == "vn-pay")
            //{
            //    giohang.HoaDon.Paymentmethod = "Thanh toán bằng Vn-Pay";
            //}
            foreach (var item in giohang.DsGioHang)
			{
				item.ProductPrice = item.Quantity * item.SanPham.PriceSale;
				giohang.HoaDon.Total += item.ProductPrice;
			}
			_db.HoaDon.Add(giohang.HoaDon);
			_db.SaveChanges();

			foreach (var item in giohang.DsGioHang)
			{
				ChiTietHoaDon chitiethoadon = new ChiTietHoaDon()
				{
					SanPhamId = item.SanPhamId,
					HoaDonId = giohang.HoaDon.Id,
					ProductPrice = item.ProductPrice,
					Quantity = item.Quantity,
				};
				_db.ChiTietHoaDon.Add(chitiethoadon);
				_db.SaveChanges();
			}

			_db.GioHang.RemoveRange(giohang.DsGioHang);
			_db.SaveChanges();
            //return RedirectToAction("Index");
            return View("Order_Complete",hoadon.Id);
		}

      
    }
}
