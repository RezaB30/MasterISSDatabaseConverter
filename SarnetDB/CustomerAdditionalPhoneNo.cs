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
    
    public partial class CustomerAdditionalPhoneNo
    {
        public long ID { get; set; }
        public long CustomerID { get; set; }
        public string PhoneNo { get; set; }
    
        public virtual Customer Customer { get; set; }
    }
}