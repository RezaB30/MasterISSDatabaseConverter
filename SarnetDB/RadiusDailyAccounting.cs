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
    
    public partial class RadiusDailyAccounting
    {
        public long ID { get; set; }
        public string Username { get; set; }
        public long UploadBytes { get; set; }
        public long DownloadBytes { get; set; }
        public System.DateTime Date { get; set; }
        public long SubscriptionID { get; set; }
    
        public virtual Subscription Subscription { get; set; }
    }
}