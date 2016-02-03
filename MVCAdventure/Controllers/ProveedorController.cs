using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFAdventure;

namespace MVCAdventure.Controllers
{
    public class ProveedorController : Controller
    {
        // GET: Proveedor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetProveedores()
        {
            List<Person> ListaProveedores;
            AdventureWorks2014Entities contexto = new AdventureWorks2014Entities();

            var proveedores = (from prov in contexto.SalesPerson
                               join per in contexto.Person on prov.BusinessEntityID equals per.BusinessEntityID into lista
                               from ListaProv in lista
                               select ListaProv);

            ListaProveedores = proveedores.ToList();
            contexto.Dispose();

            return View("Proveedor", ListaProveedores);
        }

        public ActionResult GetProductos()
        {

            return PartialView("_Productos");
        }
    }
}