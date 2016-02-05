using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFAdventure;
using MVCAdventure.Models;

namespace MVCAdventure.Controllers
{
    public class VentasController : Controller
    {
        // GET: Ventas
         public ActionResult Index()
        {
           
            return View();
        }

        public ActionResult RevisarPedido(int id)
        {
            Person cliente = new Person();
            cliente = GetCliente(id);
            ViewBag.cliente = cliente.FirstName + " " + cliente.LastName;
            Person vendedor = new Person();
            vendedor = GetVendedor(id);
            ViewBag.vendedor = vendedor.FirstName + " " + vendedor.LastName;
            ViewBag.fecha = GetFecha(id);
            return PartialView("_DetallesPedido", GetProducto(id));
        }

        public ActionResult FiltroPedidos(string name, string lastName, DateTime inicio, DateTime final)
        {
            List<int> listaID = new List<int>();
            using (AdventureWorks2014Entities contexto = new AdventureWorks2014Entities())
            {
                var id = (from vendedor in contexto.Person
                          where (vendedor.FirstName == name && vendedor.LastName == lastName)
                          select vendedor.BusinessEntityID).First();

                var listaProductos = (from sales in contexto.SalesOrderHeader
                                      where (sales.SalesPersonID == id && sales.OrderDate >= inicio && sales.OrderDate <= final)
                                      select sales.SalesOrderID);
                listaID = listaProductos.ToList();
            }
            return PartialView("Index", listaID);
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

        private Person GetVendedor(int id)
        {
            Person vendedor = null;
            using (AdventureWorks2014Entities contexto = new AdventureWorks2014Entities())
            {
                var filtro = from ventas in contexto.SalesOrderHeader
                             join vendor in contexto.SalesPerson on ventas.SalesPersonID equals vendor.BusinessEntityID
                             join personas in contexto.Person on vendor.BusinessEntityID equals personas.BusinessEntityID
                             where ventas.SalesOrderID == id
                             select personas;
                vendedor = filtro.FirstOrDefault();
            }
            return vendedor;
        }


        private List<VentasDetallesModel> GetProducto(int id)
        {
            List<VentasDetallesModel> productos = new List<VentasDetallesModel>();
            using (AdventureWorks2014Entities contexto = new AdventureWorks2014Entities())
            {
                var filtro = from ventas in contexto.SalesOrderHeader
                             join detalles in contexto.SalesOrderDetail on ventas.SalesOrderID equals detalles.SalesOrderID
                             join producto in contexto.Product on detalles.ProductID equals producto.ProductID 
                             where ventas.SalesOrderID == id
                             select new { id=producto.ProductID , nombre=producto.Name , unidades=detalles.OrderQty , PrecioUnidad=detalles.UnitPrice  , PrecioTotal=detalles.LineTotal   };
                foreach (var item in filtro)
                {
                    VentasDetallesModel pro = new VentasDetallesModel();
                    pro.ProductID = item.id;
                    pro.Name = item.nombre;
                    pro.OrderQty = item.unidades;
                    pro.UnitPrice = item.PrecioUnidad;
                    pro.LineTotal = item.PrecioTotal;

                    productos.Add(pro);

                }
            }
            return productos;
           
        }

        private DateTime GetFecha(int id)
        {
            DateTime fecha = new DateTime();
            using (AdventureWorks2014Entities contexto = new AdventureWorks2014Entities())
            {
                var filtro = from ventas in contexto.SalesOrderHeader
                             where ventas.SalesOrderID == id
                             select ventas.OrderDate;
                fecha = filtro.FirstOrDefault();
            }

            return fecha;

        }


    }
}

