using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MasterISSDatabaseConverter
{
    public static class ConvertParametersUtility
    {
        private static readonly int MaxRetries = 10;
        private static readonly int UsernameNumericLength = 10;
        private static readonly int SubscriberNoNumericLength = 7;
        private static readonly int ReferenceNoNumericLength = 6;
        public static RadiusR.DB.Enums.CustomerState ConvertCustomerState(string state)
        {
            switch (state)
            {
                case "1":
                    {
                        return RadiusR.DB.Enums.CustomerState.Active;
                    }
                case "2":
                case "3":
                    {
                        return RadiusR.DB.Enums.CustomerState.Disabled;
                    }
                case "5":
                    {
                        return RadiusR.DB.Enums.CustomerState.Cancelled;
                    }
                default:
                    {
                        return RadiusR.DB.Enums.CustomerState.Active;
                    }
            }
        }
        public static int ConvertPaymentDay(string paymentDay)
        {
            if (!string.IsNullOrEmpty(paymentDay) && int.TryParse(paymentDay, out int tempPaymentDay) && tempPaymentDay >= 1 && tempPaymentDay <= 28)
            {
                return tempPaymentDay;
            }
            return 1;
        }
        public static string ConvertSubscriberNo(string subscriberNo, List<DatabaseModels.Subscriptions> subscriptions)
        {
            if (string.IsNullOrEmpty(subscriberNo) || subscriberNo.Length != 10 || !subscriberNo.StartsWith("200"))
            {
                while (true)
                {
                    var newSubscriberNo = $"200{new Random().Next(1000000, 9999999)}";
                    using (var db = new SarnetDB.RadiusR_NetSpeed_6Entities())
                    {
                        if (!db.Subscriptions.Where(s => s.SubscriberNo == newSubscriberNo).Any() && !subscriptions.Where(s => s.SubscriberNo == newSubscriberNo).Any())
                        {
                            return newSubscriberNo;
                        }
                    }
                }
            }
            return subscriberNo;
        }
        public static string ConvertPhoneNo(string phoneNo)
        {
            if (string.IsNullOrEmpty(phoneNo))
            {
                return $"5{new Random().Next(100000000, 999999999)}";
            }
            phoneNo = phoneNo.StartsWith("0") ? phoneNo.Remove(0, 1) : phoneNo;
            if (phoneNo.Length != 10)
            {
                return $"5{new Random().Next(100000000, 999999999)}";
            }
            return phoneNo;
        }
        public static RadiusR.DB.Enums.ServiceBillingType ConvertServiceBillingType(string billingType)
        {
            if (string.IsNullOrEmpty(billingType))
            {
                return RadiusR.DB.Enums.ServiceBillingType.Invoiced;
            }
            switch (billingType)
            {
                case "1":
                    {
                        return RadiusR.DB.Enums.ServiceBillingType.PrePaid;
                    }
                default:
                    {
                        return RadiusR.DB.Enums.ServiceBillingType.Invoiced;
                    }
            }
        }
        public static DateTime? ConvertActivationDateTime(short? billingType, string ActivationDate, CultureInfo cultureInfo)
        {
            if (billingType == (short)RadiusR.DB.Enums.ServiceBillingType.Invoiced)
            {
                return DateTime.TryParse(ActivationDate, cultureInfo, DateTimeStyles.AllowWhiteSpaces, out DateTime currentDatetime)
                ? currentDatetime : (DateTime?)null;
            }
            else
            {
                return null;
            }
        }
        public static RadiusR.DB.Enums.CustomerType ConvertCustomerType(string customerType)
        {
            switch (customerType)
            {
                case "G-SAHIS":
                    {
                        return RadiusR.DB.Enums.CustomerType.Individual;
                    }
                case "G-SIRKET":
                    {
                        return RadiusR.DB.Enums.CustomerType.PrivateCompany;
                    }
                case "T-SIRKET":
                    {
                        return RadiusR.DB.Enums.CustomerType.LegalCompany;
                    }
                case "T-KAMU":
                    {
                        return RadiusR.DB.Enums.CustomerType.PublicCompany;
                    }
                default:
                    {
                        return RadiusR.DB.Enums.CustomerType.Individual;
                    }
            }
        }
        public static RadiusR.DB.Enums.IDCardTypes ConvertIDCardType(string idCardType)
        {
            switch (idCardType)
            {
                case "TCKK":
                    {
                        return RadiusR.DB.Enums.IDCardTypes.TCIDCardWithChip;
                    }
                case "TCNC":
                    {
                        return RadiusR.DB.Enums.IDCardTypes.TCBirthCertificate;
                    }
                case "TCPY":
                    {
                        return RadiusR.DB.Enums.IDCardTypes.OldTCPassportGreen;
                    }
                case "TCSC":
                    {
                        return RadiusR.DB.Enums.IDCardTypes.TCDrivingLisence;
                    }
                case "TCYK":
                    {
                        return RadiusR.DB.Enums.IDCardTypes.TCForeignerIDCard;
                    }
                case "YP":
                    {
                        return RadiusR.DB.Enums.IDCardTypes.ForeignerPassport;
                    }
                case "TCEV":
                    {
                        return RadiusR.DB.Enums.IDCardTypes.TCInternationalFamilyCertificate;
                    }
                case "TCGK":
                    {
                        return RadiusR.DB.Enums.IDCardTypes.TCTemporaryIDCard;
                    }
                case "TCGP":
                    {
                        return RadiusR.DB.Enums.IDCardTypes.TCPassportTemporary;
                    }
                case "TCHS":
                    {
                        return RadiusR.DB.Enums.IDCardTypes.TCProsecutorJudgeIDCard;
                    }
                case "TCMA":
                    {
                        return RadiusR.DB.Enums.IDCardTypes.TCBlueCard;
                    }
                case "TCPC":
                    {
                        return RadiusR.DB.Enums.IDCardTypes.TCPassportWithChip;
                    }
                case "TCPK":
                    {
                        return RadiusR.DB.Enums.IDCardTypes.OldTCPassportRed;
                    }
                case "TCSV":
                    {
                        return RadiusR.DB.Enums.IDCardTypes.TCLawyerIDCard;
                    }
                case "SB":
                    {
                        return RadiusR.DB.Enums.IDCardTypes.TravelDocument;
                    }
                default:
                    {
                        return RadiusR.DB.Enums.IDCardTypes.TCBirthCertificate;
                    }
            }
        }
        public static RadiusR.DB.Enums.Sexes ConvertSexes(string sex)
        {
            switch (sex)
            {
                case "E":
                    {
                        return RadiusR.DB.Enums.Sexes.Male;
                    }
                case "K":
                    {
                        return RadiusR.DB.Enums.Sexes.Female;
                    }
                default:
                    {
                        return RadiusR.DB.Enums.Sexes.Male;
                    }
            }
        }
        public static string CheckEmptyParameter(string param, bool isTCK = false)
        {
            if (isTCK)
            {
                if (string.IsNullOrEmpty(param) || param.Length != 11)
                {
                    using (var db = new SarnetDB.RadiusR_NetSpeed_6Entities())
                    {
                        var last = 1;
                        while (true)
                        {
                            param = $"{last}".PadLeft(11, '0');
                            if (db.Customers.Where(c => c.CustomerIDCard.TCKNo == param).FirstOrDefault() == null)
                            {
                                return param;
                            }
                            last++;
                        }

                    }
                }
                return param;
            }
            else
            {
                if (string.IsNullOrEmpty(param))
                {
                    return "-";
                }
                return param;
            }
        }
        public static RadiusR.DB.Enums.BillState ConvertBillState(string state)
        {
            switch (state)
            {
                case "0":
                    {
                        return RadiusR.DB.Enums.BillState.Unpaid;
                    }
                case "1":
                    {
                        return RadiusR.DB.Enums.BillState.Paid;
                    }
                default:
                    {
                        return RadiusR.DB.Enums.BillState.Cancelled;
                    }
            }
        }
        public static string CheckRadiusUsernameDomains(string username)
        {
            var tempUsername = username.Split('@');
            if (tempUsername.Length == 1)
            {
                tempUsername[0] = tempUsername[0] + new Random().Next(1000, 99999);
            }
            return $"{tempUsername[0]}@sarnet";
        }
        public static string GenerateEmail()
        {
            var tempGuid = string.Join("", Guid.NewGuid().ToString().Split('-'));
            var prefix = tempGuid.Substring(0, (tempGuid.Length / 2) + 1);
            return $"{prefix}@sarnettelekom.com.tr";
        }
        public static string GenerateUsername()
        {
            var tempGuid = string.Join("", Guid.NewGuid().ToString().Split('-'));
            var username = tempGuid.Substring(0, 10);
            return username;
        }
        public static string GeneratePassword()
        {
            var tempGuid = string.Join("", Guid.NewGuid().ToString().Split('-'));
            var password = tempGuid.Substring(0, 8);
            var passHash = ComputeSha1Hash(password);
            return passHash;
        }
        public static ConcurrentBag<string> registeredReferenceNo = new ConcurrentBag<string>();
        public static string GenerateUniqueReferenceNo()
        {
            var rnd = new Random();
            var characterPalette = @"0123456789ABCDEFGHIJKLMNPQRSTUVWXYZ";
            var generatedReferenceNo = string.Empty;

            var currentIteration = 0;
            while (true)
            {
                for (int i = 0; i < ReferenceNoNumericLength; i++)
                {
                    generatedReferenceNo += characterPalette[rnd.Next(characterPalette.Length)];
                }

                using (SarnetDB.RadiusR_NetSpeed_6Entities db = new SarnetDB.RadiusR_NetSpeed_6Entities())
                {
                    if (!registeredReferenceNo.Any(client => client == generatedReferenceNo))
                    {
                        registeredReferenceNo.Add(generatedReferenceNo);
                        return generatedReferenceNo;
                    }
                }

                generatedReferenceNo = string.Empty;
                currentIteration++;
            }
        }
        public static DateTime ConvertStringToDatetime(string dateTime, CultureInfo cultureInfo)
        {
            return DateTime.TryParse(dateTime, cultureInfo, DateTimeStyles.AllowWhiteSpaces, out DateTime currentDatetime)
                ? currentDatetime : DateTime.Now;
        }
        public static DateTime ConvertPeriodEnd(string dateTime, CultureInfo cultureInfo)
        {
            return DateTime.TryParse(dateTime, cultureInfo, DateTimeStyles.AllowWhiteSpaces, out DateTime currentDatetime)
                ? currentDatetime.AddMonths(1) : DateTime.Now.AddMonths(1);
        }
        private static string ComputeSha1Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA1 sha256Hash = SHA1.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
