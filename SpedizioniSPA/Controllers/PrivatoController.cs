using System;
using System.Collections.Generic;
using System.Linq;
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


        [HttpPost]
        public ActionResult CreatePrivato(Privato P)
        {

            try 
            {
                Privato.InserisciNuovoPrivato(P);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            { 
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return View();
            }

            
        }
    }
}