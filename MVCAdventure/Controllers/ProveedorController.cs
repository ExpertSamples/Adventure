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

        public ActionResult GetProductos(int provID)
        {
            List<Product> ListaProductos = new List<Product>();
            List<int> ListaID = new List<int>();
            AdventureWorks2014Entities contexto = new AdventureWorks2014Entities();

            var LHeader = (from OrderHeader in contexto.SalesOrderHeader
                           where OrderHeader.SalesPersonID == provID
                           join OrderDetail in contexto.SalesOrderDetail on OrderHeader.SalesOrderID equals OrderDetail.SalesOrderID into lista
                           from listaproductos in lista
                           select listaproductos.ProductID).Distinct();

            ListaID = LHeader.ToList();
            foreach(int id in ListaID)
            {
                var producto = (from product in contexto.Product
                                 where product.ProductID == id
                                 select product).First();
                ListaProductos.Add(producto);
            }

            contexto.Dispose();
            ViewBag.ID = provID;

            return PartialView("_Productos", ListaProductos);
        }

        public ActionResult ModificarProveedor(int negocio)
        {
            Person persona;
            AdventureWorks2014Entities contexto = new AdventureWorks2014Entities();
            var proveedor = (from prov in contexto.Person
                             where prov.BusinessEntityID == negocio
                             select prov).First();
            persona = (Person)proveedor; 
            contexto.Dispose();
            return View("ModificarProveedor", persona);
        }

        

        //Post: Proveedor
        [HttpPost]
        
    }

}