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
    
    public partial class SubscriptionTariffHistory
    {
        public long ID { get; set; }
        public long SubscriptionID { get; set; }
        public int OldTariffID { get; set; }
        public int NewTariffID { get; set; }
        public System.DateTime Date { get; set; }
    
        public virtual Service Service { get; set; }
        public virtual Service Service1 { get; set; }
        public virtual Subscription Subscription { get; set; }
    }
}
