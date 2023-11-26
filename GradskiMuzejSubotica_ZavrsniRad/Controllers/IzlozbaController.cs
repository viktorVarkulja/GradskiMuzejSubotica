using GradskiMuzejSubotica_ZavrsniRad.Models;
using GradskiMuzejSubotica_ZavrsniRad.Models.EFRepository;
using GradskiMuzejSubotica_ZavrsniRad.Models.Interfaces;
using GradskiMuzejSubotica_ZavrsniRad.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GradskiMuzejSubotica_ZavrsniRad.Controllers
{
    public class IzlozbaController : Controller
    {
        private readonly IzlozbaRepository izlozbaRepository;
        private readonly IzlozbenoRepository izlozbenoRepository;
        private readonly PredmetRepository predmetRepository;
        public IzlozbaController()
        {
            izlozbaRepository = new IzlozbaRepository();
            izlozbenoRepository = new IzlozbenoRepository();
            predmetRepository = new PredmetRepository();
        }
        private List<PredmetBO> SlobodniPredmeti()
        {
            List<PredmetBO> predmetSlobodni = new List<PredmetBO>();
            List<PredmetBO> sviPredmeti = (List<PredmetBO>)predmetRepository.GetAllPredmet();
            List<PredmetBO> izlozbeniPredmet = (List<PredmetBO>)izlozbenoRepository.GetAllIzlozbenoPredmet();
            foreach (PredmetBO p in sviPredmeti)
            {
                if (!izlozbeniPredmet.Exists(x => x.Id == p.Id))
                {
                    predmetSlobodni.Add(p);
                }
            }

            return predmetSlobodni;
        }
        public IActionResult Index()
        {
            return View();
        }
		public IActionResult SetIdIzlozba()
        {
            return View();
        }
        [HttpPost]
		public IActionResult SetIdIzlozba(SetIzlozbaIdViewModel model)
        {
            if(ModelState.IsValid)
            {
                if (izlozbaRepository.GetIzlozbaById(model.Id) == null)
                {
                    return RedirectToAction("AddIzlozba", "Izlozba", new { id = model.Id });
                }
                else
                {
                    ViewData["IdError"] = "Ova izložba već postoji.";
                    return View(model);
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult AddIzlozba(string id)
        {
            ViewData["SifIzlozba"] = id;
            ViewBag.Predmeti = SlobodniPredmeti();
            return View();
        }
        [HttpPost]
        public IActionResult AddIzlozba(IzlozbaBO izlozbaBO, string[] predmeti)
        {
            if(ModelState.IsValid)
            {
				if (predmeti.Length > 0)
                {
                    try
                    {
                        izlozbaRepository.AddIzlozba(izlozbaBO);
                        foreach (string s in predmeti)
                        {
                            IzlozbenoBO izlozbeno = new IzlozbenoBO();
                            izlozbeno.IzlozbaID = izlozbaBO.ID;
                            izlozbeno.PredmetID = Convert.ToInt32(s);

                            izlozbenoRepository.AddIzlozbeno(izlozbeno);
                        }
                        TempData["Success"] = "Uspešno ste uneli izložbu!";
                        return RedirectToAction("Strucnjak", "Home");
                    }
                    catch(Exception e)
                    {
                        TempData["Error"] = "Greška prilikom unosa izložbe.\n" + e.ToString();
						return RedirectToAction("Strucnjak", "Home");
					}
                    
                }
                else
                {
                    ViewData["ErrorPredmet"] = "Morate da izaberete barem jedan predmet!";
                }
            }
            ViewBag.Predmeti = SlobodniPredmeti();
            ViewData["SifIzlozba"] = izlozbaBO.ID;
            return View();
        }
        [HttpGet]
        public IActionResult GetPredmetByOdeljenje(int id, string[] predmeti)
        {
            ViewBag.Checked = predmeti;
            if (id == 0)
            {
                return PartialView("GetPredmetByOdeljenje", SlobodniPredmeti());
            }

			List<PredmetBO> predmetiBO = new List<PredmetBO>();
            foreach(PredmetBO p in predmetRepository.GetPredmetsByOdeljenje(id))
            {
                if(SlobodniPredmeti().Exists(x=>x.Id == p.Id))
                {
                    predmetiBO.Add(p);
                }
            }
            return PartialView("GetPredmetByOdeljenje", predmetiBO);

        }
        [HttpGet]
        public IActionResult SearchForListIzlozba(string poruka)
        {
            ViewData["SearchError"] = poruka;
            return View();
        }
        [HttpPost]
        public IActionResult SearchForListIzlozba(SearchPredmetViewModel model)
        {
            if(ModelState.IsValid)
            {
                return RedirectToAction("ListIzlozba", "Izlozba", new { type = model.SearchType, name = model.SearchName });
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult ListIzlozba(string type, string name)
        {
            int searchType = Convert.ToInt32(type);
            IEnumerable<IzlozbaBO> izlozbe = new List<IzlozbaBO>();
            if (name != null)
            {
                if (searchType == 1)
                {
                    izlozbe = izlozbaRepository.GetIzlozbasById(Convert.ToInt32(name));
                }
                else if (searchType == 2)
                {
                    izlozbe = izlozbaRepository.GetIzlozbasByName(name);
                }
            }
            else
            {
                izlozbe = izlozbaRepository.GetAllIzlozba();
            }


            if (izlozbe != null)
            {
                return View(izlozbe);
            }
            else
            {
                return RedirectToAction("SearchForListIzlozba", "Izlozba",
                    new { poruka = "Izlozba sa ovim podacima ne postoji." });
            }
        }
        [HttpPost]
        public IActionResult ListIzlozba(IzlozbaBO izlozba)
        {
            return View(izlozba);
        }
        [HttpGet]
        public IActionResult DetailsIzlozba(string id)
        {
            int sifIzl = Convert.ToInt32(id);
            IzlozbaBO izlozba = izlozbaRepository.GetIzlozbaById(sifIzl);
            return View(izlozba);
        }
        [HttpPost]
        public IActionResult DetailsIzlozba(IzlozbaBO izlozba)
        {
            return View(izlozba);
        }
        [HttpGet]
        public IActionResult SearchIzlozba(string choose)
        {
            ViewData["Action"] = choose;
            return View();
        }
        [HttpPost]
        public IActionResult SearchIzlozba(SearchPredmetViewModel model)
        {
            int choose = model.SearchType;
            IzlozbaBO izlozba = izlozbaRepository.GetIzlozbaById(Convert.ToInt32(model.SearchName));

            if (ModelState.IsValid)
            {
                if (izlozba != null)
                {
                    if (choose == 2)
                    {
                        return RedirectToAction("DeleteIzlozba", "Izlozba", new { id = model.SearchName });
                    }
                    else if (choose == 1)
                    {
                        return RedirectToAction("EditIzlozba", "Izlozba", new { id = model.SearchName });
                    }
                }
                else
                {
                    ViewData["Action"] = choose;
                    ViewData["ErrorSearch"] = "Ova izlozba ne postoji.";
                    return View(model);
                }

            }
            ViewData["Action"] = choose;
            return View(model);
        }
        [HttpGet]
        public IActionResult DeleteIzlozba(string id)
        {
            IzlozbaBO izlozba = izlozbaRepository.GetIzlozbaById(Convert.ToInt32(id));
            return View(izlozba);
        }
        [HttpPost]
        public IActionResult DeleteIzlozba(IzlozbaBO izlozba)
        {
            try
            {
				izlozbenoRepository.RemoveIzlozbeno(izlozba.ID);
				izlozbaRepository.RemoveIzlozba(izlozba);
				TempData["Success"] = "Uspešno ste izbrisali izložbu!";
				return RedirectToAction("Strucnjak", "Home");
			}
            catch (Exception e)
            {
				TempData["Error"] = "Greška prilikom brisanja izložbe.\n" + e.ToString();
				return RedirectToAction("Strucnjak", "Home");
			}  
            
        }
        [HttpGet]
        public IActionResult EditIzlozba(string id)
        {
            IzlozbaBO izlozba = izlozbaRepository.GetIzlozbaById(Convert.ToInt32(id));
            ViewBag.SlobodniPredmeti = SlobodniPredmeti();
            ViewBag.Izlozbeni = izlozbenoRepository.GetIzlozbenosByIzloba(izlozba.ID);
            return View(izlozba);
        }
        [HttpPost]
        public IActionResult EditIzlozba(IzlozbaBO izlozbaBO, string[] predmeti)
        {
            if (ModelState.IsValid)
            {

                if (predmeti.Length > 0)
                {
                    try
                    {
						izlozbaRepository.EditIZlozba(izlozbaBO);
						izlozbenoRepository.RemoveIzlozbeno(izlozbaBO.ID);
						foreach (string s in predmeti)
						{
							IzlozbenoBO izlozbeno = new IzlozbenoBO();
							izlozbeno.IzlozbaID = izlozbaBO.ID;
							izlozbeno.PredmetID = Convert.ToInt32(s);

							izlozbenoRepository.AddIzlozbeno(izlozbeno);
						}
						TempData["Success"] = "Uspešno ste promenili podatke izložbe!";
						return RedirectToAction("Strucnjak", "Home");
					}
                    catch (Exception e)
                    {
						TempData["Error"] = "Greška prilikom promene podataka izložbe.\n" + e.ToString();
						return RedirectToAction("Strucnjak", "Home");
					}
                    
                }
                else
                {
                    ViewData["ErrorPredmet"] = "Morate da izaberete barem jedan predmet!";
                }
            }
            ViewBag.SlobodniPredmeti = SlobodniPredmeti();
            ViewBag.Izlozbeni = izlozbenoRepository.GetIzlozbenosByIzloba(izlozbaBO.ID);
            return View(izlozbaBO);
        }
    }
}
