using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using SpedizioniSPA.Models;

namespace SpedizioniSPA.Controllers
{
    public class PrivatoController : Controller
    {
        // GET: Privato
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreatePrivato()
        {

            return View();
        }


        //Inserisco una nuovo Privato e invio un feed all'utente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePrivato(Privato P)
        {

            try 
            {
                if (ModelState.IsValid)
                {
                    Privato.InserisciNuovoPrivato(P);
                    Session["Inserimento"] = true;
                    Session["Messaggio"] = "Inserimento Privato avvenuto con Successo";
                    return RedirectToAction("Backoffice", "Home");
                }
                else
                {
                    return View(P);
                }
                
            }
            catch (Exception ex)
            { 
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return View();
            }

            
        }
    }
}