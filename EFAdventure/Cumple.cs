//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EFAdventure
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cumple
    {
        public int EmailID { get; set; }
        public int BusinessEntityID { get; set; }
        public string Asunto { get; set; }
        public string Texto { get; set; }
    
        public virtual Employee Employee { get; set; }
    }
}
