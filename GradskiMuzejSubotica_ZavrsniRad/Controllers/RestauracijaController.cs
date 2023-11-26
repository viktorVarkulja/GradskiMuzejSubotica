using GradskiMuzejSubotica_ZavrsniRad.Models;
using GradskiMuzejSubotica_ZavrsniRad.Models.EFRepository;
using GradskiMuzejSubotica_ZavrsniRad.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GradskiMuzejSubotica_ZavrsniRad.Controllers
{
	public class RestauracijaController : Controller
	{
		readonly ZahtevRestauracijaRepository zahtevRepository;
		readonly RestauracijaRepository restauracijaRepository;
		readonly PredmetRepository predmetRepository;
        public RestauracijaController()
        {
			zahtevRepository = new();
			restauracijaRepository = new();
			predmetRepository = new();
        }
        public IActionResult Index()
		{
			return View();
		}
		[HttpGet]
		public IActionResult ListZahtev(string korisnik)
		{
			IEnumerable<ZahtevRestauracijaBO> zahtevi = zahtevRepository.GetZahtevsByStatus(1).
				Union(zahtevRepository.GetZahtevsByStatus(2)).OrderBy(key=>key.StatusId);
			ViewData["Restaurator"] = korisnik;
			return View(zahtevi);
		}
		[HttpPost]
		public IActionResult ListZahtev(ZahtevRestauracijaBO zahtev)
		{
			return View(zahtev);
		}
		[HttpGet]
		public IActionResult ZahtevProcess(string id, string restaurator)
		{
			ZahtevRestauracijaBO? zahtev = zahtevRepository.GetZahtevById(Convert.ToInt32(id));
			PredmetBO predmet = predmetRepository.GetPredmetById(zahtev.PredmetId);
			ViewData["RestauratorId"] = restaurator;
			ViewData["Zahtev"] = id;

			return View(predmet);
		}
		[HttpPost]
		public IActionResult ZahtevProcess(PredmetBO predmet)
		{
			return View(predmet);
		}
		[HttpGet]
		public IActionResult CommentZahtev(string id, string type, string restaurator)
		{
			int zahtevId = Convert.ToInt32(id);
			int restauratorId = Convert.ToInt32(restaurator);
            ZahtevRestauracijaBO zahtev = zahtevRepository.GetZahtevById(zahtevId);
           
			CommentViewModel commentView = new();
			commentView.Id = zahtev.Id;
			commentView.PredmetId = zahtev.PredmetId;
			commentView.Comment = zahtev.Comment;
			commentView.SrucnjakId = zahtev.SrucnjakId;
            commentView.RestauratorId = restauratorId;
            if (type == "1")
			{
				commentView.StatusId = 3;
			}
			else if(type == "2")
			{
                commentView.StatusId = 2;
            }
			else if(type == "3")
			{
                commentView.StatusId = 4;
            }

			return View(commentView);
		}
		[HttpPost]
		public IActionResult CommentZahtev(CommentViewModel commentView)
		{
			try
			{
				TempData["Success"] = "Uspešno ste obradili zahtev!";
                if (commentView.StatusId == 2)
                {
                    zahtevRepository.RejectZahtev(commentView.Id, commentView.Comment);
					return RedirectToAction("Restaurator", "Home");
                }
                else if (commentView.StatusId == 3)
                {
                    zahtevRepository.AcceptZahtev(commentView.Id, commentView.RestauratorId, commentView.Comment);
                    return RedirectToAction("Restaurator", "Home");
                }
				else if (commentView.StatusId == 4)
				{
					TempData["Success"] = "Uspešno ste završili restauraciju!";
					RestauracijaBO restauracija = restauracijaRepository.GetRestauracijaByZahtev(commentView.Id);
					restauracijaRepository.FinishRestauracija(restauracija.Id, commentView.Comment);
					return RedirectToAction("Restaurator", "Home");
				}
			}
			catch (Exception e)
			{
				TempData["Error"] = "Greška prilikom upisanja komentara u bazu.\n" + e.ToString();
				return RedirectToAction("Restaurator", "Home");
			}

            return View(commentView);
		}
        public IActionResult ListResturacija()
        {
            IEnumerable<RestauracijaBO> restauracije = restauracijaRepository.GetAllRestauracija().
				OrderBy(key => zahtevRepository.GetZahtevById(key.ZahtevId).StatusId);
            return View(restauracije);
        }
        [HttpPost]
        public IActionResult ListResturacija(RestauracijaBO restauracija)
        {
            return View(restauracija);
        }
    }
}
