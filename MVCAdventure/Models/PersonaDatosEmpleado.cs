﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCAdventure.Models
{
    public class PersonaDatosEmpleado
    {
        public int BusinessEntityID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string JobTitle { get; set; }
        public string GroupName { get; set; }
        public int DepartmentID { get; set; }

    }
}