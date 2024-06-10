 using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
    [Area("Admin")]
    public class TheLoaiController : Controller
    {
        private readonly ApplicationDbContext _db;
        public TheLoaiController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()

        {
            var theloai = _db.theLoai.ToList();
            ViewBag.TheLoai = theloai;
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            var model = new TheLoai();
            return View(model);

        }
        [HttpPost]
        public IActionResult Create(TheLoai theLoai)
        {
            if (ModelState.IsValid)
            {
                _db.theLoai.Add(theLoai);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var theloai = _db.theLoai.Find(id);
            theloai.DateUpdated = DateTime.Now;
            return View(theloai);

        }
        [HttpPost]
        public IActionResult Edit(TheLoai theloai)
        {
            var TimeCreatedTheLoai = _db.theLoai.AsNoTracking().FirstOrDefault(tl => tl.Id == theloai.Id);
            if (TimeCreatedTheLoai != null)
            {
                if (ModelState.IsValid)
                {
                    theloai.DateCreated = TimeCreatedTheLoai.DateCreated;
                    theloai.DateUpdated = DateTime.Now;
                    _db.theLoai.Update(theloai);
                    _db.SaveChanges();
                    return RedirectToAction("Index");

                }
                
            }
            return View();


        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var theloai = _db.theLoai.Find(id);
            return View(theloai);
        }
        [HttpPost]
        public IActionResult DeleteConfirm(int id)
        {
            var theloai = _db.theLoai.Find(id);
            if (theloai == null)
            {
                return NotFound();
            }
            _db.theLoai.Remove(theloai);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            var theloai = _db.theLoai.Find(id);

            if (theloai == null)
            {
                return NotFound();
            }

            return View(theloai);
        }
    }
}
