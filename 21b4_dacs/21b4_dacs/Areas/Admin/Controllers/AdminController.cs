using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
//using WebApplication1.Migrations;
using WebApplication1.Models;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AdminController(ApplicationDbContext db)
        {
            _db = db;
        }
       
        [Authorize]
        public IActionResult Index()
        {
            IEnumerable<SanPham> sanpham = _db.SanPham.Include(sp => sp.TheLoaiLv2).Include(sp => sp.TheLoaiLv2.TheLoai).ToList();
            return View(sanpham);
        }
        [HttpGet]
        public IActionResult Upsert(int id)
        {
            SanPham sanPham = new SanPham();
            IEnumerable<SelectListItem> dsteloai = _db.TheLoaiLv2.Select(
                item => new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name,
                }
                );
            ViewBag.Dsteloai = dsteloai;
            if (id == 0)
            {

                return View(sanPham);
            }
            else
            {
                sanPham = _db.SanPham.Include("TheLoaiLv2").FirstOrDefault(sp => sp.Id == id);
                return View(sanPham);
            }
        }
        [HttpPost]
        public IActionResult Upsert(SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                if (sanPham.Id == 0)
                {
                    _db.SanPham.Add(sanPham);
                }
                else
                {
                    _db.SanPham.Update(sanPham);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var sanpham = _db.SanPham.FirstOrDefault(sp => sp.Id == id);
            if (sanpham == null)
            {
                return NotFound();
            }
            _db.SanPham.Remove(sanpham);
            _db.SaveChanges();
            return Json(new { success = true });
        }

        [Route("Detail/{id}")]
        public IActionResult Details(int id)
        {
            GioHang giohang = new GioHang()
            {
                SanPhamId = id,
                SanPham = _db.SanPham.Include("TheLoaiLv2").FirstOrDefault(sp => sp.Id == id),
                Quantity = 1
            };
            ViewBag.SanPham = _db.SanPham.FirstOrDefault(x => x.Id == id);
            //ViewBag.SanPham.Description = ViewBag.SanPham.Description.Replace("<br>", "</p><p>");
            return View("Details");
        }
    }
}
