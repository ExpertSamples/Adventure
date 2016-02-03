using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFAdventure;

namespace MVCAdventure.Controllers
{
    public class VentasController : Controller
    {
        // GET: Ventas
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RevisarPedido()
        {


            return PartialView("DetallesPedido");
        }

        private Person GetCliente(int id)
        {
            Person cliente = null;
            using (AdventureWorks2014Entities contexto = new AdventureWorks2014Entities())
            {
                var filtro = from ventas in contexto.SalesOrderHeader
                             join consumidor in contexto.Customer on ventas.CustomerID equals consumidor.CustomerID
                             join persona in contexto.Person on consumidor.PersonID equals persona.BusinessEntityID
                             where ventas.SalesOrderID == id
                             select persona;
                cliente = filtro.FirstOrDefault();
            }
            return cliente;
        }


    }
}