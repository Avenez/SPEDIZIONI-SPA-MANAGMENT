using SpedizioniSPA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpedizioniSPA.Controllers
{
    //[Authorize]
    //[AllowAnonymous]
    public class SpedizioneController : Controller
    {
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        //Recupero le liste di Privati e Aziende per popolare la select
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateSpedizione()
        {
            try 
            {
                List<Privato> listPrivato = Privato.GetListaPrivati();
                SelectList dropListPrivati = new SelectList(listPrivato, "IdCliente", "fullIdCliente");
                ViewBag.listPrivato = dropListPrivati;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            try
            {
                List<Azienda> listAzienda = Azienda.GetListaAziende();
                SelectList dropListAziende = new SelectList(listAzienda, "IdCliente", "fullIdCliente");
                ViewBag.listAzienda = dropListAziende;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }


            return View();
        }


        //Inserisco una nuova Spedizione e un nuovo Aggiornamento e invio un feed all'utente
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateSpedizione(Spedizione S)
        {
            try 
            {
                if (ModelState.IsValid)
                {
                    Spedizione.InserisciNuovaSpedizione(S);
                    int idSpedizione = Spedizione.GetIdSpedizione(S);
                    Aggiornamenti.InserisciNuovoAggiornamentoIniziale(idSpedizione);

                    System.Diagnostics.Debug.WriteLine(S);
                    System.Diagnostics.Debug.WriteLine(idSpedizione);

                    Session["Inserimento"] = true;
                    Session["Messaggio"] = "Inserimento Spedizione avvenuto con Successo";
                    return RedirectToAction("Backoffice", "Home");

                }
                else
                {
                    return View(S);
                }
            }
            catch 
            {
                return View();
            }

        }


        



    }
}