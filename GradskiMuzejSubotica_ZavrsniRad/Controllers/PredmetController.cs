using GradskiMuzejSubotica_ZavrsniRad.Models;
using GradskiMuzejSubotica_ZavrsniRad.Models.EFRepository;
using GradskiMuzejSubotica_ZavrsniRad.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GradskiMuzejSubotica_ZavrsniRad.Controllers
{
	public class PredmetController : Controller
    {
        private readonly PredmetRepository predmetRepository;
        private readonly OdeljenjeRepository odeljenjeRepository;
        private readonly ZbirkaRepository zbirkaRepository;
        private readonly StrucnjakRepository strucnjakRepository;
        private readonly ZahtevRestauracijaRepository zahtevRestauracija;

        public PredmetController()
        {
            predmetRepository = new PredmetRepository();
            odeljenjeRepository = new OdeljenjeRepository();
            zbirkaRepository = new ZbirkaRepository();
            strucnjakRepository = new();
            zahtevRestauracija = new();

		}
        private List<OdeljenjeBO> ListOdeljenja()
        {
            return (List<OdeljenjeBO>)odeljenjeRepository.GetAllOdelj();
        }
        private List<ZbirkaBO> ListZbirkeByOdelj(int odelj)
        {
            return (List<ZbirkaBO>)zbirkaRepository.GetZbirkaByOdelj(odelj);
        }
        private static PredmetViewModel PredmetToPredmetView(PredmetBO predmet, int korisnik)
        {
            PredmetViewModel model = new();
            model.Id = predmet.Id;
            model.Materijal = predmet.Materijal;
            model.Mesto = predmet.Mesto;
            model.Name = predmet.Name;
            model.SifKorisnik = korisnik;
            model.Stil = predmet.Stil;
            model.Tehnika = predmet.Tehnika;
            model.Vreme = predmet.Vreme;
            model.ZbirkaId = predmet.ZbirkaId;
            model.BrDelova = predmet.BrDelova;
            model.Dimenzije = predmet.Dimenzije;
            model.Autor = predmet.Autor;

            return model;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SetPredmetId()
        {
            ViewBag.Odeljenja = ListOdeljenja();
            return View();
        }
        [HttpPost]
        public IActionResult SetPredmetId(SetPredmetIdViewModel setPredmetIdViewModel)
        {
            if (ModelState.IsValid)
            {
                if (setPredmetIdViewModel.Odelj > 0)
                {
                    if (predmetRepository.GetPredmetById(setPredmetIdViewModel.Id) == null)
                    {
                        return RedirectToAction("AddPredmet", "Predmet",
                        new { id = setPredmetIdViewModel.Id,
                            odelj = setPredmetIdViewModel.Odelj });
                    }
                    else
                    {
                        ViewData["IdError"] = "Ovaj predmet već postoji";
                        ViewBag.Odeljenja = ListOdeljenja();
                        return View(setPredmetIdViewModel);
                    }

                }
                else
                {
                    ViewData["OdeljError"] = "Izaberite odeljenje";
                    ViewBag.Odeljenja = ListOdeljenja();
                    return View(setPredmetIdViewModel);
                }

            }
            ViewBag.Odeljenja = ListOdeljenja();
            return View(setPredmetIdViewModel);
        }
        [HttpGet]
        public IActionResult AddPredmet(string id, string odelj)
        {
            int sifOdelj = Convert.ToInt32(odelj);
            ViewData["InvBr"] = Convert.ToInt32(id);
            ViewData["SifOdelj"] = sifOdelj;
            ViewBag.Zbirke = ListZbirkeByOdelj(sifOdelj);
            return View();
        }

        [HttpPost]
        public IActionResult AddPredmet(PredmetViewModel predmetViewModel)
        {
            if (ModelState.IsValid)
            {
                if (predmetViewModel.ZbirkaId >= 0)
                {
                    try
                    {
						PredmetBO predmet = new PredmetBO
						{
							Id = predmetViewModel.Id,
							Name = predmetViewModel.Name,
							ZbirkaId = predmetViewModel.ZbirkaId,
							Autor = predmetViewModel.Autor,
							Materijal = predmetViewModel.Materijal,
							Mesto = predmetViewModel.Mesto,
							Vreme = predmetViewModel.Vreme,
							Stil = predmetViewModel.Stil,
							Tehnika = predmetViewModel.Tehnika,
							Dimenzije = predmetViewModel.Dimenzije,
							BrDelova = predmetViewModel.BrDelova
						};
						predmetRepository.AddPredmet(predmet);

						TempData["Success"] = "Uspešno ste uneli predmet!";

						return RedirectToAction("Strucnjak", "Home");
					}
                    catch (Exception e)
                    {
						TempData["Error"] = "Greška prilikom unosa predmeta.\n" + e.ToString();
						return RedirectToAction("Strucnjak", "Home");
					}
                    
                }
            }
            ViewData["InvBr"] = predmetViewModel.Id;
            ViewData["SifOdelj"] = predmetViewModel.OdeljenjeId;
            ViewBag.Zbirke = ListZbirkeByOdelj(predmetViewModel.OdeljenjeId);
            return View();
        }
        [HttpGet]
        public IActionResult SearchForListPredmet(string poruka)
        {
            ViewData["SearchError"] = poruka;
            return View();
        }

        [HttpPost]
        public IActionResult SearchForListPredmet(SearchPredmetViewModel model)
        {

            if (ModelState.IsValid)
            {
                return RedirectToAction("ListPredmet", "Predmet", new { type = model.SearchType, name = model.SearchName });
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult ListPredmet(string type, string name)
        {
            int searchType = Convert.ToInt32(type);
            IEnumerable<PredmetBO> predmeti = new List<PredmetBO>();
            if (name != null)
            {
                if (searchType == 1)
                {
                    predmeti = predmetRepository.GetPredmetsById(Convert.ToInt32(name));
                }
                else if (searchType == 2)
                {
                    predmeti = predmetRepository.GetPredmetsByZbirka(Convert.ToInt32(name));
                }
                else if (searchType == 3)
                {
                    predmeti = predmetRepository.GetPredmetsByName(name);
                }
            }
            else
            {
                predmeti = predmetRepository.GetAllPredmet();
            }


            if (predmeti != null)
            {
                ViewData["KorisnikId"] = HttpContext.Session.GetInt32("korisnikId");
                return View(predmeti);
            }
            else
            {
                return RedirectToAction("SearchForListPredmet", "Predmet",
                    new { poruka = "Predmet sa ovim podacima ne postoji." });
            }

        }

        [HttpPost]
        public IActionResult ListPredmet(PredmetBO model)
        {
            return View(model);
        }

        [HttpGet]
        public IActionResult DetailsPredmet(string id)
        {
            int invBr = Convert.ToInt32(id);
            PredmetBO predmet = predmetRepository.GetPredmetById(invBr);
            return View(predmet);
        }
        [HttpPost]
        public IActionResult DetailsPredmet(PredmetBO predmet)
        {
            return View(predmet);
        }
        [HttpGet]
        public IActionResult SearchPredmet(string choose, string korisnik)
        {
            ViewData["Action"] = choose;
            ViewData["Korsinik"] = korisnik;

            return View();
        }
        [HttpPost]
        public IActionResult SearchPredmet(SearchPredmetViewModel model)
        {
            int choose = model.SearchType;
            int? korisnik = model.Korisnik;
            PredmetBO predmet = predmetRepository.GetPredmetById(Convert.ToInt32(model.SearchName));

            if (ModelState.IsValid)
            {
                if (predmet != null)
                {
                    if (choose == 2)
                    {
                        return RedirectToAction("DeletePredmet", "Predmet", new { id = model.SearchName });
                    }
                    else if (choose == 1)
                    {
                        return RedirectToAction("EditPredmet", "Predmet", new { id = model.SearchName });
                    }
                    else if (choose == 3)
                    {
                        ZahtevRestauracijaBO zahtev = zahtevRestauracija.GetZahtevByPredmet(Convert.ToInt32(model.SearchName));
                        if(zahtev==null)
                        {
                            return RedirectToAction("ZahtevRestauracija", "Predmet", new { id = model.SearchName, korisnik = korisnik });
                        }
                        else
                        {
                            if(zahtev.StatusId == 2 || zahtev.StatusId == 4)
                            {
                                return RedirectToAction("ZahtevRestauracija", "Predmet", new { id = model.SearchName, korisnik = korisnik });
                            }
                            else
                            {
                                ViewData["ErrorSearch"] = "Ovaj predmet je na obradi ili je vec obradjen.";
                            }
                            
                        }
                        
                    }
                }
                else
                {
                    ViewData["Action"] = choose;
                    ViewData["ErrorSearch"] = "Ovaj predmet ne postoji.";
                    return View(model);
                }

            }
            ViewData["Action"] = choose;
            return View(model);
        }
        [HttpGet]
        public IActionResult DeletePredmet(string id)
        {
            return View(predmetRepository.GetPredmetById(Convert.ToInt32(id)));
        }

        [HttpPost]
        public IActionResult DeletePredmet(PredmetBO predmet)
        {
            try
            {
				predmetRepository.RemovePredmet(predmet);
				TempData["Success"] = "Uspešno ste izbrisali predmet!";
				return RedirectToAction("Strucnjak", "Home");
			}
            catch (Exception e)
            {
				TempData["Error"] = "Greška prilikom brisanja predmeta.\n" + e.ToString();
				return RedirectToAction("Strucnjak", "Home");
			}
            
        }

        [HttpGet]
        public IActionResult EditPredmet(string id)
        {
            return View(predmetRepository.GetPredmetById(Convert.ToInt32(id)));
        }
        [HttpPost]
        public IActionResult EditPredmet(PredmetBO predmet)
        {
            if(ModelState.IsValid)
            {
                try
                {
					predmetRepository.EditPredmet(predmet);
					TempData["Success"] = "Uspešno ste promenili podatke predmeta!";
					return RedirectToAction("Strucnjak", "Home");
				}
                catch (Exception e)
                {
					TempData["Error"] = "Greška prilikom promene podataka predmeta.\n" + e.ToString();
					return RedirectToAction("Strucnjak", "Home");
				}
                
            }
            return View(predmet);
        }
        [HttpGet]
        public IActionResult ZahtevRestauracija(string id, string korisnik)
        {
            PredmetBO predmet = predmetRepository.GetPredmetById(Convert.ToInt32(id));
            PredmetViewModel model = PredmetToPredmetView(predmet, Convert.ToInt32(korisnik));
            return View(model);
        }
        [HttpPost]
        public IActionResult ZahtevRestauracija(PredmetViewModel model)
        {
            try
            {
                StrucnjakBO strucnjak = strucnjakRepository.GetStrucnjakByKorisnik(model.SifKorisnik);
                zahtevRestauracija.SendRestauracijaZahtev(model.Id, strucnjak.Id);
                TempData["Success"] = "Uspešno ste poslali zahtev!";
                return RedirectToAction("Strucnjak", "Home");
            }
            catch(Exception e)
            {
				TempData["Error"] = "Greška prilikom slanja zahteva za restauraciju.\n" + e.ToString();
				return RedirectToAction("Strucnjak", "Home");
			}
            
        }
    }
}
