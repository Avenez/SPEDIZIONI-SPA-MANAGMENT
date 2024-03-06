using SpedizioniSPA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpedizioniSPA.Controllers
{
    public class AggiornamentoController : Controller
    {
        // GET: Aggiornamento
        public ActionResult Index()
        {
            return View();
        }


        //Recupero la lista degli aggiornamenti in funzione dell'idSpedizione
        [HttpGet]
        public ActionResult EditAggiornamento(int idSpedizione)
        {
            try 
            {
                List<Aggiornamenti> ListAggiornamenti = Aggiornamenti.GetListaAggiornamenti(idSpedizione);
                ViewBag.ListAggiornamenti = ListAggiornamenti;
            } 
            catch (Exception ex)
            { 
            System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            
        
        return View();
        }

        //Inserisco un nuvo aggiornamento passando l'oggetto e l'idSpedizione
        [HttpPost]
        public ActionResult EditAggiornamento(Aggiornamenti A, int idSpedizione)
        {

                if (ModelState.IsValid)
                {
                    Aggiornamenti.InserisciNuovoAggiornamento(A, idSpedizione);

                    return RedirectToAction("EditAggiornamento", new { idSpedizione = idSpedizione });

                }
                else
                {
                    return View(A);
                }


        }



    }
}