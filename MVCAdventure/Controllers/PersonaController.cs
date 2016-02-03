using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFAdventure;


namespace MVCAdventure.Controllers
{
    public class PersonaController : Controller
    {
        // GET: Persona
        public ActionResult BuscarPersona()
        {

            ViewBag.BusinessEntityID = 1;
            return View(BuscarPersona());
        }

        //TODO Metodo que devuelve los datos de una persona por su ID
        //Carga de BusinessEntityID
        public void CargarBusinessEntityID()
        {
            

        }
        
    }
}