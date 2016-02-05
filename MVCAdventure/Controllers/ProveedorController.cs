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
            foreach (int id in ListaID)
            {
                var producto = (from product in contexto.Product
                                where product.ProductID == id
                                select product).First();
                ListaProductos.Add(producto);
            }

            contexto.Dispose();
            ViewBag.ID = provID;

            return PartialView("_Producto", ListaProductos);
        }

        public ActionResult ModificarProveedor(int negocio)
        {
            Person persona;
            SalesPerson salesPerson;
            AdventureWorks2014Entities contexto = new AdventureWorks2014Entities();
            var proveedor = (from prov in contexto.Person
                             where prov.BusinessEntityID == negocio
                             select prov).First();
            persona = (Person)proveedor;

            var salesP = from prov in contexto.SalesPerson
                         where prov.BusinessEntityID == negocio
                         select prov;
            salesPerson = salesP.First();

            contexto.Dispose();

            ViewBag.salesPerson = salesPerson;
            return View("ModificarProveedor", persona);
        }

        public ActionResult ModificarProducto(int productID)
        {
            Product producto;
            AdventureWorks2014Entities contexto = new AdventureWorks2014Entities();

            var pro = (from productos in contexto.Product
                       where productos.ProductID == productID
                       select productos).First();

            producto = (Product)pro;
            contexto.Dispose();

            return View("ModificarProducto", producto);
        }
        
        [HttpPost]
        public ActionResult UpdateProveedor()
        {
            Person persona;
            int id = int.Parse(HttpContext.Request.Params["BusinessEntityID"]);

            using (AdventureWorks2014Entities contexto = new AdventureWorks2014Entities())
            {
                var proveedor = from prov in contexto.Person
                                where prov.BusinessEntityID == id
                                select prov;
                persona = proveedor.First();

                persona.PersonType = HttpContext.Request.Params["PersonType"];
                persona.NameStyle = bool.Parse(HttpContext.Request.Params["NameStyle"]);
                persona.Title = HttpContext.Request.Params["Title"];
                persona.FirstName = HttpContext.Request.Params["FirstName"];
                persona.LastName = HttpContext.Request.Params["LastName"];
                persona.MiddleName = HttpContext.Request.Params["MiddleName"];
                persona.Suffix = HttpContext.Request.Params["Suffix"];
                persona.EmailPromotion = int.Parse(HttpContext.Request.Params["EmailPromotion"]);
                persona.AdditionalContactInfo = HttpContext.Request.Params["AdditionalContactInfo"];
                persona.ModifiedDate = DateTime.Now;

                try
                {
                    contexto.SaveChanges();
                    ViewBag.error = false;
                }
                catch(Exception e)
                {
                    ViewBag.error = true;
                    persona = (from prov in contexto.Person
                              where prov.BusinessEntityID == id
                              select prov).First();
                }
            }

            return View("ModificarProveedor", persona);
        }



    }
}