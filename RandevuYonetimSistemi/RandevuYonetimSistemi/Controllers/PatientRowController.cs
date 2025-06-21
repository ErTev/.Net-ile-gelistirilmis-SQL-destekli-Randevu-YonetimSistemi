using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RandevuYonetimSistemi.Data;
using RandevuYonetimSistemi.Models;

namespace RandevuYonetimSistemi.Controllers
{
    //Kişilerin mevcut güne ait randevu sıralarını görüntülemek için kullanılan controller
    public class PatientRowController : Controller
    {
        private readonly RandevuDbContext  _context;

        public PatientRowController(RandevuDbContext context)
        {
            _context = context;
        }

        //Mevctut güne ait randevuları çekerek kişilerin 5'er adet randevu sıralarını görüntüler.
        //Dinamiktir her bir dakika başına güncellenir.
        //public IActionResult SıraGörüntüle()
        //{
        //    var simdi = DateTime.Now;
        //    var bugun = simdi.Date;

        //    var doktorRandevulari = _context.Doctors
        //        .Include(d => d.Randevular.Where(a => a.RandevuTarihi.Date == bugun))
        //            .ThenInclude(a => a.Hasta)
        //        .ToList()
        //        .Select(doktor => doktor.Randevular
        //            .OrderBy(r => r.RandevuTarihi)
        //            .Take(5)
        //            .ToList()
        //        )
        //        .ToList();

        //    return View(doktorRandevulari);
        //}
        public IActionResult SıraGörüntüle()
        {
            var simdi = DateTime.Now;

            var doktorRandevulari = _context.Doctors
                .Include(d => d.Randevular.Where(a => a.RandevuTarihi >= simdi && ((a.RandevuTarihi.Day <= simdi.AddDays(1).Day)))) // SAAT + TARİH kontrolü
                    .ThenInclude(a => a.Hasta)
                .ToList()
                .Select(doktor => doktor.Randevular
                    .OrderBy(r => r.RandevuTarihi)
                    .Take(5)
                    .ToList()
                )
                .ToList();

            return View(doktorRandevulari);
        }

    }
}
