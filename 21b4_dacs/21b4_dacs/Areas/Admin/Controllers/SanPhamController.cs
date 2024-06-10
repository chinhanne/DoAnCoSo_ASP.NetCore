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

    public class SanPhamController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SanPhamController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize]
        public IActionResult Index()
        {
            IEnumerable<SanPham> sanpham = _context.SanPham.Include("TheLoaiLv2").ToList();

            return View(sanpham);


        }


        [HttpGet]
        public IActionResult Upsert(int id)
        {
            SanPham? sanpham = new SanPham();
            IEnumerable<SelectListItem> dstheloai = _context.TheLoaiLv2.Select(
                item => new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                });
            ViewBag.DSTheLoai = dstheloai;
            if (id == 0) // Create / Insert
            {
                return View(sanpham);
            }
            else
            {
                sanpham = _context.SanPham.Include(x => x.TheLoaiLv2).FirstOrDefault(sp => sp.Id == id);
                if (sanpham != null)
                {
                    sanpham.DateUpdated = DateTime.Now;
                }
                return View(sanpham);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(SanPham sanpham, IFormFile imageUrl, IFormFile imageUrl1, IFormFile imageUrl2, IFormFile imageUrl3, IFormFile imageUrl4)
        {
            if (ModelState.IsValid)
            {
                if (sanpham.PriceSale > sanpham.Price)
                {
                    ModelState.AddModelError("PriceSale", "Giá bán không được lớn hơn giá gốc.");
                }
                else
                {
                    if (sanpham.Id == 0)
                    {
                        if (imageUrl != null && imageUrl1 != null && imageUrl2 != null && imageUrl3 != null && imageUrl4 != null)
                        {
                            sanpham.ImageUrl = await SaveImage(imageUrl);
                            sanpham.ImageUrl1 = await SaveImage(imageUrl1);
                            sanpham.ImageUrl2 = await SaveImage(imageUrl2);
                            sanpham.ImageUrl3 = await SaveImage(imageUrl3);
                            sanpham.ImageUrl4 = await SaveImage(imageUrl4);
                            sanpham.DateCreated = DateTime.Now;
                            sanpham.DateUpdated = DateTime.Now;
                            _context.SanPham.Add(sanpham);
                        }
                    }
                    else
                    {
                        var TimeCreatedSanPham = _context.SanPham.AsNoTracking().FirstOrDefault(sp => sp.Id == sanpham.Id);
                        if (TimeCreatedSanPham != null)
                        {
                            if (imageUrl != null && imageUrl1 != null && imageUrl2 != null && imageUrl3 != null && imageUrl4 != null)
                            {
                                sanpham.ImageUrl = await SaveImage(imageUrl);
                                sanpham.ImageUrl1 = await SaveImage(imageUrl1);
                                sanpham.ImageUrl2 = await SaveImage(imageUrl2);
                                sanpham.ImageUrl3 = await SaveImage(imageUrl3);
                                sanpham.ImageUrl4 = await SaveImage(imageUrl4);
                                sanpham.DateCreated = TimeCreatedSanPham.DateCreated;
                                sanpham.DateUpdated = DateTime.Now;
                                _context.SanPham.Update(sanpham);
                            }
                        }  
                    }
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                
            }
            IEnumerable<SelectListItem> dstheloai = _context.TheLoaiLv2.Select(
            item => new SelectListItem
            {
                Value = item.Id.ToString(),
                Text = item.Name
            });
            ViewBag.DSTheLoai = dstheloai;
            return View(sanpham);
        }

        private async Task<string> SaveImage(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/images", image.FileName);

            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return "/images/" + image.FileName;
        }



        public IActionResult Delete(int id)
        {
            var sanpham = _context.SanPham.FirstOrDefault(sp => sp.Id == id);
            if (sanpham == null)
            {
                return NotFound();
            }
            _context.SanPham.Remove(sanpham);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

       
    }
}
