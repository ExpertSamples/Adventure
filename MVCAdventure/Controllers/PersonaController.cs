using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using EFAdventure;
using MVCAdventure.Models;

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


        public ActionResult Modificar(int idPersona)
        {
            PersonaDatosEmpleado empleado = new PersonaDatosEmpleado();
            empleado.BusinessEntityID = idPersona;
            empleado.GroupName = GetGroupName(idPersona);
            empleado.JobTitle = GetJobTitle(idPersona);
            empleado.Name = GetName(idPersona)[0] + " " + GetName(idPersona)[1];
            empleado.PhoneNumber = GetTelefono(idPersona);
            empleado.EmailAddress = GetEmail(idPersona);
            empleado.DepartmentID = GetDepartamentoID(idPersona);

            return View("ModificarPersona", empleado);
        }

        public ActionResult AgregarPersona()
        {
            return View("AgregarPersona");
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

        private string GetGroupName(int id)
        {
            string groupname;
            using (AdventureWorks2014Entities contexto = new AdventureWorks2014Entities())
            {
                var grupo = from d in contexto.Department
                            join e in contexto.EmployeeDepartmentHistory on d.DepartmentID equals e.DepartmentID
                            where e.BusinessEntityID == id
                            select d.GroupName;
                groupname = grupo.First();
            }
            return groupname;
        }
        private string GetJobTitle(int id)
        {
            string jobtitle;
            using (AdventureWorks2014Entities contexto = new AdventureWorks2014Entities())
            {
                var job = from e in contexto.Employee
                          where e.BusinessEntityID == id
                          select e.JobTitle;
                jobtitle = job.First();
            }
            return jobtitle;
        }

        private List<string> GetName(int id)
        {
            List<string> names = new List<string>();
            using (AdventureWorks2014Entities contexto = new AdventureWorks2014Entities())
            {
                var nombres = from p in contexto.Person
                              where p.BusinessEntityID == id
                              select p.FirstName;

                names.Add(nombres.First());
                
                var apellido = from p in contexto.Person
                               where p.BusinessEntityID == id
                               select p.LastName;

                names.Add(apellido.First());
            }
            return names;
        }
        private string GetTelefono(int id)
        {
            string telefono;
            using (AdventureWorks2014Entities contexto = new AdventureWorks2014Entities())
            {
                var tele=from p in contexto.PersonPhone
                         where p.BusinessEntityID == id
                         select p.PhoneNumber;
                telefono = tele.First();
            }

            return telefono;
        }
        private string GetEmail(int id)
        {
            string email;
            using (AdventureWorks2014Entities contexto = new AdventureWorks2014Entities())
            {
                var correo=from p in contexto.EmailAddress
                           where p.BusinessEntityID == id
                           select p.EmailAddress1;
                email = correo.First();
            }
            return email;

        }

        private int GetDepartamentoID(int id)
        {

            int departamentoId;
            using (AdventureWorks2014Entities contexto = new AdventureWorks2014Entities())
            {
                var depID = from e in contexto.EmployeeDepartmentHistory
                         where e.BusinessEntityID == id
                         select e.DepartmentID;
                departamentoId = depID.First();

            }
            return departamentoId;
         }

        }
    }
}