using GradskiMuzejSubotica_ZavrsniRad.Models;
using GradskiMuzejSubotica_ZavrsniRad.Models.Account;
using GradskiMuzejSubotica_ZavrsniRad.Models.EFRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GradskiMuzejSubotica_ZavrsniRad.Controllers
{
	public class AccountController : Controller
	{
		private readonly KorisnikRepository korisnikRepository;
		private readonly RolaRepository rolaRepository;

        public AccountController()
        {
            korisnikRepository = new KorisnikRepository();
			rolaRepository = new RolaRepository();
        }

		private List<RolaBO> RolaList()
		{
			return (List<RolaBO>)rolaRepository.GetAllRola();
		}
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Register()
		{
			ViewBag.Roles = RolaList();
			return View();
		}

		[HttpPost]
		public IActionResult Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				
				var korisnik = new KorisnikBO
				{
					UserName = model.UserName,
					Password = model.Password,
					Name = model.Name,
					Role = model.Role
				};

				if(korisnikRepository.CheckUserName(korisnik.UserName))
				{
					ViewBag.Roles = RolaList(); 
					ViewData["LoginError"] = "Korisničko ime već postoji";
					return View(model);
				}

				if(!rolaRepository.CheckRole(korisnik.Role)) 
				{
					ViewBag.Roles = RolaList();
					ViewData["RolesError"] = "Izaberite rolu";
					return View(model);
				}

				if(korisnikRepository.ResgisterResult(korisnik))
				{
                    return RedirectToAction("Admin", "Home");
                }
			}
			return View(model);
		}

		public IActionResult Login()
		{
			
			return View();
		}

		[HttpPost]
		public IActionResult Login(LoginViewModel login)
		{
			if(ModelState.IsValid)
			{
				var korisnik = new KorisnikBO
				{
					UserName = login.UserName,
					Password = login.Password
				};
				if(korisnikRepository.CheckUserName(korisnik.UserName))
				{
					if(korisnikRepository.CheckPassword(korisnik.UserName, korisnik.Password))
					{
						KorisnikBO korisnikBO = korisnikRepository.GetKorisnikByUserName(korisnik.UserName);
						HttpContext.Session.SetInt32("korisnikId", korisnikBO.Id);
						if(korisnikBO.Role == 1)
						{
							return RedirectToAction("Strucnjak", "Home");
						}
						else if(korisnikBO.Role == 2)
						{
							return RedirectToAction("Radnik", "Home");
						}
						else if (korisnikBO.Role == 3)
						{
							return RedirectToAction("Restaurator", "Home");
						}
						else if (korisnikBO.Role == 4)
						{
							return RedirectToAction("Admin", "Home");
						}
					}
					else
					{
						ViewData["PasswordError"] = "Lozinka nije validna.";
						return View(login);
					}
				}
				else
				{
					ViewData["UserError"] = "Ovo korisnicko ime ne postoji.";
					return View(login);
				}
			}
			return View(login);
		}
	}
}
