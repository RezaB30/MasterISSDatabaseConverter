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
    
    public partial class PartnerRegisteredSubscription
    {
        public long SubscriptionID { get; set; }
        public int PartnerID { get; set; }
        public int TariffID { get; set; }
        public decimal Allowance { get; set; }
        public Nullable<long> PartnerCollectionID { get; set; }
        public short AllowanceState { get; set; }
    
        public virtual Partner Partner { get; set; }
        public virtual PartnerCollection PartnerCollection { get; set; }
        public virtual Service Service { get; set; }
        public virtual Subscription Subscription { get; set; }
    }
}