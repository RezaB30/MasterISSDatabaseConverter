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
    
    public partial class MobilExpressAutoPayment
    {
        public long SubscriptionID { get; set; }
        public string CardToken { get; set; }
        public short PaymentType { get; set; }
        public Nullable<System.DateTime> LastOperationTime { get; set; }
    
        public virtual Subscription Subscription { get; set; }
    }
}