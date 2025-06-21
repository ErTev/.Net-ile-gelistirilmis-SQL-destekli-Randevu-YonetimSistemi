using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RandevuYonetimSistemi.Data;
using RandevuYonetimSistemi.Models;

namespace RandevuYonetimSistemi.Controllers
{
    //Çalışanları yönetmek için kullanılan controller
    public class EmployeController : BaseController
    {
        private readonly RandevuDbContext _context;
        public EmployeController(RandevuDbContext context)
        {
            _context = context;
        }
        public IActionResult Görüntüle()
        {
            var employes = _context.Employes.ToList();
            return View(employes);
        }

        public IActionResult ÇalışanEkle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ÇalışanEkle(Employe employe)
        {
            if (ModelState.IsValid)
            {
                _context.Employes.Add(employe);
                _context.SaveChanges();
                return RedirectToAction("Görüntüle");
            }
            return View(employe);
        }
        public IActionResult Güncelle(int? id)
        {
            if (id == null) return NotFound();

            var employe = _context.Employes.Find(id);
            if (employe == null) return NotFound();

            return View(employe);
        }
        [HttpPost]
        public IActionResult Güncelle(int? id, Employe employe)
        {
            if (id != employe.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employe);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Employes.Any(d => d.Id == id))
                        return NotFound();
                    throw;
                }

                return RedirectToAction("Görüntüle");
            }

            return View(employe);
        }
        
    }
}
