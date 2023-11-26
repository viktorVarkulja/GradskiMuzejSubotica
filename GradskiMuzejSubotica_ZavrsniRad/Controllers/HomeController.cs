using GradskiMuzejSubotica_ZavrsniRad.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GradskiMuzejSubotica_ZavrsniRad.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
		public IActionResult Strucnjak()
		{
			if(HttpContext.Features.Get<ISessionFeature>()?.Session != null)
			{
				ViewData["KorisnikId"] = HttpContext.Session.GetInt32("korisnikId");
				return View();
			}
			ViewData["SessionExpire"] = "Sesija je istekla, ulogujte se opet";
			return RedirectToAction("Login", "Account");
		}
		public IActionResult Radnik()
		{
			if (HttpContext.Features.Get<ISessionFeature>()?.Session != null)
			{
				ViewData["KorisnikId"] = HttpContext.Session.GetInt32("korisnikId");
				return View();
			}
			ViewData["SessionExpire"] = "Sesija je istekla, ulogujte se opet";
			return RedirectToAction("Login", "Account");
		}
        public IActionResult Restaurator()
        {
            if (HttpContext.Features.Get<ISessionFeature>()?.Session != null)
            {
                ViewData["KorisnikId"] = HttpContext.Session.GetInt32("korisnikId");
                return View();
            }
            ViewData["SessionExpire"] = "Sesija je istekla, ulogujte se opet";
            return RedirectToAction("Login", "Account");
        }
    }
}