using Microsoft.AspNetCore.Mvc;
using RandevuYonetimSistemi.Data;
using RandevuYonetimSistemi.Models;
using System.Linq;

namespace RandevuYonetimSistemi.Controllers
{
    //Admin panelini yönetmek için kullanılan controller
    public class AdminController : BaseController
    {
        private readonly RandevuDbContext _context;

        public AdminController(RandevuDbContext context)
        {
            _context = context;
        }


        // Adminin doktor, çalışan ve adminleri görüntüleyebileceği layout sayfası
        [HttpGet]
        public IActionResult AdminLayout()
        {
            var model = new AdminPanelViewModel
            {
                Adminler = _context.Admins.ToList(),
                Calisanlar = _context.Employes.ToList(),
                Doktorlar = _context.Doctors.ToList()
            };

            return View(model);
        }

        // Admin giriş sayfası, admin bilgilerini kontrol eder ve session oluşturur
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Admin giriş işlemi, admin bilgilerini kontrol eder ve session oluşturur
        [HttpPost]
        public IActionResult Index(Admin admin)
        {
            var admn = _context.Admins.FirstOrDefault(a => a.Email == admin.Email && a.Sifre == admin.Sifre);

            if (admn != null)
            {
                // Başarılı giriş, session oluştur
                HttpContext.Session.SetString("admin", "true");
                return RedirectToAction("AdminLayout");
            }
            else
            {
                ViewBag.Hata = "Kullanıcı adı veya şifre hatalı";
                return View();
            }
        }

        // Çıkış işlemi için logout metodu (isteğe bağlı)
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("admin");
            return RedirectToAction("Index");
        }
    }
}
