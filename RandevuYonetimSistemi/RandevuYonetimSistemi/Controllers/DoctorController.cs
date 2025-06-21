using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RandevuYonetimSistemi.Data;
using RandevuYonetimSistemi.Models;

namespace RandevuYonetimSistemi.Controllers
{
    //Doktorları yönetmek için kullanılan controller
  
    public class DoctorController : BaseController
    {
        private readonly RandevuDbContext _context;

        public DoctorController(RandevuDbContext context)
        {
            _context = context;
        }
        public IActionResult Görüntüle()
        {
            var doctors = _context.Doctors.ToList();
            return View(doctors);
        }

        public IActionResult DoktorEkle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DoktorEkle(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _context.Doctors.Add(doctor);
                _context.SaveChanges();
                return RedirectToAction("Görüntüle");
            }
            return View(doctor);
        }
        public IActionResult Güncelle(int? id)
        {
            if (id == null) return NotFound();

            var doctor = _context.Doctors.Find(id);
            if (doctor == null) return NotFound();

            return View(doctor);
        }
        [HttpPost]
        public IActionResult Güncelle(int? id, Doctor doctor)
        {
            if (id != doctor.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctor);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Doctors.Any(d => d.Id == id))
                        return NotFound();
                    throw;
                }

                return RedirectToAction("Görüntüle");
            }

            return View(doctor);
        }
        
    }
}
