using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterISSDatabaseConverter
{
    public static class DBUtility
    {
        public static ConcurrentBag<int> TCKCounts = new ConcurrentBag<int>();

        public static int TCKCount = 0;
        private static string RewriteTCKNo(string tckNo)
        {
            if (string.IsNullOrEmpty(tckNo) || tckNo.Length != 11)
            {
                while (TCKCounts.Any(tc => tc == TCKCount))
                {
                    TCKCount++;
                }

                TCKCounts.Add(TCKCount);
                return $"{TCKCount}".PadLeft(11, '0');
            }
            return tckNo;
        }
        private static string RewriteRadiusUsername(string username)
        {
            if (username.Split('@').Length == 1)
            {
                return $"{username}00@sarnet";
            }
            var tempUsername = $"{username.Split('@')[0]}@sarnet";
            return tempUsername;
        }
        public static void AddPartners(List<string> dataList, List<DatabaseModels.Partners> partners)
        {
            partners.Add(new DatabaseModels.Partners()
            {
                ID = string.IsNullOrEmpty(dataList[0]) ? null : dataList[0],
                FullName = string.IsNullOrEmpty(dataList[1]) ? null : dataList[1],
                CompanyTitle = string.IsNullOrEmpty(dataList[2]) ? null : dataList[2],
                TaxAdministration = string.IsNullOrEmpty(dataList[3]) ? null : dataList[3],
                TaxNo = string.IsNullOrEmpty(dataList[4]) ? null : dataList[4],
                Address = string.IsNullOrEmpty(dataList[5]) ? null : dataList[5],
                PhoneNo = string.IsNullOrEmpty(dataList[6]) ? null : dataList[6],
                Email = string.IsNullOrEmpty(dataList[7]) ? null : dataList[7],
                Username = string.IsNullOrEmpty(dataList[8]) ? null : dataList[8],
                Password = string.IsNullOrEmpty(dataList[9]) ? null : dataList[9],
                State = string.IsNullOrEmpty(dataList[10]) ? null : dataList[10],
                Authorization = string.IsNullOrEmpty(dataList[11]) ? null : dataList[11],
                Datetime = string.IsNullOrEmpty(dataList[12]) ? null : dataList[12],
            });
        }
        public static void AddSubscriptions(List<string> dataList, List<DatabaseModels.Subscriptions> subscriptions)
        {
            subscriptions.Add(new DatabaseModels.Subscriptions
            {
                ID = string.IsNullOrEmpty(dataList[0]) ? null : dataList[0],
                PartnerID = string.IsNullOrEmpty(dataList[1]) ? null : dataList[1],
                Name = string.IsNullOrEmpty(dataList[2]) ? null : dataList[2],
                Surname = string.IsNullOrEmpty(dataList[3]) ? null : dataList[3],
                TCKNo = RewriteTCKNo(dataList[4]),
                Email = string.IsNullOrEmpty(dataList[5]) ? null : dataList[5],
                TaxNo = string.IsNullOrEmpty(dataList[6]) ? null : dataList[6],
                Address = string.IsNullOrEmpty(dataList[7]) ? null : dataList[7],
                PhoneNo = string.IsNullOrEmpty(dataList[8]) ? null : dataList[8],
                ak_soyad = string.IsNullOrEmpty(dataList[9]) ? null : dataList[9],
                RadiusUsername = RewriteRadiusUsername(dataList[10]), //string.IsNullOrEmpty(dataList[10]) ? null : dataList[10],
                RadiusPassword = string.IsNullOrEmpty(dataList[11]) ? null : dataList[11],
                BirthDate = string.IsNullOrEmpty(dataList[12]) ? null : dataList[12],
                CommitmentDate = string.IsNullOrEmpty(dataList[13]) ? null : dataList[13],
                Tariff = string.IsNullOrEmpty(dataList[14]) ? null : dataList[14],
                State = string.IsNullOrEmpty(dataList[15]) ? null : dataList[15],
                evrak_kontrol = string.IsNullOrEmpty(dataList[16]) ? null : dataList[16],
                MembershipDate = string.IsNullOrEmpty(dataList[17]) ? null : dataList[17],
                ExpirationDate = string.IsNullOrEmpty(dataList[18]) ? null : dataList[18],
                GroupID = string.IsNullOrEmpty(dataList[19]) ? null : dataList[19],
                f_tarih = string.IsNullOrEmpty(dataList[20]) ? null : dataList[20],
                Description = string.IsNullOrEmpty(dataList[23]) ? null : dataList[23],
                GroupCode = string.IsNullOrEmpty(dataList[24]) ? null : dataList[24],
                Akn = string.IsNullOrEmpty(dataList[25]) ? null : dataList[25],
                PhoneNo2 = string.IsNullOrEmpty(dataList[26]) ? null : dataList[26],
                PhoneNo3 = string.IsNullOrEmpty(dataList[27]) ? null : dataList[27],
                Station = string.IsNullOrEmpty(dataList[28]) ? null : dataList[28],
                Location = string.IsNullOrEmpty(dataList[29]) ? null : dataList[29],
                cihaz_kad = string.IsNullOrEmpty(dataList[31]) ? null : dataList[31],
                cihaz_sifre = string.IsNullOrEmpty(dataList[32]) ? null : dataList[32],
                hat = string.IsNullOrEmpty(dataList[33]) ? null : dataList[33],
                PostalCode = string.IsNullOrEmpty(dataList[34]) ? null : dataList[34],
                District = string.IsNullOrEmpty(dataList[35]) ? null : dataList[35],
                Province = string.IsNullOrEmpty(dataList[36]) ? null : dataList[36],
                OnlinePassword = string.IsNullOrEmpty(dataList[37]) ? null : dataList[37],
                Credit = string.IsNullOrEmpty(dataList[38]) ? null : dataList[38],
                verici_id = string.IsNullOrEmpty(dataList[39]) ? null : dataList[39],
                cihaz_tip = string.IsNullOrEmpty(dataList[40]) ? null : dataList[40],
                TaxAdministration = string.IsNullOrEmpty(dataList[41]) ? null : dataList[41],
                SubscriberNo = string.IsNullOrEmpty(dataList[43]) ? null : dataList[43],
                BillingPeriod = string.IsNullOrEmpty(dataList[44]) ? null : dataList[44],
                aknsms = string.IsNullOrEmpty(dataList[45]) ? null : dataList[45],
                Sex = string.IsNullOrEmpty(dataList[46]) ? null : dataList[46],
                Nationality = string.IsNullOrEmpty(dataList[47]) ? null : dataList[47],
                BirthPlace = string.IsNullOrEmpty(dataList[48]) ? null : dataList[48],
                Profession = string.IsNullOrEmpty(dataList[49]) ? null : dataList[49],
                IDCardVolumeNo = string.IsNullOrEmpty(dataList[50]) ? null : dataList[50],
                IDCardRegistrationNumber = string.IsNullOrEmpty(dataList[51]) ? null : dataList[51],
                IDCardPageNo = string.IsNullOrEmpty(dataList[52]) ? null : dataList[52],
                IDCardProvince = string.IsNullOrEmpty(dataList[53]) ? null : dataList[53],
                IDCardDistrict = string.IsNullOrEmpty(dataList[54]) ? null : dataList[54],
                IDCardNeighbourhood = string.IsNullOrEmpty(dataList[55]) ? null : dataList[55],
                IDCardType = string.IsNullOrEmpty(dataList[56]) ? null : dataList[56],
                IDCardSerialNo = string.IsNullOrEmpty(dataList[57]) ? null : dataList[57],
                IDCardRegistrationPlace = string.IsNullOrEmpty(dataList[58]) ? null : dataList[58],
                IDCardRegistrationDate = string.IsNullOrEmpty(dataList[59]) ? null : dataList[59],
                MERSISNo = string.IsNullOrEmpty(dataList[61]) ? null : dataList[61],
                CompanyTitle = string.IsNullOrEmpty(dataList[63]) ? null : dataList[63],
                ExecutiveTCK = string.IsNullOrEmpty(dataList[64]) ? null : dataList[64],
                ExecutivePhoneNo = string.IsNullOrEmpty(dataList[65]) ? null : dataList[65],
                ExecutiveName = string.IsNullOrEmpty(dataList[66]) ? null : dataList[66],
                ExecutiveSurname = string.IsNullOrEmpty(dataList[67]) ? null : dataList[67],
                MotherName = string.IsNullOrEmpty(dataList[68]) ? null : dataList[68],
                FatherName = string.IsNullOrEmpty(dataList[69]) ? null : dataList[69],
                sms_gitsin = string.IsNullOrEmpty(dataList[70]) ? null : dataList[70],
                CustomerType = string.IsNullOrEmpty(dataList[71]) ? null : dataList[71],
                AddressCode = string.IsNullOrEmpty(dataList[72]) ? null : dataList[72],
                ulkeye_giris_tarihi = string.IsNullOrEmpty(dataList[73]) ? null : dataList[73],
                PhoneCode = string.IsNullOrEmpty(dataList[74]) ? null : dataList[74],
                ReferenceNo = ConvertParametersUtility.GenerateUniqueReferenceNo(),
            });
        }
        public static void AddSubscriptionExtras(List<string> dataList, List<DatabaseModels.SubscriptionsExtra> subscriptionsExtras)
        {
            subscriptionsExtras.Add(new DatabaseModels.SubscriptionsExtra
            {
                ExtraId = string.IsNullOrEmpty(dataList[0]) ? null : dataList[0],
                BillAddress = string.IsNullOrEmpty(dataList[1]) ? null : dataList[1],
                SubscriptionId = string.IsNullOrEmpty(dataList[2]) ? null : dataList[2],
                StationId = string.IsNullOrEmpty(dataList[3]) ? null : dataList[3],
                SMSInformation = string.IsNullOrEmpty(dataList[4]) ? null : dataList[4],
                MailInformation = string.IsNullOrEmpty(dataList[5]) ? null : dataList[5],
                faturasecenegi = string.IsNullOrEmpty(dataList[6]) ? null : dataList[6],
                BuildingNo = string.IsNullOrEmpty(dataList[7]) ? null : dataList[7],
                BuildingName = string.IsNullOrEmpty(dataList[8]) ? null : dataList[8],
                StreetName = string.IsNullOrEmpty(dataList[9]) ? null : dataList[9],
                ApartmentNo = string.IsNullOrEmpty(dataList[10]) ? null : dataList[10],
                NeighbourhoodName = string.IsNullOrEmpty(dataList[11]) ? null : dataList[11],
                AddressType = string.IsNullOrEmpty(dataList[12]) ? null : dataList[12],
                InvoicePreference = string.IsNullOrEmpty(dataList[14]) ? null : dataList[14],
            });
        }
        public static void AddEArchive(List<string> dataList, List<DatabaseModels.EArchive> archives)
        {
            archives.Add(new DatabaseModels.EArchive
            {
                ID = string.IsNullOrEmpty(dataList[0]) ? null : dataList[0],
                ReferenceNo = string.IsNullOrEmpty(dataList[2]) ? null : dataList[2],
                BillNo = string.IsNullOrEmpty(dataList[3]) ? null : dataList[3],
                BillIssueDate = string.IsNullOrEmpty(dataList[4]) ? null : dataList[4],
                SubscriptionId = string.IsNullOrEmpty(dataList[5]) ? null : dataList[5],
                BillNumber = string.IsNullOrEmpty(dataList[6]) ? null : dataList[6],
                Description = string.IsNullOrEmpty(dataList[7]) ? null : dataList[7],
                State = string.IsNullOrEmpty(dataList[8]) ? null : dataList[8],
                Message = string.IsNullOrEmpty(dataList[9]) ? null : dataList[9],
                Date = string.IsNullOrEmpty(dataList[4]) ? null : dataList[4],
                Amount = string.IsNullOrEmpty(dataList[12]) ? null : dataList[12],
            });
        }
        public static void AddEBill(List<string> dataList, List<DatabaseModels.EBill> eBills)
        {
            eBills.Add(new DatabaseModels.EBill
            {
                ID = string.IsNullOrEmpty(dataList[0]) ? null : dataList[0],
                ReferenceNo = string.IsNullOrEmpty(dataList[2]) ? null : dataList[2],
                BillNo = string.IsNullOrEmpty(dataList[3]) ? null : dataList[3],
                BillIssueDate = string.IsNullOrEmpty(dataList[4]) ? null : dataList[4],
                SubscriptionId = string.IsNullOrEmpty(dataList[5]) ? null : dataList[5],
                BillNumber = string.IsNullOrEmpty(dataList[6]) ? null : dataList[6],
                Description = string.IsNullOrEmpty(dataList[7]) ? null : dataList[7],
                State = string.IsNullOrEmpty(dataList[8]) ? null : dataList[8],
                Message = string.IsNullOrEmpty(dataList[9]) ? null : dataList[9],
                Date = string.IsNullOrEmpty(dataList[4]) ? null : dataList[4],
                Amount = string.IsNullOrEmpty(dataList[12]) ? null : dataList[12],
            });
        }
        public static void AddBill(List<string> dataList, List<DatabaseModels.Bills> bills)
        {
            bills.Add(new DatabaseModels.Bills
            {
                ID = string.IsNullOrEmpty(dataList[0]) ? null : dataList[0],
                PartnerID = string.IsNullOrEmpty(dataList[1]) ? null : dataList[1],
                SubscriptionId = string.IsNullOrEmpty(dataList[2]) ? null : dataList[2],
                GroupID = string.IsNullOrEmpty(dataList[3]) ? null : dataList[3],
                TariffID = string.IsNullOrEmpty(dataList[4]) ? null : dataList[4],
                Amount = string.IsNullOrEmpty(dataList[5]) ? null : dataList[5],
                AmountWithoutTariff = string.IsNullOrEmpty(dataList[6]) ? null : dataList[6],
                State = string.IsNullOrEmpty(dataList[7]) ? null : dataList[7],
                Round = string.IsNullOrEmpty(dataList[8]) ? null : dataList[8],
                Day = string.IsNullOrEmpty(dataList[9]) ? null : dataList[9],
                Invoice = string.IsNullOrEmpty(dataList[10]) ? null : dataList[10],
                BillIssueDate = string.IsNullOrEmpty(dataList[11]) ? null : dataList[11],
                Description = string.IsNullOrEmpty(dataList[12]) ? null : dataList[12],
                DueDate = string.IsNullOrEmpty(dataList[13]) ? null : dataList[13],
                BillNo = string.IsNullOrEmpty(dataList[17]) ? null : dataList[17],
                PaymentDate = string.IsNullOrEmpty(dataList[18]) ? null : dataList[18],
                Paying = string.IsNullOrEmpty(dataList[19]) ? null : dataList[19],
                BillType = string.IsNullOrEmpty(dataList[20]) ? null : dataList[20],
            });
        }
        public static void AddGroup(List<string> dataList, List<DatabaseModels.Groups> groups)
        {
            groups.Add(new DatabaseModels.Groups
            {
                ID = string.IsNullOrEmpty(dataList[0]) ? null : dataList[0],
                GroupName = string.IsNullOrEmpty(dataList[1]) ? null : dataList[1],
                GroupID = string.IsNullOrEmpty(dataList[2]) ? null : dataList[2],
            });
        }
        public static void AddSubscriptionNotes(List<string> dataList, List<DatabaseModels.SubscriptionNotes> subscriptionNotes)
        {
            subscriptionNotes.Add(new DatabaseModels.SubscriptionNotes
            {
                ID = string.IsNullOrEmpty(dataList[0]) ? null : dataList[0],
                FullName = string.IsNullOrEmpty(dataList[1]) ? null : dataList[1],
                SubscriptionID = string.IsNullOrEmpty(dataList[2]) ? null : dataList[2],
                Note = string.IsNullOrEmpty(dataList[3]) ? null : dataList[3],
                Date = string.IsNullOrEmpty(dataList[4]) ? null : dataList[4],
            });
        }
        public static void AddNAS(List<string> dataList, List<DatabaseModels.NAS> nas)
        {
            nas.Add(new DatabaseModels.NAS
            {
                ID = string.IsNullOrEmpty(dataList[0]) ? null : dataList[0],
                IP = string.IsNullOrEmpty(dataList[1]) ? null : dataList[1],
                Username = string.IsNullOrEmpty(dataList[2]) ? null : dataList[2],
                Password = string.IsNullOrEmpty(dataList[3]) ? null : dataList[3],
                Name = string.IsNullOrEmpty(dataList[4]) ? null : dataList[4],
                Secret = string.IsNullOrEmpty(dataList[5]) ? null : dataList[5],
                PartnerID = string.IsNullOrEmpty(dataList[6]) ? null : dataList[6],
                State = string.IsNullOrEmpty(dataList[7]) ? null : dataList[7],
            });
        }
        public static void AddPaymentNotes(List<string> dataList, List<DatabaseModels.PaymentNotes> paymentNotes)
        {
            paymentNotes.Add(new DatabaseModels.PaymentNotes
            {
                SubscriptionId = string.IsNullOrEmpty(dataList[0]) ? null : dataList[0],
                BillNumber = string.IsNullOrEmpty(dataList[1]) ? null : dataList[1],
                Note = string.IsNullOrEmpty(dataList[2]) ? null : dataList[2],
            });
        }
        public static void AddTariffs(List<string> dataList, List<DatabaseModels.Tariffs> tariffs)
        {
            tariffs.Add(new DatabaseModels.Tariffs
            {
                ID = string.IsNullOrEmpty(dataList[0]) ? null : dataList[0],
                Name = string.IsNullOrEmpty(dataList[1]) ? null : dataList[1],
                TariffType = string.IsNullOrEmpty(dataList[2]) ? null : dataList[2],
                CommitmentLength = string.IsNullOrEmpty(dataList[3]) ? null : dataList[3],
                PriceWithoutCommitment = string.IsNullOrEmpty(dataList[4]) ? null : dataList[4],
                CommitmentFreeMonth = string.IsNullOrEmpty(dataList[5]) ? null : dataList[5],
                Day = string.IsNullOrEmpty(dataList[6]) ? null : dataList[6],
                Price = string.IsNullOrEmpty(dataList[7]) ? null : dataList[7],
                bfiyat = string.IsNullOrEmpty(dataList[8]) ? null : dataList[8],
                DownloadLimit = string.IsNullOrEmpty(dataList[9]) ? null : dataList[9],
                UploadLimit = string.IsNullOrEmpty(dataList[10]) ? null : dataList[10],
                TariffLimit = string.IsNullOrEmpty(dataList[11]) ? null : dataList[11],
                TimeLimit = string.IsNullOrEmpty(dataList[12]) ? null : dataList[12],
                IsActive = string.IsNullOrEmpty(dataList[13]) ? null : dataList[13],
                PromotionPrice = string.IsNullOrEmpty(dataList[14]) ? null : dataList[14],
                PartnerId = string.IsNullOrEmpty(dataList[16]) ? null : dataList[16],
                Akn = string.IsNullOrEmpty(dataList[17]) ? null : dataList[17],
                AknUpload = string.IsNullOrEmpty(dataList[18]) ? null : dataList[18],
                AknDownload = string.IsNullOrEmpty(dataList[19]) ? null : dataList[19],
            });
        }
        public static void AddRadCheck(List<string> dataList, List<DatabaseModels.RadCheck> radChecks)
        {
            radChecks.Add(new DatabaseModels.RadCheck()
            {
                ID = string.IsNullOrEmpty(dataList[0]) ? null : dataList[0],
                Username = string.IsNullOrEmpty(dataList[1]) ? null : dataList[1],
                Attribute = string.IsNullOrEmpty(dataList[2]) ? null : dataList[2],
                OP = string.IsNullOrEmpty(dataList[3]) ? null : dataList[3],
                Value = string.IsNullOrEmpty(dataList[4]) ? null : dataList[4],
            });
        }
        public static void AddAgentTariff(List<string> dataList, List<DatabaseModels.AgentTariff> agentTariffs)
        {
            agentTariffs.Add(new DatabaseModels.AgentTariff()
            {
                id = string.IsNullOrEmpty(dataList[0]) ? null : dataList[0],
                AgentId = string.IsNullOrEmpty(dataList[1]) ? null : dataList[1],
                AgentTariffId = string.IsNullOrEmpty(dataList[2]) ? null : dataList[2],
            });
        }
        public static void CopyAddresses(List<string> dataList, List<DatabaseModels.RegisteredAddresses> registeredAddresses)
        {
            registeredAddresses.Add(new DatabaseModels.RegisteredAddresses()
            {
                RadiusUsername = string.IsNullOrEmpty(dataList[0]) ? null : dataList[0],
                AddressID = string.IsNullOrEmpty(dataList[1]) ? null : dataList[1],
                ProvinceID = string.IsNullOrEmpty(dataList[2]) ? null : dataList[2],
                DistrictID = string.IsNullOrEmpty(dataList[3]) ? null : dataList[3],
                RuralCode = string.IsNullOrEmpty(dataList[4]) ? null : dataList[4],
                NeighborhoodID = string.IsNullOrEmpty(dataList[5]) ? null : dataList[5],
                StreetID = string.IsNullOrEmpty(dataList[6]) ? null : dataList[6],
                DoorID = string.IsNullOrEmpty(dataList[7]) ? null : dataList[7],
                ApartmentID = string.IsNullOrEmpty(dataList[8]) ? null : dataList[8],
                AddressNo = string.IsNullOrEmpty(dataList[9]) ? null : dataList[9],
                AddressText = string.IsNullOrEmpty(dataList[10]) ? null : dataList[10],
                PostalCode = string.IsNullOrEmpty(dataList[11]) ? null : dataList[11],
                ProvinceName = string.IsNullOrEmpty(dataList[12]) ? null : dataList[12],
                DistrictName = string.IsNullOrEmpty(dataList[13]) ? null : dataList[13],
                NeighborhoodName = string.IsNullOrEmpty(dataList[14]) ? null : dataList[14],
                StreetName = string.IsNullOrEmpty(dataList[15]) ? null : dataList[15],
                DoorNo = string.IsNullOrEmpty(dataList[16]) ? null : dataList[16],
                ApartmentNo = string.IsNullOrEmpty(dataList[17]) ? null : dataList[17],
                BuildingName = string.IsNullOrEmpty(dataList[18]) ? null : dataList[18],
                Floor = string.IsNullOrEmpty(dataList[19]) ? null : dataList[19],
            });
        }
    }
}
