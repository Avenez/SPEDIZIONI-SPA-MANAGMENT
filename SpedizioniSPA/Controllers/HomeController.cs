using SpedizioniSPA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult CercaSpedizione()
        {
            TempData["SrcData"] = 0;
            List<Aggiornamenti> ListAggiornamentiSrc = new List<Aggiornamenti>();
            ViewBag.ListAggiornamentiSrc = ListAggiornamentiSrc;

            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CercaSpedizione(int idSpedizione)
        {
            if (idSpedizione != 0)
            {
                // Effettua la ricerca della spedizione
                List<Aggiornamenti> ListAggiornamentiSrc = Aggiornamenti.GetListaAggiornamenti(idSpedizione);

                // Controlla se sono stati trovati aggiornamenti per la spedizione
                if (ListAggiornamentiSrc.Count > 0)
                {
                    // Se sono stati trovati aggiornamenti, memorizzali nella ViewBag per visualizzarli nella vista
                    ViewBag.ListAggiornamentiSrc = ListAggiornamentiSrc;
                }
                else
                {
                    // Se non sono stati trovati aggiornamenti, visualizza un messaggio di avviso
                    ViewBag.ErrorMessage = "Nessun aggiornamento trovato per la spedizione con ID: " + idSpedizione;
                }

                // Memorizza l'ID della spedizione nella TempData per eventuali utilizzi futuri
                TempData["SrcData"] = idSpedizione;
            }
            else
            {
                // Se l'ID della spedizione è 0, mostra un messaggio di errore
                ViewBag.ErrorMessage = "Inserisci un ID di spedizione valido";
            }

            // Ritorna la vista
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Statistics() 
        {

            int consegneNonEffettuate = Spedizione.GetNumSpedNonConsegnate();
            ViewBag.consegneNonEffettuate = consegneNonEffettuate;

            List<Tuple<string, int>> spedizioniPerCittaList = Spedizione.GetSpedizioniPerCitta();
            ViewBag.SpedizioniPerCittaList = spedizioniPerCittaList;

            List<Spedizione>SpedizioniInConsegna = Spedizione.GetSpedizioniInConsegna();
            ViewBag.SpedizioniInConsegna = SpedizioniInConsegna;



            return View();
        }

    }
}