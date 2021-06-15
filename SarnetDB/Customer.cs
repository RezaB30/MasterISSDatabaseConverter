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
    
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            this.CustomerAdditionalPhoneNoes = new HashSet<CustomerAdditionalPhoneNo>();
            this.Subscriptions = new HashSet<Subscription>();
            this.SystemLogs = new HashSet<SystemLog>();
        }
    
        public long ID { get; set; }
        public short CustomerType { get; set; }
        public long AddressID { get; set; }
        public string ContactPhoneNo { get; set; }
        public string Culture { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FathersName { get; set; }
        public string MothersName { get; set; }
        public string MothersMaidenName { get; set; }
        public short Sex { get; set; }
        public System.DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public int Profession { get; set; }
        public int Nationality { get; set; }
        public long BillingAddressID { get; set; }
    
        public virtual Address Address { get; set; }
        public virtual Address Address1 { get; set; }
        public virtual CorporateCustomerInfo CorporateCustomerInfo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAdditionalPhoneNo> CustomerAdditionalPhoneNoes { get; set; }
        public virtual CustomerIDCard CustomerIDCard { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Subscription> Subscriptions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SystemLog> SystemLogs { get; set; }
    }
}
