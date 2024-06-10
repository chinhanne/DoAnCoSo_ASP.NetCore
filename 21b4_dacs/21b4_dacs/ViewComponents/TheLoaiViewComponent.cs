using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace WebApplication1.ViewComponents
{
    public class TheLoaiViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        public TheLoaiViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task <IViewComponentResult> InvokeAsync()
        {
            
                var theLoaiLv2 = await _db.TheLoaiLv2.Include(t => t.TheLoai).ToListAsync();
                return View(theLoaiLv2);
            

        }
    }
}
