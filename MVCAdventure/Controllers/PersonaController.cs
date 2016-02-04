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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Buscar()
        {
            return View("BuscarPersona", GetListaDepartamentos());
        }

        public ActionResult MostrarJobTitle(int depId)
        {
            ViewBag.listaJobTitle = GetListaJobTitle(depId);
            ViewBag.depId = depId;

            return View("BuscarPersona", GetListaDepartamentos());
        }

        public ActionResult MostrarPersonas(int depId, string jobTitle)
        {
            ViewBag.listaJobTitle = GetListaJobTitle(depId);
            ViewBag.listaPersonas = GetListaPersonas(depId, jobTitle);
            ViewBag.depId = depId;
            ViewBag.jobTitle = jobTitle;

            return View("BuscarPersona", GetListaDepartamentos());
        }

        ////////////////////////////////////
        // CONSULTAS DE ENTITY FRAMEWORK //
        //////////////////////////////////

        private List<Department> GetListaDepartamentos()
        {
            List<Department> listaDepartamentos = null;

            using(AdventureWorks2014Entities contexto = new AdventureWorks2014Entities())
            {
                var lista = from departamento in contexto.Department
                            select departamento;

                listaDepartamentos = lista.ToList();
            }

            return listaDepartamentos;
        }

        public List<String> GetListaJobTitle(int depId)
        {
            List<String> listaJobTitle = null;

            using (AdventureWorks2014Entities contexto = new AdventureWorks2014Entities())
            {
                var lista = from e in contexto.Employee
                            join h in contexto.EmployeeDepartmentHistory on e.BusinessEntityID equals h.BusinessEntityID
                            join d in contexto.Department on h.DepartmentID equals d.DepartmentID
                            where d.DepartmentID == depId
                            select e.JobTitle;

                listaJobTitle = lista.Distinct().ToList();
            }

            return listaJobTitle;
        }

        private List<Person> GetListaPersonas(int depId, string jobTitle)
        {
            List<Person> listaPersonas = null;

            using (AdventureWorks2014Entities contexto = new AdventureWorks2014Entities())
            {
                var lista = from e in contexto.Employee
                            join h in contexto.EmployeeDepartmentHistory on e.BusinessEntityID equals h.BusinessEntityID
                            join p in contexto.Person on h.BusinessEntityID equals p.BusinessEntityID
                            where h.DepartmentID == depId && e.JobTitle == jobTitle
                            select p;

                listaPersonas = lista.ToList();
            }

            return listaPersonas;
        }
        
    }
}