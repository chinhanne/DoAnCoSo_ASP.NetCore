using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TheLoaiLv2Controller : Controller
    {

        private readonly ApplicationDbContext _db;
        public TheLoaiLv2Controller(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<TheLoaiLv2> theloailv2 = _db.TheLoaiLv2.Include("TheLoai").ToList();
            ViewBag.TheLoaiLv2 = theloailv2;
            return View();
        }
        [HttpGet]
        public IActionResult Upsert(int id)
        {
            TheLoaiLv2 theLoaiLv2 = new TheLoaiLv2();
            IEnumerable<SelectListItem> dstheloai = _db.theLoai.Select(
                item => new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                });
            ViewBag.DSTheLoai = dstheloai;
            if (id == 0) // Create / Insert
            {
                return View(theLoaiLv2);
            }
            else
            {
                theLoaiLv2 = _db.TheLoaiLv2.Include(x => x.TheLoai).FirstOrDefault(sp => sp.Id == id);
                if (theLoaiLv2 != null)
                {
                    theLoaiLv2.DateUpdated = DateTime.Now;
                }
                return View(theLoaiLv2);
            }
        }
        [HttpPost]
        public IActionResult Upsert(TheLoaiLv2 theLoaiLv2)
        {
            if (ModelState.IsValid) // Kiểm tra tính hợp lệ của dữ liệu
            {
                if (theLoaiLv2.Id == 0)
                {
                    _db.TheLoaiLv2.Add(theLoaiLv2);
                }
                else
                {
                    var TimeCreatedTL2 = _db.TheLoaiLv2.AsNoTracking().FirstOrDefault(tl => tl.Id == theLoaiLv2.Id);
                    if (TimeCreatedTL2 != null)
                    {
                        theLoaiLv2.DateCreated = TimeCreatedTL2.DateCreated;
                        theLoaiLv2.DateUpdated = DateTime.Now;
                        _db.TheLoaiLv2.Update(theLoaiLv2);

                    }

                }

                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            IEnumerable<SelectListItem> dstheloai = _db.theLoai.Select(
              item => new SelectListItem
              {
                  Value = item.Id.ToString(),
                  Text = item.Name
              });
            ViewBag.DSTheLoai = dstheloai;
            return View(theLoaiLv2);
        }

        public IActionResult Delete(int id)
        {
            var theloailv2 = _db.TheLoaiLv2.FirstOrDefault(sp => sp.Id == id);
            if (theloailv2 == null)
            {
                return NotFound();
            }
            _db.TheLoaiLv2.Remove(theloailv2);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
     
    }
}
