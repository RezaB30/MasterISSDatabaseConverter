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
    
    public partial class NASVerticalIPMap
    {
        public long ID { get; set; }
        public int NASID { get; set; }
        public string LocalIPStart { get; set; }
        public string LocalIPEnd { get; set; }
        public string RealIPStart { get; set; }
        public string RealIPEnd { get; set; }
        public int PortCount { get; set; }
    
        public virtual NA NA { get; set; }
    }
}
