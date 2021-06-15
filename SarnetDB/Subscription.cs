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
    
    public partial class Subscription
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Subscription()
        {
            this.Bills = new HashSet<Bill>();
            this.BTKSubscriptionChanges = new HashSet<BTKSubscriptionChange>();
            this.ChangeServiceTypeTasks = new HashSet<ChangeServiceTypeTask>();
            this.ChangeStateTasks = new HashSet<ChangeStateTask>();
            this.CustomerSetupTasks = new HashSet<CustomerSetupTask>();
            this.Fees = new HashSet<Fee>();
            this.RadiusAccountings = new HashSet<RadiusAccounting>();
            this.RadiusDailyAccountings = new HashSet<RadiusDailyAccounting>();
            this.RadiusSMS = new HashSet<RadiusSM>();
            this.RecurringDiscounts = new HashSet<RecurringDiscount>();
            this.ScheduledSMS = new HashSet<ScheduledSM>();
            this.SMSArchives = new HashSet<SMSArchive>();
            this.SubscriptionCredits = new HashSet<SubscriptionCredit>();
            this.SubscriptionNotes = new HashSet<SubscriptionNote>();
            this.SubscriptionQuotas = new HashSet<SubscriptionQuota>();
            this.SubscriptionStateHistories = new HashSet<SubscriptionStateHistory>();
            this.SubscriptionSupportRequests = new HashSet<SubscriptionSupportRequest>();
            this.SubscriptionTariffHistories = new HashSet<SubscriptionTariffHistory>();
            this.SubscriptionTransferHistories = new HashSet<SubscriptionTransferHistory>();
            this.SubscriptionTransferHistories1 = new HashSet<SubscriptionTransferHistory>();
            this.SupportRequests = new HashSet<SupportRequest>();
            this.SystemLogs = new HashSet<SystemLog>();
            this.TelekomWorkOrders = new HashSet<TelekomWorkOrder>();
            this.Groups = new HashSet<Group>();
        }
    
        public long ID { get; set; }
        public long CustomerID { get; set; }
        public long AddressID { get; set; }
        public System.DateTime MembershipDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public short State { get; set; }
        public int PaymentDay { get; set; }
        public int ServiceID { get; set; }
        public Nullable<System.DateTime> ActivationDate { get; set; }
        public string OnlinePassword { get; set; }
        public string SubscriberNo { get; set; }
        public Nullable<System.DateTime> OnlinePasswordExpirationDate { get; set; }
        public bool ArchiveScanned { get; set; }
        public int DomainID { get; set; }
        public string ReferenceNo { get; set; }
        public Nullable<System.DateTime> LastTariffChangeDate { get; set; }
        public short RegistrationType { get; set; }
        public Nullable<int> AgentID { get; set; }
    
        public virtual Address Address { get; set; }
        public virtual Agent Agent { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bill> Bills { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BTKSubscriptionChange> BTKSubscriptionChanges { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChangeServiceTypeTask> ChangeServiceTypeTasks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChangeStateTask> ChangeStateTasks { get; set; }
        public virtual Customer Customer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerSetupTask> CustomerSetupTasks { get; set; }
        public virtual Domain Domain { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fee> Fees { get; set; }
        public virtual MobilExpressAutoPayment MobilExpressAutoPayment { get; set; }
        public virtual PartnerRegisteredSubscription PartnerRegisteredSubscription { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RadiusAccounting> RadiusAccountings { get; set; }
        public virtual RadiusAuthorization RadiusAuthorization { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RadiusDailyAccounting> RadiusDailyAccountings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RadiusSM> RadiusSMS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RecurringDiscount> RecurringDiscounts { get; set; }
        public virtual RecurringPaymentSubscription RecurringPaymentSubscription { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScheduledSM> ScheduledSMS { get; set; }
        public virtual Service Service { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SMSArchive> SMSArchives { get; set; }
        public virtual SubscriptionCancellation SubscriptionCancellation { get; set; }
        public virtual SubscriptionCommitment SubscriptionCommitment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubscriptionCredit> SubscriptionCredits { get; set; }
        public virtual SubscriptionGPSCoord SubscriptionGPSCoord { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubscriptionNote> SubscriptionNotes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubscriptionQuota> SubscriptionQuotas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubscriptionStateHistory> SubscriptionStateHistories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubscriptionSupportRequest> SubscriptionSupportRequests { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubscriptionTariffHistory> SubscriptionTariffHistories { get; set; }
        public virtual SubscriptionTelekomInfo SubscriptionTelekomInfo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubscriptionTransferHistory> SubscriptionTransferHistories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubscriptionTransferHistory> SubscriptionTransferHistories1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupportRequest> SupportRequests { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SystemLog> SystemLogs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TelekomWorkOrder> TelekomWorkOrders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Group> Groups { get; set; }
    }
}
