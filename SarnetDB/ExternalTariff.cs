//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SarnetDB
{
    using System;
    using System.Collections.Generic;
    
    public partial class ExternalTariff
    {
        public int TariffID { get; set; }
        public int DomainID { get; set; }
        public string DisplayName { get; set; }
        public bool HasXDSL { get; set; }
        public bool HasFiber { get; set; }
    
        public virtual Domain Domain { get; set; }
        public virtual Service Service { get; set; }
    }
}