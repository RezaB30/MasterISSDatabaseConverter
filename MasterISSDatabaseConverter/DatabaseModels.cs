using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterISSDatabaseConverter
{
    public class DatabaseModels
    {
        public class Partners
        {
            public string ID { get; set; }
            public string FullName { get; set; }
            public string CompanyTitle { get; set; }
            public string TaxAdministration { get; set; }
            public string TaxNo { get; set; }
            public string Address { get; set; }
            public string PhoneNo { get; set; }
            public string Email { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string State { get; set; }
            public string Authorization { get; set; }
            public string Datetime { get; set; }
        }
        public class Subscriptions
        {
            public string ID { get; set; }
            public string PartnerID { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string TCKNo { get; set; }
            public string Email { get; set; }
            public string TaxNo { get; set; }
            public string Address { get; set; }
            public string PhoneNo { get; set; }
            public string ak_soyad { get; set; }
            public string RadiusUsername { get; set; }
            public string RadiusPassword { get; set; }
            public string BirthDate { get; set; }
            public string CommitmentDate { get; set; }
            public string Tariff { get; set; }
            public string State { get; set; }
            public string evrak_kontrol { get; set; }
            public string MembershipDate { get; set; }
            public string ExpirationDate { get; set; }
            public string GroupID { get; set; }
            public string f_tarih { get; set; }
            public string Description { get; set; }
            public string GroupCode { get; set; }
            public string Akn { get; set; }
            public string PhoneNo2 { get; set; }
            public string PhoneNo3 { get; set; }
            public string Station { get; set; }
            public string Location { get; set; }
            public string cihaz_kad { get; set; }
            public string cihaz_sifre { get; set; }
            public string hat { get; set; }
            public string PostalCode { get; set; }
            public string District { get; set; }
            public string Province { get; set; }
            public string OnlinePassword { get; set; }
            public string Credit { get; set; } // bakiye
            public string verici_id { get; set; }
            public string cihaz_tip { get; set; }
            public string TaxAdministration { get; set; }
            public string CustomerType { get; set; } // müşteri tipi
            public string SubscriberNo { get; set; }
            public string BillingPeriod { get; set; }
            public string aknsms { get; set; }
            public string Sex { get; set; }
            public string Nationality { get; set; }
            public string BirthPlace { get; set; }
            public string Profession { get; set; }
            public string IDCardVolumeNo { get; set; } // cilt no
            public string IDCardRegistrationNumber { get; set; } // sicil - kütük no
            public string IDCardPageNo { get; set; } // sayfa no
            public string IDCardProvince { get; set; }
            public string IDCardDistrict { get; set; }
            public string IDCardNeighbourhood { get; set; }
            public string IDCardType { get; set; }
            public string IDCardSerialNo { get; set; }
            public string IDCardRegistrationPlace { get; set; }
            public string IDCardRegistrationDate { get; set; }
            public string MERSISNo { get; set; }
            public string kimlik_aidiyet { get; set; }
            public string CompanyTitle { get; set; }
            public string ExecutiveTCK { get; set; }
            public string ExecutivePhoneNo { get; set; }
            public string ExecutiveName { get; set; }
            public string ExecutiveSurname { get; set; }
            public string MotherName { get; set; }
            public string FatherName { get; set; }
            public string sms_gitsin { get; set; }
            public string AddressCode { get; set; }
            public string ulkeye_giris_tarihi { get; set; }
            public string PhoneCode { get; set; }
            //public string alici_id { get; set; } // have null values
            public string ReferenceNo { get; set; }
        }
        public class SubscriptionsExtra
        {
            public string ExtraId { get; set; }
            public string BillAddress { get; set; }
            public string SubscriptionId { get; set; } // musteriUid
            public string StationId { get; set; } // ?
            public string SMSInformation { get; set; }
            public string MailInformation { get; set; }
            public string faturasecenegi { get; set; }
            public string BuildingNo { get; set; }
            public string BuildingName { get; set; }
            public string StreetName { get; set; }
            public string ApartmentNo { get; set; }
            public string NeighbourhoodName { get; set; }
            public string AddressType { get; set; } // ?
            public string InvoicePreference { get; set; } // fatura tercihi 1- faturalı , 2- ön ödemeli
        }
        public class EArchive
        {
            public string ID { get; set; }
            public string ReferenceNo { get; set; }
            public string BillNo { get; set; } // fatura no srt...
            public string Date { get; set; } // Oluşma zamanı
            public string SubscriptionId { get; set; }
            public string BillNumber { get; set; } // TblFatNo
            public string Description { get; set; }
            public string State { get; set; }
            public string Message { get; set; }
            public string BillIssueDate { get; set; } // oluşma zamanı
            public string Amount { get; set; }
        }
        public class EBill
        {
            public string ID { get; set; }
            public string ReferenceNo { get; set; }
            public string BillNo { get; set; } // fatura no srt...
            public string Date { get; set; } // Oluşma zamanı
            public string SubscriptionId { get; set; }
            public string BillNumber { get; set; } // TblFatNo
            public string Description { get; set; }
            public string State { get; set; }
            public string Message { get; set; }
            public string BillIssueDate { get; set; } // oluşma zamanı
            public string Amount { get; set; }
        }
        public class Bills
        {
            public string ID { get; set; }
            public string PartnerID { get; set; } // b_id
            public string SubscriptionId { get; set; } // u_id
            public string GroupID { get; set; } // g_id
            public string TariffID { get; set; } // t_id
            public string Amount { get; set; }
            public string AmountWithoutTariff { get; set; }
            public string State { get; set; }
            public string Round { get; set; } // tur
            public string Day { get; set; }
            public string Invoice { get; set; } // fatura_kesim
            public string BillIssueDate { get; set; }
            public string Description { get; set; }
            public string DueDate { get; set; } // son ödeme tarihi
            public string BillNo { get; set; }
            public string PaymentDate { get; set; }
            public string Paying { get; set; } // ödeyen
            public string BillType { get; set; }

        }
        public class Groups
        {
            public string ID { get; set; }
            public string GroupName { get; set; }
            public string GroupID { get; set; }
        }
        public class SubscriptionNotes
        {
            public string ID { get; set; }
            public string FullName { get; set; }
            public string SubscriptionID { get; set; }
            public string Note { get; set; }
            public string Date { get; set; }

        }
        public class NAS
        {
            public string ID { get; set; }
            public string IP { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string Name { get; set; }
            public string Secret { get; set; }
            public string PartnerID { get; set; }
            public string State { get; set; } // 1 aktif
        }
        public class PaymentNotes
        {
            public string SubscriptionId { get; set; }
            public string BillNumber { get; set; }
            public string Note { get; set; }
        }
        public class Tariffs
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public string TariffType { get; set; } // 1,2,3, - 6,9
            public string CommitmentLength { get; set; }
            public string PriceWithoutCommitment { get; set; }
            public string CommitmentFreeMonth { get; set; }
            public string Day { get; set; }
            public string Price { get; set; }
            public string bfiyat { get; set; }  // ??
            public string DownloadLimit { get; set; }
            public string UploadLimit { get; set; }
            public string TariffLimit { get; set; } // not need
            public string TimeLimit { get; set; }
            public string IsActive { get; set; }
            public string PromotionPrice { get; set; }
            public string PartnerId { get; set; } // 0-1
            public string Akn { get; set; }
            public string AknUpload { get; set; }
            public string AknDownload { get; set; }

        }
        public class RadCheck
        {
            public string ID { get; set; }
            public string Username { get; set; }
            public string Attribute { get; set; }
            public string OP { get; set; }
            public string Value { get; set; }
        }
        public class AgentTariff
        {
            public string id { get; set; }
            public string AgentId { get; set; }
            public string AgentTariffId { get; set; }
        }
        public class RegisteredAddresses
        {
            public string RadiusUsername { get; set; }
            public string AddressID { get; set; }
            public string ProvinceID { get; set; }
            public string DistrictID { get; set; }
            public string RuralCode { get; set; }
            public string NeighborhoodID { get; set; }
            public string StreetID { get; set; }
            public string DoorID { get; set; }
            public string ApartmentID { get; set; }
            public string AddressNo { get; set; }
            public string AddressText { get; set; }
            public string PostalCode { get; set; }
            public string ProvinceName { get; set; }
            public string DistrictName { get; set; }
            public string NeighborhoodName { get; set; }
            public string StreetName { get; set; }
            public string DoorNo { get; set; }
            public string ApartmentNo { get; set; }
            public string BuildingName { get; set; }
            public string Floor { get; set; }

        }
    }
    public enum TableType
    {
        Partners = 1,
        EArchive = 2,
        EBill = 3,
        Bill = 4,
        Group = 5,
        SubscriptionNotes = 6,
        NAS = 7,
        PaymentNotes = 8,
        Tariffs = 9,
        Subscriptions = 10,
        SubscriptionsExtra = 11,
        RadCheck = 12,
        AgentTariff = 13
    }
}
