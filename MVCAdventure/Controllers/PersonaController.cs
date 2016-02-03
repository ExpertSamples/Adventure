using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using EFAdventure;


namespace MVCAdventure.Controllers
{
    public class PersonaController : Controller
    {
        // GET: Persona
       
        public ActionResult BuscarPersona()
        {
            ViewBag.BusinessEntityID = 1;
            return View("Modificar");
        }

        //TODO Metodo que devuelve los datos de una persona por su ID

        //Carga de BusinessEntityID
        public void CargarBusinessEntityID()
        {
            using (AdventureWorks2014Entities contexto = new AdventureWorks2014Entities())
            {
                contexto.BusinessEntity.Select(p => p.BusinessEntityID);
            }
        }
        
    }
}