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
    
    public partial class ExternalPayment
    {
        public long BillID { get; set; }
        public Nullable<int> ExternalUserID { get; set; }
        public Nullable<int> OfflineUserID { get; set; }
    
        public virtual Bill Bill { get; set; }
        public virtual RadiusRBillingService RadiusRBillingService { get; set; }
        public virtual OfflinePaymentGateway OfflinePaymentGateway { get; set; }
    }
}