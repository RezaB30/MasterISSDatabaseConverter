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
    
    public partial class CorporateCustomerInfo
    {
        public long CustomerID { get; set; }
        public string Title { get; set; }
        public string TaxNo { get; set; }
        public string TaxOffice { get; set; }
        public string CentralSystemNo { get; set; }
        public string TradeRegistrationNo { get; set; }
        public long CompanyAddressID { get; set; }
    
        public virtual Address Address { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
