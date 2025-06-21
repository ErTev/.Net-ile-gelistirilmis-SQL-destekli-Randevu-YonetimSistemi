using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RandevuYonetimSistemi.Controllers
{
    // Bu controller, tüm admin işlemleri için temel kontrolcü olarak kullanılır.
    //Doktor, Employe ve Admin Controllerlar bu sınıftan türeyecektir.
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.RouteData.Values["controller"]?.ToString();
            var action = context.RouteData.Values["action"]?.ToString();

            // Admin Giriş sayfası ve Index Post (login) muaf
            if (controller == "Admin" && (action == "Index" || action == "IndexPost"))
            {
                base.OnActionExecuting(context);
                return;
            }

            // Session'dan admin kontrolü
            var isAdmin = context.HttpContext.Session.GetString("admin");

            if (isAdmin != "true")
            {
                // Admin değilse giriş sayfasına yönlendir
                context.Result = new RedirectToActionResult("Index", "Admin", null);
            }

            base.OnActionExecuting(context);
        }
    }
}
