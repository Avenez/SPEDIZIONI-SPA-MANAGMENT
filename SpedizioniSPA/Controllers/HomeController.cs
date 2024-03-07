using SpedizioniSPA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SpedizioniSPA.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Backoffice()
        {
            if (Session["inserimento"] == null)
            {
                Session["inserimento"] = false;
            }

            bool ins = (bool)Session["inserimento"];

            if (ins == false)
            {
                TempData["Inserimento"] = false;
            }
            else
            {
                TempData["Inserimento"] = true;
                Session["inserimento"] = false;
            }

            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult EditSpedizioni() 
        {
            try
            {
                List<Spedizione> ListSpedizione = Spedizione.GetListaSpedizioni();
                ViewBag.ListSpedizioni = ListSpedizione;
                
            }
            catch
            {
                return View("Backoffice", "Home");
            }

            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult CercaSpedizione()
        {
            TempData["SrcData"] = 0;
            List<Aggiornamenti> ListAggiornamentiSrc = new List<Aggiornamenti>();
            ViewBag.ListAggiornamentiSrc = ListAggiornamentiSrc;

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult CercaSpedizione(int idSpedizione, string idCliente)
        {
            if (idSpedizione != 0)
            {
                bool shipmentFound = Spedizione.GetSrcSpedizioni(idSpedizione, idCliente);
                
                if (shipmentFound)
                {
                    System.Diagnostics.Debug.WriteLine("Prendo la Lista");
                    List<Aggiornamenti> ListAggiornamentiSrc = Aggiornamenti.GetListaAggiornamenti(idSpedizione);
                    if (ListAggiornamentiSrc.Count > 0)
                    {
                        ViewBag.ListAggiornamentiSrc = ListAggiornamentiSrc;
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Nessun aggiornamento trovato per la spedizione con ID: " + idSpedizione;
                }

                TempData["SrcData"] = idSpedizione;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Errore per il true");
                ViewBag.ErrorMessage = "Inserisci un ID di spedizione valido";
            }

            return View();
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Statistics()
        {

            return View();

        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Statistics2()
        {
            int consegneNonEffettuate = Spedizione.GetNumSpedNonConsegnate();

            List<Tuple<string, int>> spedizioniPerCittaList = Spedizione.GetSpedizioniPerCitta();

            List<Spedizione> spedizioniInConsegna = Spedizione.GetSpedizioniInConsegna();

            var jsonData = new
            {
                consegneNonEffettuate = consegneNonEffettuate,
                spedizioniPerCittaList = spedizioniPerCittaList,
                spedizioniInConsegna = spedizioniInConsegna
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

    }
}