using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using RandevuYonetimSistemi.Data;
using RandevuYonetimSistemi.Models;
using RandevuYonetimSistemi.Services;
using System.Collections.Generic;
using System.Net.Mail;

namespace RandevuYonetimSistemi.Controllers
{
    public class SickPersonController : Controller
    {
        public string _MailMesaj { get; set; } //Göndereceğimiz mailin mesaj içeriği
        EmailService _emailService { get; set; }//Mail göndermek için mail servisimiz
        public string _Subject { get; set; }// Göndereceğimiz mailin konusu


        private readonly RandevuDbContext _context;
        public SickPersonController(RandevuDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // Bu metot, hastanın randevu kontrolden post ettiği TC kimlik numarasına göre mevcut tarihten sonraki randevularını görüntüler.
        #region Randevu Görüntüle 
        [HttpPost]
        public IActionResult RandevuGörüntüle(string tc)
        {
            var tarih = DateTime.Now;
            var hasta = _context.SickPeople.FirstOrDefault(o => o.Tc == tc);
            if (hasta == null)
            {
                ViewBag.Hata = "Kişi bulunamadı.";
                return View("RandevuKontrol");
            }

            var randevular = _context.Appointments
                .Where(o => o.HastaId == hasta.Id && (o.RandevuTarihi>=tarih))
                .Include(o => o.Doctor) // ✅ Doktor bilgilerini de getir
                .ToList();

            ViewBag.Randevular = randevular;
            ViewBag.HastaAdi = hasta.Adi + " " + hasta.Soyadi;

            return View();


        }
        #endregion

        // Bu metot,hastanın T.C'sini gireceği sayfayı açar.
        #region Randevu Kontrol
        public IActionResult RandevuKontrol()
        {
            return View();
        }
        #endregion


        
        [HttpPost]//Seçilen tarihe göre boş saatleri çekiuoruz
        public IActionResult GetAvailableTimes(int doctorId, DateTime randevuTarihi)
        {
            // Seçilen tarihe göre boş saatleri çek
            var availableTimes = GetRandevuSaatleri(randevuTarihi, doctorId);

            return Json(availableTimes);
        }

        // Seçilen Doktora ve Tarihe göre boş randevu saatlerini döndüren metot
        private List<string> GetRandevuSaatleri(DateTime randevuTarihi, int? doctorId = null)
        {
            var saatler = new List<DateTime>
    {
        new DateTime(randevuTarihi.Year, randevuTarihi.Month, randevuTarihi.Day, 9, 0, 0),
        new DateTime(randevuTarihi.Year, randevuTarihi.Month, randevuTarihi.Day, 9, 20, 0),
        new DateTime(randevuTarihi.Year, randevuTarihi.Month, randevuTarihi.Day, 9, 40, 0),
        new DateTime(randevuTarihi.Year, randevuTarihi.Month, randevuTarihi.Day, 10, 0, 0),
        new DateTime(randevuTarihi.Year, randevuTarihi.Month, randevuTarihi.Day, 10, 20, 0),
        new DateTime(randevuTarihi.Year, randevuTarihi.Month, randevuTarihi.Day, 10, 40, 0),
        new DateTime(randevuTarihi.Year, randevuTarihi.Month, randevuTarihi.Day, 11, 0, 0),
        new DateTime(randevuTarihi.Year, randevuTarihi.Month, randevuTarihi.Day, 11, 20, 0),
        new DateTime(randevuTarihi.Year, randevuTarihi.Month, randevuTarihi.Day, 13, 0, 0),
        new DateTime(randevuTarihi.Year, randevuTarihi.Month, randevuTarihi.Day, 13, 20, 0),
        new DateTime(randevuTarihi.Year, randevuTarihi.Month, randevuTarihi.Day, 13, 40, 0),
        new DateTime(randevuTarihi.Year, randevuTarihi.Month, randevuTarihi.Day, 14, 0, 0),
        new DateTime(randevuTarihi.Year, randevuTarihi.Month, randevuTarihi.Day, 14, 20, 0),
        new DateTime(randevuTarihi.Year, randevuTarihi.Month, randevuTarihi.Day, 14, 40, 0),
        new DateTime(randevuTarihi.Year, randevuTarihi.Month, randevuTarihi.Day, 15, 0, 0),
        new DateTime(randevuTarihi.Year, randevuTarihi.Month, randevuTarihi.Day, 15, 20, 0),
        new DateTime(randevuTarihi.Year, randevuTarihi.Month, randevuTarihi.Day, 15, 40, 0),
        new DateTime(randevuTarihi.Year, randevuTarihi.Month, randevuTarihi.Day, 16, 0, 0),
        new DateTime(randevuTarihi.Year, randevuTarihi.Month, randevuTarihi.Day, 16, 20, 0),
        new DateTime(randevuTarihi.Year, randevuTarihi.Month, randevuTarihi.Day, 16, 40, 0)
    };

            // Eğer doktorId belirtilmişse, o doktora ait randevu saatlerini filtrele
            if (doctorId.HasValue)
            {
                var randevular = _context.Appointments
                    .Where(a => a.DoctorId == doctorId && a.RandevuTarihi.Date == randevuTarihi.Date)
                    .Select(a => a.RandevuTarihi)
                    .ToList();

                saatler.RemoveAll(saat => randevular.Any(r => r.Hour == saat.Hour && r.Minute == saat.Minute));
            }
            
            return saatler.Select(s => s.ToString("HH:mm")).ToList();
        }

        // Bu metot, hastanın randevu alacağı sayfayı açar. Eğer TempData'da kontrol bilgisi varsa, doktorları ve randevu saatlerini yükler.
        #region RANDEVUAL
        public IActionResult RandevuAl()
        {
            bool kontrol = TempData["kontrol"] != null && Convert.ToBoolean(TempData["kontrol"]);

            if (kontrol)
            {
                var doktorlar = _context.Doctors.Where(d=>d.Aktiflik).ToList();
                ViewBag.Doctorlar = doktorlar;

                var enYakinTarih = DateTime.Now.AddDays(1);
                int? ilkDoktorId = doktorlar.FirstOrDefault()?.Id;

                var randevuSaatleri = GetRandevuSaatleri(enYakinTarih, ilkDoktorId);
                ViewBag.RandevuSaatleri = randevuSaatleri;

                return View();
            }

            return RedirectToAction("PersonSave");
        }


        [HttpPost]
        public IActionResult RandevuAl(Appointment randevu)
        {
            ModelState.Remove("Doctor");
            ModelState.Remove("Hasta");

            var doktorlar = _context.Doctors.Where(d=>d.Aktiflik).ToList();
            ViewBag.Doctorlar = doktorlar;

            randevu.RandevuGirisSaati = DateTime.Now;

            var hasta = new SickPerson
            {
                Adi = TempData["Adi"]?.ToString(),
                Soyadi = TempData["Soyadi"]?.ToString(),
                Tc = TempData["Tc"]?.ToString(),
                Email = TempData["Email"]?.ToString(),
                Telefon = TempData["Telefon"]?.ToString(),
            };

            var HastaKontrol = _context.SickPeople.Where(o => o.Tc == hasta.Tc);
            if (!HastaKontrol.Any())
            {
                _context.SickPeople.Add(hasta);
                _context.SaveChanges();
            }
            else
            {
                hasta = HastaKontrol.FirstOrDefault();
            }

            randevu.HastaId = hasta.Id;
            randevu.Doctor = _context.Doctors.FirstOrDefault(o => o.Id == randevu.DoctorId);
            randevu.Hasta = _context.SickPeople.FirstOrDefault(o => o.Id == randevu.HastaId);

            // Aynı saate randevu varsa engelle
            bool doluMu = _context.Appointments.Any(a =>
                a.DoctorId == randevu.DoctorId &&
                a.RandevuTarihi == randevu.RandevuTarihi);

            if (doluMu)
            {
                ModelState.AddModelError("", "Seçilen saat zaten dolu.");
            }

            if (ModelState.IsValid)
            {
                
                _Subject = "Randevu Bilgisi";
                _MailMesaj = $"Sayın {hasta.Adi} {hasta.Soyadi},\n" +
                             $"Randevunuz {randevu.RandevuTarihi.ToString("dd/MM/yyyy HH:mm")} tarihinde " +
                             $"{randevu.Doctor.Unvan} {randevu.Doctor.Adi} {randevu.Doctor.Soyadi} ile alınmıştır.\n" +
                             $"Lütfen belirtilen tarihte hastaneye geliniz.";
                _context.Appointments.Add(randevu);
                _context.SaveChanges();
                _emailService.SendEmailAsync(hasta.Email, _Subject, _MailMesaj);
                return RedirectToAction("Index","Home");


            }

            // ModelState geçersizse saatleri yeniden yükle
            ViewBag.RandevuSaatleri = GetRandevuSaatleri(randevu.RandevuTarihi.Date, randevu.DoctorId);

            return View(randevu);
        }

        #endregion


        //Doğrulama kodu için gerekli metotlar doğrulama kodu doğru girlirse randevu al sayfasına yönlendirir.
        #region dogrulama
        public IActionResult dogrulama()
        {
            return View();
        }
        [HttpPost]
        public IActionResult dogrulama(string kod)
        {
            var dogrulamaKodu = TempData["MailMesaj"]?.ToString();
            TempData["kontrol"] = false;

            if (kod == dogrulamaKodu)
            {
                TempData["kontrol"] = true;

                return RedirectToAction("RandevuAl");
            }
            return RedirectToAction("PersonSave");
        }
        #endregion


        // Bu metot, hastanın kayıt olacağı sayfayı açar.
        #region personsave
        public IActionResult PersonSave()
        {
            return View();
        }

        // Bu metot, hastanın kayıt bilgilerini alır ve doğrulama kodu gönderir.
        [HttpPost]
        public IActionResult PersonSave(SickPerson hasta)
        {


            if (ModelState.IsValid)
            {
                _Subject = "Doğrulama Kodu :";
                Random random = new Random();

                _MailMesaj = random.Next(1000, 9999).ToString();//Rastgele dört haneli doğrulama kodu oluşturur.

                //Kişinin bilgilerini alır ve randevu al sayfasında randevu alındıktan sonra kayıt olmasını sağlar.
                TempData["Id"] = hasta.Id;
                TempData["MailMesaj"] = _MailMesaj;
                TempData["Adi"] = hasta.Adi;
                TempData["Soyadi"] = hasta.Soyadi;
                TempData["Tc"] = hasta.Tc;
                TempData["Email"] = hasta.Email;
                TempData["Telefon"] = hasta.Telefon;
                _emailService.SendEmailAsync(hasta.Email, _Subject, _MailMesaj);
                return View("dogrulama");
            }
            return View();
        }
        #endregion
    }
}
