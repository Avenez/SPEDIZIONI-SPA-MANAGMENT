using SpedizioniSPA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SpedizioniSPA.Controllers
{
    public class AziendaController : Controller
    {
        // GET: Azienda
        public ActionResult Index()
        {
            return View();
        }




        [HttpGet]
        public ActionResult CreateAzienda()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAzienda(Azienda A)
        {
            try 
            {
                if (ModelState.IsValid)
                {
                    Azienda.InserisciNuovaAzienda(A);

                    Session["Inserimento"] = true;
                    Session["Messaggio"] = "Inserimento Azienda avvenuto con Successo";

                    return RedirectToAction("Backoffice", "Home");
                    
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