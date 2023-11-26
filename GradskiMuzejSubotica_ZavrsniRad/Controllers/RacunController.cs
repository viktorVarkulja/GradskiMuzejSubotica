using GradskiMuzejSubotica_ZavrsniRad.Models;
using GradskiMuzejSubotica_ZavrsniRad.Models.EFRepository;
using GradskiMuzejSubotica_ZavrsniRad.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GradskiMuzejSubotica_ZavrsniRad.Controllers
{
	public class RacunController : Controller
	{
		readonly RacunRepository racunRepository;
		readonly IzlozbaRepository izlozbaRepository;
		public RacunController() 
		{
			racunRepository = new();
			izlozbaRepository = new();
		}
		public IActionResult Index()
		{
			return View();
		}
		[HttpGet]
		public IActionResult SetUsluga(string korisnik)
		{
			UslugaViewModel uslugaViewModel = new UslugaViewModel();
			uslugaViewModel.RadnikId = Convert.ToInt32(korisnik);
			ViewBag.Usluge = racunRepository.GetAllUsluga();
			return View(uslugaViewModel);
		}
		[HttpPost]
		public IActionResult SetUsluga(UslugaViewModel uslugaViewModel)
		{
			if(ModelState.IsValid)
			{
				if(uslugaViewModel.Usluga>=0)
				{
					int radnikId = uslugaViewModel.RadnikId;
					int uslugaId = uslugaViewModel.Usluga;
					return RedirectToAction("AddRacun", "Racun", new { radnik = radnikId, usluga = uslugaId });
				}
				else
				{
					ViewData["UslugaError"] = "Morate da izbirate uslugu";
                }
				
			}
			ViewBag.Usluge = racunRepository.GetAllUsluga();
			return View(uslugaViewModel);
		}
		[HttpGet]
		public IActionResult AddRacun(string radnik, string usluga)
		{
			RacunBO racun = new RacunBO()
			{
				RadnikId = Convert.ToInt32(radnik),
				UslugaId = Convert.ToInt32(usluga)
			};
			ViewData["UslugaPrice"] = racunRepository.GetUslugaById(Convert.ToInt32(usluga)).Price;
			return View(racun);
		}
		[HttpPost]
		public IActionResult AddRacun(RacunBO racun)
		{
			if(ModelState.IsValid)
			{
				try
				{
					racunRepository.AddRacun(racun);

					return RedirectToAction("StampaniRacun", "Racun", new
					{
						usluga = racun.UslugaId,
						amount = racun.Amount,
						date = racun.Date.Day + "/" + racun.Date.Month + "/" + racun.Date.Year,
						time = racun.Time,
						vrednost = racun.Vrednost
					});
				}
				catch (Exception e)
				{
					TempData["Error"] = "Greška prilikom unosa računa.\n" + e.ToString();
					return RedirectToAction("Radnik", "Home");
				}
				
			}
			return View(racun);
		}
		[HttpGet]
		public IActionResult StampaniRacun(string usluga, string amount, string date, string time, string vrednost)
		{
			ViewData["Usluga"] = Convert.ToInt32(usluga);
			ViewData["Amount"] = double.Parse(amount);
			ViewData["Date"] = date;
			ViewData["Time"] = time;
			ViewData["Vrednost"] = double.Parse(vrednost);
			return View();
		}
		[HttpGet]
		public IActionResult SearchRacun(string poruka)
		{
			ViewData["SearchError"] = poruka;

            return View();
		}
		[HttpPost]
		public IActionResult SearchRacun(SearchPredmetViewModel model)
		{
			if(ModelState.IsValid)
			{
				return RedirectToAction("ListRacun", "Racun", new { type = model.SearchType, name = model.SearchName, date = model.Date });
			}
			return View(model);
		}
		[HttpGet]
		public IActionResult ListRacun(string type, string name, string date)
		{
			int searchType = Convert.ToInt32(type);
			IEnumerable<RacunBO> racuni = new List<RacunBO>();
			if(name!=null || date!=null)
			{
				if (searchType == 1)
				{
					racuni = racunRepository.GetRacunsById(Convert.ToInt32(name));
				}
				else if (searchType == 2) 
				{
					racuni = racunRepository.GetRacunsByDate(Convert.ToDateTime(date));
				}
			}
			else
			{
				racuni = racunRepository.GetAllRacun();
			}
			

			if (racuni.Any())
			{
				ViewData["KorisnikId"] = HttpContext.Session.GetInt32("korisnikId");
				return View(racuni);
			}
			else
			{
				return RedirectToAction("SearchRacun", "Racun",
					new { poruka = "Racun sa ovim podacima ne postoji." });
			}
		}
		[HttpPost]
		public IActionResult ListRacun(RacunBO racun)
		{
			return View(racun);
		}

		[HttpGet]
		public IActionResult DetailsRacun(string id)
		{
			RacunBO racun = racunRepository.GetRacunById(Convert.ToInt32(id));

			return View(racun);
		}
		[HttpPost]
		public IActionResult DetailsRacun(RacunBO racun)
		{
			return View(racun);
		}
		public IActionResult SearchRacunForDelete()
		{
			return View();
		}
		[HttpPost]
		public IActionResult SearchRacunForDelete(SearchPredmetViewModel model)
		{
			RacunBO racun = racunRepository.GetRacunById(model.SearchType);
			if(ModelState.IsValid)
			{
				if(racun != null)
				{
					return RedirectToAction("DeleteRacun", "Racun", new { id = racun.Id });
				}
				else
				{
					ViewData["SearchError"] = "Ovaj racun ne postoji!";
					return View(model);
				}
			}
            
            return View(model);
        }
        [HttpGet]
		public IActionResult DeleteRacun(string id)
		{
            RacunBO racun = racunRepository.GetRacunById(Convert.ToInt32(id));

            return View(racun);
        }
        [HttpPost]
        public IActionResult DeleteRacun(RacunBO racun)
        {
			if(ModelState.IsValid)
			{
				try
				{
					racunRepository.DeleteRacun(racun.Id);
					TempData["Success"] = "Uspešno ste stornirali račun!";
					return RedirectToAction("Radnik", "Home");
				}
				catch (Exception e)
				{
					TempData["Error"] = "Greška prilikom storniranje računa.\n" + e.ToString();
					return RedirectToAction("Radnik", "Home");
				}
				
			}
            return View(racun);
        }
    }
}
