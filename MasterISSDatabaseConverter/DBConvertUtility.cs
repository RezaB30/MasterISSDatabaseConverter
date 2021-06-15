using NLog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MasterISSDatabaseConverter
{
    public class DBConvertUtility
    {
        Logger generalLogger = LogManager.GetLogger("general");
        public ConcurrentDictionary<string, long> AddedTariffs = new ConcurrentDictionary<string, long>();
        public ConcurrentDictionary<string, long> AddedAddress = new ConcurrentDictionary<string, long>();
        public ConcurrentDictionary<string, long> AddedCustomerAddress = new ConcurrentDictionary<string, long>();
        public ConcurrentDictionary<string, long> AddedBillingAddress = new ConcurrentDictionary<string, long>();
        public ConcurrentDictionary<string, long> AddedPartnerAddress = new ConcurrentDictionary<string, long>();
        public ConcurrentDictionary<string, long> AddedGroups = new ConcurrentDictionary<string, long>();
        public ConcurrentDictionary<string, long> AddedAgent = new ConcurrentDictionary<string, long>();
        private List<DatabaseModels.Subscriptions> _subscriptions { get; set; }
        private List<DatabaseModels.Groups> _groups { get; set; }
        private List<DatabaseModels.Tariffs> _tariffs { get; set; }
        private List<DatabaseModels.SubscriptionsExtra> _subscriptionExtra { get; set; }
        private List<DatabaseModels.Bills> _bills { get; set; }
        private List<DatabaseModels.EBill> _ebills { get; set; }
        private List<DatabaseModels.EArchive> _earchive { get; set; }
        private List<DatabaseModels.NAS> _nas { get; set; }
        private List<DatabaseModels.PaymentNotes> _paymentNotes { get; set; }
        private List<DatabaseModels.SubscriptionNotes> _subscriptionNotes { get; set; }
        private List<DatabaseModels.Partners> _partners { get; set; }
        private List<DatabaseModels.AgentTariff> _agentTariffs { get; set; }
        public DBConvertUtility(List<DatabaseModels.Subscriptions> subscriptions, List<DatabaseModels.Groups> groups, List<DatabaseModels.Bills> bills,
            List<DatabaseModels.EArchive> eArchives, List<DatabaseModels.EBill> eBills, List<DatabaseModels.NAS> nas, List<DatabaseModels.Partners> partners,
            List<DatabaseModels.PaymentNotes> paymentNotes, List<DatabaseModels.SubscriptionNotes> subscriptionNotes, List<DatabaseModels.SubscriptionsExtra> subscriptionsExtras,
            List<DatabaseModels.Tariffs> tariffs, List<DatabaseModels.AgentTariff> agentTariffs)
        {
            _subscriptions = subscriptions;
            _groups = groups;
            _bills = bills;
            _earchive = eArchives;
            _ebills = eBills;
            _nas = nas;
            _partners = partners;
            _paymentNotes = paymentNotes;
            _subscriptionExtra = subscriptionsExtras;
            _subscriptionNotes = subscriptionNotes;
            _tariffs = tariffs;
            _agentTariffs = agentTariffs;
        }
        public void LoadDatabaseTables(ProgressBar progressBar)
        {
            //ClearAll();
            //LoadNAS();
            LoadService();
            LoadServiceDomain();
            LoadGroups();
            //GetSubscriptionGroup();
            LoadAgent();
            LoadAgentTariff();
            GetRegisteredAddress();
            var customers = GetCustomers();
            Console.WriteLine("Customer Add Started");
            var customerCount = 1;
            progressBar.Maximum = customers.Count();
            foreach (var item in customers)
            {
                try
                {
                    if (progressBar.Value < customers.Count())
                    {
                        progressBar.Value++;
                    }
                    using (SarnetDB.RadiusR_NetSpeed_6Entities db = new SarnetDB.RadiusR_NetSpeed_6Entities())
                    {
                        //var tempHasRadiusUsername = item.Value.Select(s => ConvertParametersUtility.CheckRadiusUsernameDomains(s.RadiusUsername)).ToArray();
                        var tempCustomer = new SarnetDB.Customer();
                        var tempCustomerInfo = item.Value.FirstOrDefault();
                        var tempId = tempCustomerInfo.ID;
                        if (!db.Customers.Where(c => c.CustomerIDCard.TCKNo == item.Key).Any())
                        {
                            tempCustomer = db.Customers.Add(new SarnetDB.Customer()
                            {
                                AddressID = LoadCustomerAddress(tempId).ID,
                                BillingAddressID = LoadBillingAddress(tempId).ID,
                                FirstName = ConvertParametersUtility.ConvertCustomerType(tempCustomerInfo.CustomerType) == RadiusR.DB.Enums.CustomerType.Individual
                                                                ? ConvertParametersUtility.CheckEmptyParameter(tempCustomerInfo.Name) :
                                                                ConvertParametersUtility.CheckEmptyParameter(tempCustomerInfo.ExecutiveName),
                                LastName = ConvertParametersUtility.ConvertCustomerType(tempCustomerInfo.CustomerType) != RadiusR.DB.Enums.CustomerType.Individual
                                                                ? ConvertParametersUtility.CheckEmptyParameter(tempCustomerInfo.ExecutiveSurname) : ConvertParametersUtility.CheckEmptyParameter(tempCustomerInfo.Surname),
                                CustomerType = (short)ConvertParametersUtility.ConvertCustomerType(tempCustomerInfo.CustomerType),
                                MothersMaidenName = ConvertParametersUtility.ConvertCustomerType(tempCustomerInfo.CustomerType) != RadiusR.DB.Enums.CustomerType.Individual
                                                                ? ConvertParametersUtility.CheckEmptyParameter(tempCustomerInfo.ExecutiveSurname) : ConvertParametersUtility.CheckEmptyParameter(tempCustomerInfo.Surname),
                                BirthDate = ConvertParametersUtility.ConvertStringToDatetime(tempCustomerInfo.BirthDate, CultureInfo.InvariantCulture),
                                BirthPlace = ConvertParametersUtility.CheckEmptyParameter(tempCustomerInfo.BirthPlace),
                                ContactPhoneNo = ConvertParametersUtility.ConvertPhoneNo(tempCustomerInfo.PhoneNo),
                                Culture = "tr-tr",
                                CorporateCustomerInfo = ConvertParametersUtility.ConvertCustomerType(tempCustomerInfo.CustomerType) == RadiusR.DB.Enums.CustomerType.Individual
                                                                ? null : LoadCorporate(tempId, tempCustomerInfo.MERSISNo,
                                                                tempCustomerInfo.TaxNo, tempCustomerInfo.TaxAdministration, tempCustomerInfo.CompanyTitle, "000001"),
                                CustomerIDCard = new SarnetDB.CustomerIDCard()
                                {
                                    DateOfIssue = ConvertParametersUtility.ConvertStringToDatetime(tempCustomerInfo.IDCardRegistrationDate, CultureInfo.InvariantCulture),
                                    District = ConvertParametersUtility.CheckEmptyParameter(tempCustomerInfo.IDCardDistrict),
                                    Neighbourhood = ConvertParametersUtility.CheckEmptyParameter(tempCustomerInfo.IDCardNeighbourhood),
                                    PageNo = ConvertParametersUtility.CheckEmptyParameter(tempCustomerInfo.IDCardPageNo),
                                    PassportNo = ConvertParametersUtility.ConvertIDCardType(tempCustomerInfo.IDCardType) == RadiusR.DB.Enums.IDCardTypes.ForeignerPassport
                                        ? ConvertParametersUtility.CheckEmptyParameter(tempCustomerInfo.TCKNo) : null,
                                    PlaceOfIssue = ConvertParametersUtility.CheckEmptyParameter(tempCustomerInfo.IDCardRegistrationPlace),
                                    Province = ConvertParametersUtility.CheckEmptyParameter(tempCustomerInfo.IDCardProvince),
                                    RowNo = ConvertParametersUtility.CheckEmptyParameter(tempCustomerInfo.IDCardRegistrationNumber),
                                    SerialNo = ConvertParametersUtility.CheckEmptyParameter(tempCustomerInfo.IDCardSerialNo),
                                    TCKNo = ConvertParametersUtility.ConvertCustomerType(tempCustomerInfo.CustomerType) == RadiusR.DB.Enums.CustomerType.Individual
                                                                    ? ConvertParametersUtility.CheckEmptyParameter(tempCustomerInfo.TCKNo, true) : ConvertParametersUtility.CheckEmptyParameter(tempCustomerInfo.ExecutiveTCK, true),
                                    VolumeNo = ConvertParametersUtility.CheckEmptyParameter(tempCustomerInfo.IDCardVolumeNo),
                                    TypeID = (short)ConvertParametersUtility.ConvertIDCardType(tempCustomerInfo.IDCardType)
                                },
                                Email = ConvertParametersUtility.CheckEmptyParameter(tempCustomerInfo.Email),
                                FathersName = ConvertParametersUtility.CheckEmptyParameter(tempCustomerInfo.FatherName),
                                MothersName = ConvertParametersUtility.CheckEmptyParameter(tempCustomerInfo.MotherName),
                                Nationality = 228,
                                Profession = 962,
                                Sex = (short)ConvertParametersUtility.ConvertSexes(tempCustomerInfo.Sex)
                            });
                            db.SaveChanges();
                        }
                        else
                        {
                            tempCustomer = db.Customers.Where(c => c.CustomerIDCard.TCKNo == item.Key).FirstOrDefault();
                        }

                        foreach (var sub in item.Value)
                        {
                            if (sub != null && !db.Subscriptions.Where(s => s.SubscriberNo == sub.SubscriberNo).Any())
                            {
                                var tempServiceId = (int)(AddedTariffs.TryGetValue(sub.Tariff, out long serviceId) ? serviceId : AddedTariffs.FirstOrDefault().Value);
                                var tempService = db.Services.Find(tempServiceId);
                                var subscription = db.Subscriptions.Add(new SarnetDB.Subscription()
                                {
                                    ActivationDate = ConvertParametersUtility.ConvertActivationDateTime(tempService?.BillingType, sub.MembershipDate, CultureInfo.InvariantCulture),  //ConvertParametersUtility.ConvertStringToDatetime(sub.MembershipDate, CultureInfo.InvariantCulture),
                                    MembershipDate = ConvertParametersUtility.ConvertStringToDatetime(sub.MembershipDate, CultureInfo.InvariantCulture),
                                    AddressID = LoadAddress(sub.ID).ID,
                                    DomainID = db.Domains.FirstOrDefault().ID,
                                    ArchiveScanned = false,
                                    LastTariffChangeDate = null,
                                    OnlinePassword = string.IsNullOrEmpty(sub.OnlinePassword) ? "123456" : sub.OnlinePassword,
                                    OnlinePasswordExpirationDate = null,
                                    PaymentDay = 1, //ConvertParametersUtility.ConvertPaymentDay(sub.BillingPeriod), //Convert.ToInt32(sub.f_tarih),
                                    EndDate = ConvertParametersUtility.ConvertCustomerState(sub.State) == RadiusR.DB.Enums.CustomerState.Cancelled
                                    ? ConvertParametersUtility.ConvertStringToDatetime(sub.ExpirationDate, CultureInfo.InvariantCulture) : (DateTime?)null,
                                    SubscriberNo = sub.SubscriberNo, //ConvertParametersUtility.ConvertSubscriberNo(sub.SubscriberNo),
                                    Customer = tempCustomer,
                                    //Address = LoadAddress(sub.ID),
                                    CustomerID = tempCustomer.ID,
                                    Domain = db.Domains.FirstOrDefault(),
                                    ReferenceNo = sub.ReferenceNo, //ConvertParametersUtility.GenerateUniqueReferenceNo(), //RadiusR.DB.DomainsCache.UsernameFactory.GenerateUniqueReferenceNo(),
                                    State = (short)ConvertParametersUtility.ConvertCustomerState(sub.State),
                                    RegistrationType = (short)RadiusR.DB.Enums.SubscriptionRegistrationType.NewRegistration,
                                    ServiceID = tempServiceId,
                                    AgentID = AddedAgent.TryGetValue(sub.PartnerID, out long partnerId) ? (int?)partnerId : null //(int?)AddedAgent[sub.PartnerID]
                                });
                                db.SaveChanges();
                                LoadRadiusAuthorization(sub.RadiusUsername, sub.RadiusPassword, subscription.ID, ConvertParametersUtility.ConvertStringToDatetime(sub.ExpirationDate, CultureInfo.InvariantCulture));
                                LoadSubscriptionNote(subscription, sub.ID);
                                LoadSubscriptionGroup(subscription, sub.ID);
                                LoadBills(subscription.ID, tempId);
                                Console.WriteLine($"Subscription Completed {customerCount}");
                                customerCount++;
                            }

                        }

                    }
                }
                catch (DbEntityValidationException ex)
                {
                    generalLogger.Error(ex, string.Join(Environment.NewLine, ex.EntityValidationErrors.SelectMany(e => e.ValidationErrors.Select(se => se.PropertyName + "->" + se.ErrorMessage))));
                }
                catch (Exception ex)
                {
                    generalLogger.Error(ex);
                }
            }
            MessageBox.Show("Load Database Completed");
        }
        public Dictionary<string, List<DatabaseModels.Subscriptions>> GetCustomers()
        {
            foreach (var item in _subscriptions)
            {
                item.SubscriberNo = ConvertParametersUtility.ConvertSubscriberNo(item.SubscriberNo, _subscriptions);
            }
            var customers = _subscriptions.GroupBy(s => s.TCKNo).Select(s => new
            {
                id = s.Key,
                subscriptionList = s.Select(sub => sub).ToList()
            });
            Dictionary<string, List<DatabaseModels.Subscriptions>> keyValuePairs = new Dictionary<string, List<DatabaseModels.Subscriptions>>();
            foreach (var item in customers)
            {
                var tcCount = 1;
                if (string.IsNullOrEmpty(item.id))
                {
                    var tempTck = $"{tcCount}".PadLeft(11, '0');
                    while (keyValuePairs.TryGetValue(tempTck, out List<DatabaseModels.Subscriptions> tempSub))
                    {
                        tcCount++;
                        tempTck = $"{tcCount}".PadLeft(11, '0');
                    }
                    keyValuePairs.Add(tempTck, item.subscriptionList);
                }
                else
                {
                    keyValuePairs.Add(item.id, item.subscriptionList);
                }
            }
            return keyValuePairs;
        }
        private SarnetDB.CorporateCustomerInfo LoadCorporate(string tempId, /*long customerId , */string mersisNo, string taxNo, string taxOffice, string title, string tradeRegistrationNo)
        {
            try
            {
                Console.WriteLine("LoadCorporate started");

                using (SarnetDB.RadiusR_NetSpeed_6Entities db = new SarnetDB.RadiusR_NetSpeed_6Entities())
                {
                    var customerCorporate = db.CorporateCustomerInfoes.Add(new SarnetDB.CorporateCustomerInfo()
                    {
                        //CustomerID = customerId,
                        //Address = LoadAddress(tempId),
                        CentralSystemNo = string.IsNullOrEmpty(mersisNo) ? "0000000000000019" : mersisNo.PadLeft(16, '0'),
                        CompanyAddressID = LoadAddress(tempId).ID,
                        TaxNo = ConvertParametersUtility.CheckEmptyParameter(taxNo),
                        TaxOffice = ConvertParametersUtility.CheckEmptyParameter(taxOffice),
                        Title = ConvertParametersUtility.CheckEmptyParameter(title),
                        TradeRegistrationNo = ConvertParametersUtility.CheckEmptyParameter(tradeRegistrationNo)
                    });
                    Console.WriteLine("LoadCorporate completed");

                    return customerCorporate;
                }
            }
            catch (Exception ex)
            {
                generalLogger.Error(ex);
                throw;
            }
        }
        private void LoadService()
        {
            try
            {
                Console.WriteLine("Load Service started");
                using (var db = new SarnetDB.RadiusR_NetSpeed_6Entities())
                {
                    if (_tariffs.Count() == db.Services.Count())
                    {
                        var tempService = db.Services.ToArray();
                        for (int i = 0; i < _tariffs.Count(); i++)
                        {
                            AddedTariffs.TryAdd(_tariffs[i].ID, tempService[i].ID);
                        }
                    }
                    else
                    {
                        foreach (var item in _tariffs)
                        {
                            var currentTariff = item;
                            var subscription = _subscriptions.Where(s => s.Tariff == item.ID).FirstOrDefault();
                            //var billingType = subscription == null ? null : _subscriptionExtra.Where(s => s.SubscriptionId == subscription.ID).FirstOrDefault();
                            if (db.Services.Where(s => s.Name == item.Name).Any())
                            {
                                currentTariff.Name = currentTariff.Name + "-" + new Random().Next(0, 10000);
                            }
                            var service = db.Services.Add(new SarnetDB.Service()
                            {
                                InfrastructureType = (short)RadiusR.DB.Enums.ServiceInfrastructureTypes.WIFI,
                                BaseQuota = null,
                                QuotaType = null,
                                NoQueue = false,
                                ExpirationTolerance = 0,
                                PaymentTolerance = 0,
                                IsActive = true,
                                Name = currentTariff.Name,
                                RateLimit = currentTariff.UploadLimit.Replace("K", "k") + "/" + currentTariff.DownloadLimit.Replace("K", "k"),
                                Price = Convert.ToDecimal(currentTariff.Price, CultureInfo.InvariantCulture),
                                SmartQuotaMaxPrice = null,
                                SoftQuotaRateLimit = null,
                                BillingType = (short)ConvertParametersUtility.ConvertServiceBillingType(item.TariffType),
                            });
                            db.SaveChanges();
                            Console.WriteLine("Service added");

                            var serviceBillingPeriod = db.ServiceBillingPeriods.Add(new SarnetDB.ServiceBillingPeriod()
                            {
                                DayOfMonth = 1, //short.Parse(item.Day),
                                ServiceID = service.ID
                            });
                            db.SaveChanges();
                            Console.WriteLine("Service billing period added");

                            AddedTariffs.TryAdd(currentTariff.ID, service.ID);
                            //LoadServiceDomain(service);
                        }
                    }

                    Console.WriteLine("Load Service completed");

                }
            }
            catch (Exception ex)
            {
                generalLogger.Error(ex);
            }
        }
        private SarnetDB.RadiusAuthorization LoadRadiusAuthorization(string username, string password, long subscriptionId, DateTime expirationDate)
        {
            try
            {
                Console.WriteLine("LoadRadiusAuthorization started");

                using (SarnetDB.RadiusR_NetSpeed_6Entities db = new SarnetDB.RadiusR_NetSpeed_6Entities())
                {
                    var radiusAuth = db.RadiusAuthorizations.Add(new SarnetDB.RadiusAuthorization()
                    {
                        UsingExpiredPool = false,
                        IsEnabled = true,
                        CLID = null,
                        IsHardQuotaExpired = null,
                        LastInterimUpdate = null,
                        LastLogout = null,
                        ExpirationDate = expirationDate,
                        Password = password,
                        Username = ConvertParametersUtility.CheckRadiusUsernameDomains(username),
                        NASIP = null,
                        RateLimit = null,
                        StaticIP = null,
                        SubscriptionID = subscriptionId
                    });
                    db.SaveChanges();
                    Console.WriteLine("LoadRadiusAuthorization completed");
                    return radiusAuth;
                }
            }
            catch (Exception ex)
            {
                generalLogger.Error(ex);
                throw;
            }
        }
        private SarnetDB.Address LoadBillingAddress(string tempId)
        {
            try
            {
                Console.WriteLine("LoadBillingAddress started");

                var subscriptionExtra = _subscriptionExtra.Where(s => s.SubscriptionId == tempId).FirstOrDefault();
                using (SarnetDB.RadiusR_NetSpeed_6Entities db = new SarnetDB.RadiusR_NetSpeed_6Entities())
                {
                    if (AddedBillingAddress.TryGetValue(tempId, out long registeredAddress))
                    {
                        Console.WriteLine("LoadBillingAddress completed");

                        return db.Addresses.Find(registeredAddress);
                    }
                    var address = db.Addresses.Add(new SarnetDB.Address()
                    {
                        AddressNo = 0,
                        NeighborhoodID = 0,
                        NeighborhoodName = "-",
                        AddressText = subscriptionExtra == null ? "-" : ConvertParametersUtility.CheckEmptyParameter(subscriptionExtra.BillAddress),
                        ApartmentID = 0,
                        ApartmentNo = "-",
                        BuildingName = "-",
                        DistrictID = 0,
                        DoorID = 0,
                        DoorNo = "-",
                        DistrictName = "-",
                        Floor = "-",
                        PostalCode = 0,
                        ProvinceName = "-",
                        ProvinceID = 0,
                        RuralCode = 0,
                        StreetID = 0,
                        StreetName = "-",
                    });
                    db.SaveChanges();
                    if (!AddedBillingAddress.ContainsKey(tempId))
                    {
                        AddedBillingAddress.TryAdd(tempId, address.ID);
                    }
                    Console.WriteLine("LoadBillingAddress completed");
                    return address;
                }
            }
            catch (Exception ex)
            {
                generalLogger.Error(ex);
                throw;
            }
        }
        private void GetRegisteredAddress()
        {
            try
            {
                Console.WriteLine("GetRegisteredAddress");
                using (var db = new SarnetDB.RadiusR_NetSpeed_6Entities())
                {
                    var subscriptions = db.Subscriptions.ToArray();
                    for (int i = 0; i < subscriptions.Length; i++)
                    {
                        //var tempSubscriptions = _subscriptions[i];
                        var currentSubscriptions = subscriptions[i];
                        var tempAddress = currentSubscriptions.Address.AddressText;
                        var address = _subscriptions.Where(s => tempAddress == "-" ? string.IsNullOrEmpty(s.Address)
                        : s.Address == currentSubscriptions.Address.AddressText).FirstOrDefault();
                        AddedAddress.TryAdd(address.ID, currentSubscriptions.AddressID);
                    }
                }
            }
            catch (Exception ex)
            {
                generalLogger.Error(ex);
            }
        }
        private SarnetDB.Address LoadAddress(string tempId)
        {
            try
            {
                Console.WriteLine("Load address started");
                var subscription = _subscriptions.Where(s => s.ID == tempId).FirstOrDefault();
                using (SarnetDB.RadiusR_NetSpeed_6Entities db = new SarnetDB.RadiusR_NetSpeed_6Entities())
                {
                    if (AddedAddress.TryGetValue(tempId, out long registeredAddress))
                    {
                        Console.WriteLine("Load address completed");
                        return db.Addresses.Find(registeredAddress);
                    }
                    var address = db.Addresses.Add(new SarnetDB.Address()
                    {
                        AddressNo = 0,
                        NeighborhoodID = 0,
                        NeighborhoodName = "-",
                        AddressText = ConvertParametersUtility.CheckEmptyParameter(subscription.Address),
                        ApartmentID = 0,
                        ApartmentNo = "-",
                        BuildingName = "-",
                        DistrictID = 0,
                        DoorID = 0,
                        DoorNo = "-",
                        DistrictName = "-",
                        Floor = "-",
                        PostalCode = 0,
                        ProvinceName = "-",
                        ProvinceID = 0,
                        RuralCode = 0,
                        StreetID = 0,
                        StreetName = "-",
                    });
                    db.SaveChanges();
                    if (!AddedAddress.ContainsKey(tempId))
                    {
                        AddedAddress.TryAdd(tempId, address.ID);
                    }
                    Console.WriteLine("Load address completed");
                    return address;
                }
            }
            catch (Exception ex)
            {
                generalLogger.Error(ex);
                throw;
            }
        }
        private SarnetDB.Address LoadCustomerAddress(string tempId)
        {
            try
            {
                Console.WriteLine("Load address started");
                var subscription = _subscriptions.Where(s => s.ID == tempId).FirstOrDefault();
                using (SarnetDB.RadiusR_NetSpeed_6Entities db = new SarnetDB.RadiusR_NetSpeed_6Entities())
                {
                    if (AddedCustomerAddress.TryGetValue(tempId, out long registeredAddress))
                    {
                        Console.WriteLine("Load address completed");
                        return db.Addresses.Find(registeredAddress);
                    }
                    var address = db.Addresses.Add(new SarnetDB.Address()
                    {
                        AddressNo = 0,
                        NeighborhoodID = 0,
                        NeighborhoodName = "-",
                        AddressText = ConvertParametersUtility.CheckEmptyParameter(subscription.Address),
                        ApartmentID = 0,
                        ApartmentNo = "-",
                        BuildingName = "-",
                        DistrictID = 0,
                        DoorID = 0,
                        DoorNo = "-",
                        DistrictName = "-",
                        Floor = "-",
                        PostalCode = 0,
                        ProvinceName = "-",
                        ProvinceID = 0,
                        RuralCode = 0,
                        StreetID = 0,
                        StreetName = "-",
                    });
                    db.SaveChanges();
                    if (!AddedCustomerAddress.ContainsKey(tempId))
                    {
                        AddedCustomerAddress.TryAdd(tempId, address.ID);
                    }
                    Console.WriteLine("Load address completed");
                    return address;
                }
            }
            catch (Exception ex)
            {
                generalLogger.Error(ex);
                throw;
            }
        }
        private SarnetDB.Address LoadPartnerAddress(string tempPartnerId)
        {
            try
            {
                Console.WriteLine("Load partner address started");
                var partner = _partners.Where(s => s.ID == tempPartnerId).FirstOrDefault();
                using (SarnetDB.RadiusR_NetSpeed_6Entities db = new SarnetDB.RadiusR_NetSpeed_6Entities())
                {
                    if (AddedPartnerAddress.TryGetValue(tempPartnerId, out long registeredAddress))
                    {
                        Console.WriteLine("Load partner address completed");
                        return db.Addresses.Find(registeredAddress);
                    }
                    var address = db.Addresses.Add(new SarnetDB.Address()
                    {
                        AddressNo = 0,
                        NeighborhoodID = 0,
                        NeighborhoodName = "-",
                        AddressText = ConvertParametersUtility.CheckEmptyParameter(partner.Address),
                        ApartmentID = 0,
                        ApartmentNo = "-",
                        BuildingName = "-",
                        DistrictID = 0,
                        DoorID = 0,
                        DoorNo = "-",
                        DistrictName = "-",
                        Floor = "-",
                        PostalCode = 0,
                        ProvinceName = "-",
                        ProvinceID = 0,
                        RuralCode = 0,
                        StreetID = 0,
                        StreetName = "-",
                    });
                    db.SaveChanges();
                    if (!AddedPartnerAddress.ContainsKey(tempPartnerId))
                    {
                        AddedPartnerAddress.TryAdd(tempPartnerId, address.ID);

                    }
                    Console.WriteLine("Load partner address completed");
                    return address;
                }
            }
            catch (Exception ex)
            {
                generalLogger.Error(ex);
                throw;
            }
        }
        private void LoadGroups()
        {
            try
            {
                Console.WriteLine("Load groups started");
                using (SarnetDB.RadiusR_NetSpeed_6Entities db = new SarnetDB.RadiusR_NetSpeed_6Entities())
                {
                    if (db.Groups.Count() != _groups.Count())
                    {
                        foreach (var item in _groups)
                        {
                            var group = db.Groups.Add(new SarnetDB.Group()
                            {
                                Name = item.GroupName,
                                IsActive = true
                            });
                            db.SaveChanges();
                            AddedGroups.TryAdd(item.ID, group.ID);
                        }
                    }
                    else
                    {
                        var tempGroup = db.Groups.ToArray();
                        for (int i = 0; i < _groups.Count(); i++)
                        {
                            AddedGroups.TryAdd(_groups[i].ID, tempGroup[i].ID);
                        }
                        Console.WriteLine("Groups load from csv");
                    }
                    Console.WriteLine("Load groups completed");
                }
            }
            catch (Exception ex)
            {
                generalLogger.Error(ex);
            }
        }
        private List<SarnetDB.Bill> TestBills(string tempId, string serviceName)
        {
            try
            {
                Console.WriteLine("Load bills started");
                List<SarnetDB.Bill> billList = new List<SarnetDB.Bill>();
                List<SarnetDB.BillFee> billFees = new List<SarnetDB.BillFee>();
                SarnetDB.EBill eBills = new SarnetDB.EBill();

                foreach (var item in _bills.Where(b => b.SubscriptionId == tempId).ToList())
                {
                    using (SarnetDB.RadiusR_NetSpeed_6Entities db = new SarnetDB.RadiusR_NetSpeed_6Entities())
                    {
                        var tempEBill = _ebills.Where(eb => eb.BillNumber == item.BillNo).FirstOrDefault();
                        if (tempEBill != null)
                        {
                            if (!db.EBills.Where(eb => eb.BillCode == tempEBill.BillNo).Any())
                            {
                                eBills = new SarnetDB.EBill()
                                {
                                    //BillID = bill.ID,
                                    BillCode = tempEBill.BillNo,
                                    ReferenceNo = tempEBill.ReferenceNo,
                                    EBillIssueDate = ConvertParametersUtility.ConvertStringToDatetime(tempEBill.BillIssueDate, CultureInfo.InvariantCulture),
                                    Date = ConvertParametersUtility.ConvertStringToDatetime(tempEBill.Date, CultureInfo.InvariantCulture),
                                    InternalSerialNo = int.Parse(tempEBill.BillNo.Substring(7)),
                                    EBillType = 1
                                };
                                Console.WriteLine("ebill added");
                            }

                        }
                        else
                        {
                            var tempEArchive = _earchive.Where(eb => eb.BillNumber == item.BillNo).FirstOrDefault();
                            if (tempEArchive != null)
                            {
                                if (!db.EBills.Where(eb => eb.BillCode == tempEArchive.BillNo).Any())
                                {
                                    eBills = new SarnetDB.EBill()
                                    {
                                        //BillID = bill.ID,
                                        BillCode = tempEArchive.BillNo,
                                        ReferenceNo = tempEArchive.ReferenceNo,
                                        EBillIssueDate = ConvertParametersUtility.ConvertStringToDatetime(tempEArchive.BillIssueDate, CultureInfo.InvariantCulture),
                                        Date = ConvertParametersUtility.ConvertStringToDatetime(tempEArchive.Date, CultureInfo.InvariantCulture),
                                        InternalSerialNo = int.Parse(tempEArchive.BillNo.Substring(7)),
                                        EBillType = 2
                                    };
                                    Console.WriteLine("earchive added");
                                }
                            }
                        }
                    }
                    billFees.Add(new SarnetDB.BillFee()
                    {
                        FeeTypeID = (short)RadiusR.DB.Enums.FeeType.Tariff,
                        FeeID = null,
                        CurrentCost = Convert.ToDecimal(item.Amount, CultureInfo.InvariantCulture),
                        Description = serviceName,
                        DiscountID = null,
                        StartDate = null,
                        EndDate = null,
                        InstallmentCount = 1,
                    });
                    billList.Add(new SarnetDB.Bill()
                    {
                        AccountantID = null,
                        BillStatusID = (short)ConvertParametersUtility.ConvertBillState(item.State),
                        DueDate = ConvertParametersUtility.ConvertStringToDatetime(item.DueDate, CultureInfo.InvariantCulture),
                        Source = (short)RadiusR.DB.Enums.BillSources.System,
                        PeriodEnd = ConvertParametersUtility.ConvertPeriodEnd(item.BillIssueDate, CultureInfo.InvariantCulture),
                        PeriodStart = ConvertParametersUtility.ConvertStringToDatetime(item.BillIssueDate, CultureInfo.InvariantCulture),
                        PayDate = ConvertParametersUtility.ConvertStringToDatetime(item.PaymentDate, CultureInfo.InvariantCulture),
                        IssueDate = ConvertParametersUtility.ConvertStringToDatetime(item.BillIssueDate, CultureInfo.InvariantCulture),
                        PaymentTypeID = (short)RadiusR.DB.Enums.PaymentType.Cash,
                        EBill = eBills,
                        BillFees = billFees,
                    });
                }
                return billList;
            }
            catch (Exception ex)
            {
                generalLogger.Error(ex);
                return null;
            }
        }
        public void LoadBills(long subscriptionId, string tempId)
        {
            try
            {
                Console.WriteLine("Load bills started");
                using (SarnetDB.RadiusR_NetSpeed_6Entities db = new SarnetDB.RadiusR_NetSpeed_6Entities())
                {
                    foreach (var item in _bills.Where(b => b.SubscriptionId == tempId).ToList())
                    {

                        var bill = db.Bills.Add(new SarnetDB.Bill()
                        {
                            AccountantID = null,
                            BillStatusID = (short)ConvertParametersUtility.ConvertBillState(item.State),
                            DueDate = ConvertParametersUtility.ConvertStringToDatetime(item.DueDate, CultureInfo.InvariantCulture),
                            Source = (short)RadiusR.DB.Enums.BillSources.System,
                            PeriodEnd = ConvertParametersUtility.ConvertPeriodEnd(item.BillIssueDate, CultureInfo.InvariantCulture),
                            PeriodStart = ConvertParametersUtility.ConvertStringToDatetime(item.BillIssueDate, CultureInfo.InvariantCulture),
                            PayDate = ConvertParametersUtility.ConvertStringToDatetime(item.PaymentDate, CultureInfo.InvariantCulture),
                            IssueDate = ConvertParametersUtility.ConvertStringToDatetime(item.BillIssueDate, CultureInfo.InvariantCulture),
                            SubscriptionID = subscriptionId,
                            PaymentTypeID = (short)RadiusR.DB.Enums.PaymentType.Cash,
                        });
                        //db.SaveChanges();
                        Console.WriteLine("bills added");
                        var tariffName = db.Subscriptions.Find(subscriptionId) == null ? "-" : db.Subscriptions.Find(subscriptionId).Service.Name;
                        var billFee = db.BillFees.Add(new SarnetDB.BillFee()
                        {
                            //BillID = bill.ID,
                            FeeTypeID = (short)RadiusR.DB.Enums.FeeType.Tariff,
                            FeeID = null,
                            CurrentCost = Convert.ToDecimal(item.Amount, CultureInfo.InvariantCulture),
                            Description = tariffName,
                            DiscountID = null,
                            StartDate = null,
                            EndDate = null,
                            InstallmentCount = 1,
                            Bill = bill
                        });
                        db.SaveChanges();
                        Console.WriteLine("billfee added");

                        var tempEBill = _ebills.Where(eb => eb.BillNumber == item.BillNo).FirstOrDefault();
                        if (tempEBill != null)
                        {
                            if (!db.EBills.Where(eb => eb.BillCode == tempEBill.BillNo).Any())
                            {
                                var ebill = db.EBills.Add(new SarnetDB.EBill()
                                {
                                    Bill = bill,
                                    //BillID = bill.ID,
                                    BillCode = tempEBill.BillNo,
                                    ReferenceNo = tempEBill.ReferenceNo,
                                    EBillIssueDate = ConvertParametersUtility.ConvertStringToDatetime(tempEBill.BillIssueDate, CultureInfo.InvariantCulture),
                                    Date = ConvertParametersUtility.ConvertStringToDatetime(tempEBill.Date, CultureInfo.InvariantCulture),
                                    InternalSerialNo = int.Parse(tempEBill.BillNo.Substring(7)),
                                    EBillType = 1
                                });
                                Console.WriteLine("ebill added");
                            }

                        }
                        else
                        {
                            var tempEArchive = _earchive.Where(eb => eb.BillNumber == item.BillNo).FirstOrDefault();
                            if (tempEArchive != null)
                            {
                                if (!db.EBills.Where(eb => eb.BillCode == tempEArchive.BillNo).Any())
                                {
                                    var ebill = db.EBills.Add(new SarnetDB.EBill()
                                    {
                                        Bill = bill,
                                        //BillID = bill.ID,
                                        BillCode = tempEArchive.BillNo,
                                        ReferenceNo = tempEArchive.ReferenceNo,
                                        EBillIssueDate = ConvertParametersUtility.ConvertStringToDatetime(tempEArchive.BillIssueDate, CultureInfo.InvariantCulture),
                                        Date = ConvertParametersUtility.ConvertStringToDatetime(tempEArchive.Date, CultureInfo.InvariantCulture),
                                        InternalSerialNo = int.Parse(tempEArchive.BillNo.Substring(7)),
                                        EBillType = 2
                                    });
                                    Console.WriteLine("earchive added");
                                }

                            }
                        }

                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                generalLogger.Error(ex);
            }
        }
        private void LoadNAS()
        {
            try
            {
                using (var db = new SarnetDB.RadiusR_NetSpeed_6Entities())
                {
                    if (db.NAS.Count() != _nas.Count())
                    {
                        foreach (var item in _nas)
                        {
                            db.NAS.Add(new SarnetDB.NA()
                            {
                                TypeID = 1,
                                RadiusIncomingPort = 3799,
                                ApiPort = 8728,
                                NATType = (short)RadiusR.DB.Enums.NATType.Vertical,
                                BackboneNASID = null,
                                Name = item.Name,
                                Disabled = false,
                                Secret = item.Secret,
                                ApiPassword = item.Password,
                                ApiUsername = item.Username,
                                IP = item.IP,
                            });
                            db.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                generalLogger.Error(ex);
            }
        }
        private void LoadSubscriptionGroup(SarnetDB.Subscription subscription, string tempId)
        {
            try
            {
                var tempSubscription = _subscriptions.Where(s => s.ID == tempId).FirstOrDefault();
                var subGroup = AddedGroups[tempSubscription.GroupID];
                using (var db = new SarnetDB.RadiusR_NetSpeed_6Entities())
                {
                    var currentGroup = db.Groups.Find(subGroup);
                    if (!subscription.Groups.Any() && currentGroup != null)
                    {
                        db.Database.ExecuteSqlCommand($"insert into SubscriptionGroup values({subscription.ID},{currentGroup.ID})");
                        //subscription.Groups.Add(currentGroup);
                        Console.WriteLine("Sub Group Added");
                    }
                    else
                    {
                        Console.WriteLine($"Error - {subscription.ID}");
                    }
                }
            }
            catch (Exception ex)
            {
                generalLogger.Error(ex);
            }
        }
        public void GetSubscriptionGroup()
        {
            LoadGroups(); // will remove
            using (var db = new SarnetDB.RadiusR_NetSpeed_6Entities())
            {
                List<DatabaseModels.Subscriptions> subscriptions = new List<DatabaseModels.Subscriptions>();
                var sarNetSubscriptions = GetCustomers();
                foreach (var item in sarNetSubscriptions)
                {
                    foreach (var subs in item.Value)
                    {
                        subscriptions.Add(subs);
                    }
                }
                var dbSubscriptions = db.Subscriptions.ToArray();
                for (int i = 0; i < dbSubscriptions.Length; i++)
                {
                    LoadSubscriptionGroup(dbSubscriptions[i], subscriptions[i].ID);
                }
            }

        }
        public void LoadServiceDomain()
        {
            try
            {
                using (var db = new SarnetDB.RadiusR_NetSpeed_6Entities())
                {
                    foreach (var item in db.Services.ToArray())
                    {
                        if (!item.Domains.Any())
                        {
                            db.Database.ExecuteSqlCommand($"insert into ServiceDomain values({item.ID},{db.Domains.FirstOrDefault().ID})");
                        }
                    }
                    //service.Domains.Add(db.Domains.FirstOrDefault());
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                generalLogger.Error(ex);
            }
        }
        private void LoadSubscriptionNote(SarnetDB.Subscription subscription, string tempId)
        {
            try
            {
                Console.WriteLine("subscription note started");
                using (var db = new SarnetDB.RadiusR_NetSpeed_6Entities())
                {
                    var tempSubscriptionNotes = _subscriptionNotes.Where(s => s.SubscriptionID == tempId).ToList();
                    foreach (var item in tempSubscriptionNotes)
                    {
                        db.SubscriptionNotes.Add(new SarnetDB.SubscriptionNote()
                        {
                            AppUser = db.AppUsers.FirstOrDefault(),
                            Date = ConvertParametersUtility.ConvertStringToDatetime(item.Date, CultureInfo.InvariantCulture),
                            Message = ConvertParametersUtility.CheckEmptyParameter(item.Note),
                            SubscriptionID = subscription.ID,
                            WriterID = db.AppUsers.FirstOrDefault().ID
                        });
                        db.SaveChanges();
                        Console.WriteLine("subscription note added");
                    }
                    foreach (var item in _paymentNotes.Where(p => p.SubscriptionId == tempId).ToArray())
                    {
                        db.SubscriptionNotes.Add(new SarnetDB.SubscriptionNote()
                        {
                            AppUser = db.AppUsers.FirstOrDefault(),
                            Date = ConvertParametersUtility.ConvertStringToDatetime(DateTime.Now.ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture),
                            Message = ConvertParametersUtility.CheckEmptyParameter(item.Note),
                            SubscriptionID = subscription.ID,
                            WriterID = db.AppUsers.FirstOrDefault().ID
                        });
                        db.SaveChanges();
                        Console.WriteLine("subscription note added");
                    }

                }
                Console.WriteLine("subscription note completed");
            }
            catch (Exception ex)
            {
                generalLogger.Error(ex);
            }
        }
        //private string GetBillNote(string billNumber)
        //{
        //    try
        //    {
        //        Console.WriteLine("GetBillNote started");
        //        var tempBill = _paymentNotes.Where(p => p.BillNumber == billNumber).FirstOrDefault();
        //        if (tempBill == null)
        //        {
        //            Console.WriteLine("GetBillNote completed");

        //            return "-";
        //        }
        //        Console.WriteLine("GetBillNote completed");

        //        return string.IsNullOrEmpty(tempBill.Note) ? "-" : tempBill.Note;
        //    }
        //    catch (Exception ex)
        //    {
        //        generalLogger.Error(ex);
        //        throw;
        //    }
        //}
        private void LoadAgentTariff()
        {
            using (var db = new SarnetDB.RadiusR_NetSpeed_6Entities())
            {
                var currentDomainID = db.Domains.FirstOrDefault().ID;
                foreach (var item in _agentTariffs)
                {
                    try
                    {
                        if (AddedAgent.TryGetValue(item.AgentId, out long CurrentAgentID))
                        {
                            if (AddedTariffs.TryGetValue(item.AgentTariffId, out long CurrentTariffID))
                            {
                                //var hasAgentTariff = db.Agents.Join(db.Services,
                                //    agent => agent.ID,
                                //    service => service.ID,
                                //    (agent, service) => new { Agents = agent, Services = service })
                                //    .Where((x) => x.Agents.ID == CurrentAgentID && x.Services.ID == CurrentTariffID).Any();
                                if (!db.Agents.Where(a => a.AgentTariffs.Select(s => s.Service.ID).Contains((int)CurrentTariffID) && a.ID == CurrentAgentID).Any())
                                {
                                    db.Database.ExecuteSqlCommand($"insert into AgentTariff values({CurrentAgentID},{CurrentTariffID},{currentDomainID})");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        generalLogger.Error(ex);
                    }

                }
            }
        }
        private void LoadAgent()
        {
            try
            {
                Console.WriteLine("Load agent started");

                using (var db = new SarnetDB.RadiusR_NetSpeed_6Entities())
                {
                    if (_partners.Count() == db.Agents.Count())
                    {
                        var tempAgent = db.Agents.ToArray();
                        for (int i = 0; i < _partners.Count(); i++)
                        {
                            AddedAgent.TryAdd(_partners[i].ID, tempAgent[i].ID);
                        }
                    }
                    else
                    {
                        foreach (var item in _partners)
                        {
                            var agent = db.Agents.Add(new SarnetDB.Agent()
                            {
                                AddressID = LoadPartnerAddress(item.ID).ID,
                                ExecutiveName = ConvertParametersUtility.CheckEmptyParameter(item.FullName),
                                CompanyTitle = ConvertParametersUtility.CheckEmptyParameter(item.CompanyTitle),
                                Allowance = 0.1000m,
                                IsEnabled = true,
                                TaxOffice = ConvertParametersUtility.CheckEmptyParameter(item.TaxAdministration),
                                TaxNo = ConvertParametersUtility.CheckEmptyParameter(item.TaxNo),
                                Email = ConvertParametersUtility.GenerateEmail(),
                                Password = ConvertParametersUtility.GeneratePassword(),
                                CustomerSetupUserID = AddCustomerSetupUser(item.CompanyTitle).ID,
                                PhoneNo = ConvertParametersUtility.ConvertPhoneNo(item.PhoneNo)
                            });
                            db.SaveChanges();
                            AddedAgent.TryAdd(item.ID, agent.ID);
                            Console.WriteLine("Load agent added");
                        }
                    }
                }
                Console.WriteLine("Load agent completed");

            }
            catch (Exception ex)
            {
                generalLogger.Error(ex);
            }
        }
        private SarnetDB.CustomerSetupUser AddCustomerSetupUser(string title)
        {
            try
            {
                Console.WriteLine("AddCustomerSetupUser started");

                using (var db = new SarnetDB.RadiusR_NetSpeed_6Entities())
                {
                    var customerSetupUser = db.CustomerSetupUsers.Add(new SarnetDB.CustomerSetupUser()
                    {
                        IsEnabled = true,
                        Name = title,
                        Password = ConvertParametersUtility.GeneratePassword(),
                        Username = ConvertParametersUtility.GenerateUsername(),
                    });
                    db.SaveChanges();
                    Console.WriteLine("SetupUser added");

                    return customerSetupUser;
                }
            }
            catch (Exception ex)
            {
                generalLogger.Error(ex);
                throw;
            }
        }
        private void ClearAll()
        {
            using (var db = new SarnetDB.RadiusR_NetSpeed_6Entities())
            {
                //db.EBills.RemoveRange(db.EBills.ToArray());
                db.Database.ExecuteSqlCommand("TRUNCATE TABLE [EBill]");
                //db.ServiceBillingPeriods.RemoveRange(db.ServiceBillingPeriods.ToArray());
                db.Database.ExecuteSqlCommand("TRUNCATE TABLE [ServiceBillingPeriod]");
                //db.RadiusAuthorizations.RemoveRange(db.RadiusAuthorizations.ToArray());
                db.Database.ExecuteSqlCommand("TRUNCATE TABLE [RadiusAuthorization]");
                //db.BillFees.RemoveRange(db.BillFees.ToArray());
                db.Database.ExecuteSqlCommand("TRUNCATE TABLE [BillFee]");
                //db.Bills.RemoveRange(db.Bills.ToArray());
                db.Database.ExecuteSqlCommand("DELETE FROM [Bill]");
                //db.Database.ExecuteSqlCommand("TRUNCATE TABLE [Bill]");
                //db.CustomerIDCards.RemoveRange(db.CustomerIDCards.ToArray());
                db.Database.ExecuteSqlCommand("TRUNCATE TABLE [CustomerIDCard]");
                //db.SubscriptionNotes.RemoveRange(db.SubscriptionNotes.ToArray());
                db.Database.ExecuteSqlCommand("TRUNCATE TABLE [SubscriptionNote]");
                db.Subscriptions.RemoveRange(db.Subscriptions.ToArray());
                //db.Database.ExecuteSqlCommand("TRUNCATE TABLE [Subscription]");
                db.Customers.RemoveRange(db.Customers.ToArray());
                //db.Database.ExecuteSqlCommand("TRUNCATE TABLE [Customer]");
                db.Services.RemoveRange(db.Services.ToArray());
                //db.Database.ExecuteSqlCommand("TRUNCATE TABLE [Service]");
                db.Agents.RemoveRange(db.Agents.ToArray());
                //db.Database.ExecuteSqlCommand("TRUNCATE TABLE [Agent]");
                db.CustomerSetupUsers.RemoveRange(db.CustomerSetupUsers.ToArray());
                //db.Database.ExecuteSqlCommand("TRUNCATE TABLE [CustomerSetupUser]");
                db.SaveChanges();
            }
        }
    }
}
