//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ORM_Core
{
    using System;
    using System.Collections.Generic;
    
    public partial class program_parameters
    {
        public int id { get; set; }
        public int program_id { get; set; }
        public string parameter_name { get; set; }
        public string parameter_value { get; set; }
    
        public virtual program program { get; set; }
    }
}
