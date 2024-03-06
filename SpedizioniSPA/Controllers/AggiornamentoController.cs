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

        [HttpPost]
        public ActionResult EditAggiornamento(Aggiornamenti A, int idSpedizione)
        {
            try
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
            catch
            {
                return View();
            }

        }

    }
}