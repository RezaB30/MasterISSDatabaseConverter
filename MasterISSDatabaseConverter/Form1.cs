using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MasterISSDatabaseConverter
{
    public partial class Form1 : Form
    {
        public Dictionary<string, TableType> TablePaths = new Dictionary<string, TableType>();
        public string filePath { get; set; }
        public string fileName { get; set; }
        public List<DatabaseModels.Partners> Partners = new List<DatabaseModels.Partners>();
        public List<DatabaseModels.Subscriptions> Subscriptions = new List<DatabaseModels.Subscriptions>();
        public List<DatabaseModels.SubscriptionsExtra> SubscriptionExtras = new List<DatabaseModels.SubscriptionsExtra>();
        public List<DatabaseModels.Bills> Bills = new List<DatabaseModels.Bills>();
        public List<DatabaseModels.EArchive> EArchives = new List<DatabaseModels.EArchive>();
        public List<DatabaseModels.EBill> EBills = new List<DatabaseModels.EBill>();
        public List<DatabaseModels.Groups> Groups = new List<DatabaseModels.Groups>();
        public List<DatabaseModels.NAS> NAS = new List<DatabaseModels.NAS>();
        public List<DatabaseModels.PaymentNotes> PaymentNotes = new List<DatabaseModels.PaymentNotes>();
        public List<DatabaseModels.SubscriptionNotes> SubscriptionNotes = new List<DatabaseModels.SubscriptionNotes>();
        public List<DatabaseModels.Tariffs> Tariffs = new List<DatabaseModels.Tariffs>();
        public List<DatabaseModels.RadCheck> RadChecks = new List<DatabaseModels.RadCheck>();
        public List<DatabaseModels.AgentTariff> AgentTariffs = new List<DatabaseModels.AgentTariff>();
        public List<DatabaseModels.RegisteredAddresses> RegisteredAddresses = new List<DatabaseModels.RegisteredAddresses>();

        public Form1()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            TablePaths.Add(@"C:\Users\user\Desktop\Sarnet Raw Data\tbliss_bayiler.csv", TableType.Partners);
            TablePaths.Add(@"C:\Users\user\Desktop\Sarnet Raw Data\radcheck.csv", TableType.RadCheck);
            TablePaths.Add(@"C:\Users\user\Desktop\Sarnet Raw Data\tbliss_earsiv.csv", TableType.EArchive);
            TablePaths.Add(@"C:\Users\user\Desktop\Sarnet Raw Data\tbliss_efatura.csv", TableType.EBill);
            TablePaths.Add(@"C:\Users\user\Desktop\Sarnet Raw Data\tbliss_fatura.csv", TableType.Bill);
            TablePaths.Add(@"C:\Users\user\Desktop\Sarnet Raw Data\tbliss_grup.csv", TableType.Group);
            TablePaths.Add(@"C:\Users\user\Desktop\Sarnet Raw Data\tbliss_m_notlar.csv", TableType.SubscriptionNotes);
            TablePaths.Add(@"C:\Users\user\Desktop\Sarnet Raw Data\tbliss_nas.csv", TableType.NAS);
            TablePaths.Add(@"C:\Users\user\Desktop\Sarnet Raw Data\tbliss_odeme_notu.csv", TableType.PaymentNotes);
            TablePaths.Add(@"C:\Users\user\Desktop\Sarnet Raw Data\tbliss_tarifeler.csv", TableType.Tariffs);
            TablePaths.Add(@"C:\Users\user\Desktop\Sarnet Raw Data\tbliss_uyeler.csv", TableType.Subscriptions);
            TablePaths.Add(@"C:\Users\user\Desktop\Sarnet Raw Data\tbliss_uyeler_ek.csv", TableType.SubscriptionsExtra);
            TablePaths.Add(@"C:\Users\user\Desktop\Sarnet Raw Data\tbliss_bayi_tarife.csv", TableType.AgentTariff);
        }

        //private void chooseFile_btn_Click(object sender, EventArgs e)
        //{
        //    OpenFileDialog fileDialog = new OpenFileDialog();
        //    if (fileDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        fileName = fileDialog.SafeFileName;
        //        filePath = fileDialog.FileName;
        //        selectedFileNameLbl.Text = fileName;
        //    }
        //}

        private void converter_btn_Click(object sender, EventArgs e)
        {
            backgroundWorker2.RunWorkerAsync();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            process_listbox.Items.Clear();
            DBConvertUtility convertUtility = new DBConvertUtility(Subscriptions, Groups, Bills, EArchives, EBills, NAS, Partners,
                PaymentNotes, SubscriptionNotes, SubscriptionExtras, Tariffs, AgentTariffs);
            convertUtility.LoadDatabaseTables(progressBar1);
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                foreach (var keyValuePair in TablePaths)
                {
                    //Enum.TryParse<TableType>(TableTypes_Combobox.SelectedValue.ToString(), out TableType selectedTableType);
                    ////C:\Users\user\Desktop\Sarnet Raw Data\tbliss_bayiler.csv
                    var readLines = FileUtility.ReadFile(keyValuePair.Key);
                    process_listbox.Items.Add("Process Started...");
                    foreach (var item in readLines)
                    {
                        var dataList = RegexConverter.GetDatas(item, keyValuePair.Value);
                        if (dataList == null)
                        {
                            process_listbox.Items.Add("Error while convert item...");
                        }
                        else
                        {
                            //process_listbox.Items.Add(string.Join(" ", dataList.ToArray()));
                            switch (keyValuePair.Value)
                            {
                                case TableType.Partners:
                                    {
                                        if (dataList.Count() != 13)
                                        {
                                            process_listbox.Items.Add($"Wrong data count : {dataList.Count()}");
                                        }
                                        DBUtility.AddPartners(dataList, Partners);
                                        //process_listbox.Items.Add(string.Join(" ", dataList));
                                    }
                                    break;
                                case TableType.EArchive:
                                    {
                                        if (dataList.Count() != 15)
                                        {
                                            process_listbox.Items.Add($"Wrong data count : {dataList.Count()}");
                                        }
                                        DBUtility.AddEArchive(dataList, EArchives);
                                    }
                                    break;
                                case TableType.EBill:
                                    {
                                        if (dataList.Count() != 15)
                                        {
                                            process_listbox.Items.Add($"Wrong data count : {dataList.Count()}");
                                        }
                                        DBUtility.AddEBill(dataList, EBills);
                                    }
                                    break;
                                case TableType.Bill:
                                    {
                                        if (dataList.Count() != 21)
                                        {
                                            process_listbox.Items.Add($"Wrong data count : {dataList.Count()}");
                                        }
                                        DBUtility.AddBill(dataList, Bills);
                                    }
                                    break;
                                case TableType.Group:
                                    {
                                        if (dataList.Count() != 3)
                                        {
                                            process_listbox.Items.Add($"Wrong data count : {dataList.Count()}");
                                        }
                                        DBUtility.AddGroup(dataList, Groups);
                                    }
                                    break;
                                case TableType.SubscriptionNotes:
                                    {
                                        if (dataList.Count() != 6)
                                        {
                                            process_listbox.Items.Add($"Wrong data count : {dataList.Count()}");
                                        }
                                        DBUtility.AddSubscriptionNotes(dataList, SubscriptionNotes);
                                    }
                                    break;
                                case TableType.NAS:
                                    {
                                        if (dataList.Count() != 8)
                                        {
                                            process_listbox.Items.Add($"Wrong data count : {dataList.Count()}");
                                        }
                                        DBUtility.AddNAS(dataList, NAS);
                                    }
                                    break;
                                case TableType.PaymentNotes:
                                    {
                                        if (dataList.Count() != 3)
                                        {
                                            process_listbox.Items.Add($"Wrong data count : {dataList.Count()}");
                                        }
                                        DBUtility.AddPaymentNotes(dataList, PaymentNotes);
                                    }
                                    break;
                                case TableType.Tariffs:
                                    {
                                        if (dataList.Count() != 39)
                                        {
                                            process_listbox.Items.Add($"Wrong data count : {dataList.Count()}");
                                        }
                                        DBUtility.AddTariffs(dataList, Tariffs);
                                    }
                                    break;
                                case TableType.Subscriptions:
                                    {
                                        if (dataList.Count() != 76)
                                        {
                                            process_listbox.Items.Add($"Wrong data count : {dataList.Count()}");
                                        }
                                        DBUtility.AddSubscriptions(dataList, Subscriptions);
                                    }
                                    break;
                                case TableType.SubscriptionsExtra:
                                    {
                                        if (dataList.Count() != 18)
                                        {
                                            process_listbox.Items.Add($"Wrong data count : {dataList.Count()}");
                                        }
                                        DBUtility.AddSubscriptionExtras(dataList, SubscriptionExtras);
                                    }
                                    break;
                                case TableType.RadCheck:
                                    {
                                        if (dataList.Count() != 5)
                                        {
                                            process_listbox.Items.Add($"Wrong data count : {dataList.Count()}");
                                        }
                                        DBUtility.AddRadCheck(dataList, RadChecks);
                                    }
                                    break;
                                case TableType.AgentTariff:
                                    {
                                        if (dataList.Count() != 3)
                                        {
                                            process_listbox.Items.Add($"Wrong data count : {dataList.Count()}");
                                        }
                                        DBUtility.AddAgentTariff(dataList, AgentTariffs);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    completedTables_listbox.Items.Add(keyValuePair.Value.ToString());
                    process_listbox.Items.Add("Process Finished...");
                }
                MessageBox.Show("Completed");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void subGroup_btn_Click(object sender, EventArgs e)
        {
            subGroupBgWorker.RunWorkerAsync();
        }

        //private void serviceDomain_btn_Click(object sender, EventArgs e)
        //{
        //    serviceDomainBgWorker.RunWorkerAsync();
        //}

        private void subGroupBgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            DBConvertUtility convertUtility = new DBConvertUtility(Subscriptions, Groups, Bills, EArchives, EBills, NAS, Partners,
                PaymentNotes, SubscriptionNotes, SubscriptionExtras, Tariffs, AgentTariffs);
            convertUtility.GetSubscriptionGroup();
            MessageBox.Show("Subscription groups loaded");
        }

        //private void serviceDomainBgWorker_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    DBConvertUtility convertUtility = new DBConvertUtility(Subscriptions, Groups, Bills, EArchives, EBills, NAS, Partners,
        //        PaymentNotes, SubscriptionNotes, SubscriptionExtras, Tariffs, AgentTariffs);
        //    convertUtility.LoadServiceDomain();
        //}

        private void copyAddress_btn_Click(object sender, EventArgs e)
        {
            backgroundWorker3.RunWorkerAsync();
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

        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            var addressTablePath = @"C:\Users\user\Desktop\Sarnet Raw Data\Sarnet_Address_Datas.csv";
            var readLines = FileUtility.ReadFile(addressTablePath);
            foreach (var item in readLines)
            {
                var dataList = item.Split(',').ToList();
                DBUtility.CopyAddresses(dataList, RegisteredAddresses);

            }
            var sarNetDb = new SarnetDB.RadiusR_NetSpeed_6Entities();
            var failCount = 0;
            var subscriptions = sarNetDb.Subscriptions.ToArray();
            progressBar1.Value = 0;
            progressBar1.Maximum = subscriptions.Length;
            foreach (var item in subscriptions)
            {
                try
                {
                    using (var db = new SarnetDB.RadiusR_NetSpeed_6Entities())
                    {
                        var tempAddress = RegisteredAddresses.Where(ra => ra.RadiusUsername == item.RadiusAuthorization.Username).FirstOrDefault();
                        if (tempAddress != null)
                        {
                            var CurrentAddress = db.Addresses.Find(item.AddressID);
                            if (CurrentAddress != null)
                            {
                                CurrentAddress.AddressNo = Convert.ToInt64(tempAddress.AddressNo);
                                CurrentAddress.AddressText = tempAddress.AddressText;
                                CurrentAddress.ApartmentID = Convert.ToInt64(tempAddress.ApartmentID);
                                CurrentAddress.ApartmentNo = tempAddress.ApartmentNo;
                                CurrentAddress.BuildingName = tempAddress.BuildingName;
                                CurrentAddress.DistrictID = Convert.ToInt64(tempAddress.DistrictID);
                                CurrentAddress.DistrictName = tempAddress.DistrictName;
                                CurrentAddress.DoorID = Convert.ToInt64(tempAddress.DoorID);
                                CurrentAddress.DoorNo = tempAddress.DoorNo;
                                CurrentAddress.Floor = tempAddress.Floor;
                                CurrentAddress.NeighborhoodID = Convert.ToInt64(tempAddress.NeighborhoodID);
                                CurrentAddress.NeighborhoodName = tempAddress.NeighborhoodName;
                                CurrentAddress.PostalCode = Convert.ToInt32(tempAddress.PostalCode);
                                CurrentAddress.ProvinceID = Convert.ToInt64(tempAddress.ProvinceID);
                                CurrentAddress.ProvinceName = tempAddress.ProvinceName;
                                CurrentAddress.RuralCode = Convert.ToInt64(tempAddress.RuralCode);
                                CurrentAddress.StreetID = Convert.ToInt64(tempAddress.StreetID);
                                CurrentAddress.StreetName = tempAddress.StreetName;
                            }
                            CurrentAddress = db.Addresses.Find(item.Customer.BillingAddressID);
                            if (CurrentAddress != null)
                            {
                                CurrentAddress.AddressNo = Convert.ToInt64(tempAddress.AddressNo);
                                CurrentAddress.AddressText = tempAddress.AddressText;
                                CurrentAddress.ApartmentID = Convert.ToInt64(tempAddress.ApartmentID);
                                CurrentAddress.ApartmentNo = tempAddress.ApartmentNo;
                                CurrentAddress.BuildingName = tempAddress.BuildingName;
                                CurrentAddress.DistrictID = Convert.ToInt64(tempAddress.DistrictID);
                                CurrentAddress.DistrictName = tempAddress.DistrictName;
                                CurrentAddress.DoorID = Convert.ToInt64(tempAddress.DoorID);
                                CurrentAddress.DoorNo = tempAddress.DoorNo;
                                CurrentAddress.Floor = tempAddress.Floor;
                                CurrentAddress.NeighborhoodID = Convert.ToInt64(tempAddress.NeighborhoodID);
                                CurrentAddress.NeighborhoodName = tempAddress.NeighborhoodName;
                                CurrentAddress.PostalCode = Convert.ToInt32(tempAddress.PostalCode);
                                CurrentAddress.ProvinceID = Convert.ToInt64(tempAddress.ProvinceID);
                                CurrentAddress.ProvinceName = tempAddress.ProvinceName;
                                CurrentAddress.RuralCode = Convert.ToInt64(tempAddress.RuralCode);
                                CurrentAddress.StreetID = Convert.ToInt64(tempAddress.StreetID);
                                CurrentAddress.StreetName = tempAddress.StreetName;
                            }
                            CurrentAddress = db.Addresses.Find(item.Customer.AddressID);
                            if (CurrentAddress != null)
                            {
                                CurrentAddress.AddressNo = Convert.ToInt64(tempAddress.AddressNo);
                                CurrentAddress.AddressText = tempAddress.AddressText;
                                CurrentAddress.ApartmentID = Convert.ToInt64(tempAddress.ApartmentID);
                                CurrentAddress.ApartmentNo = tempAddress.ApartmentNo;
                                CurrentAddress.BuildingName = tempAddress.BuildingName;
                                CurrentAddress.DistrictID = Convert.ToInt64(tempAddress.DistrictID);
                                CurrentAddress.DistrictName = tempAddress.DistrictName;
                                CurrentAddress.DoorID = Convert.ToInt64(tempAddress.DoorID);
                                CurrentAddress.DoorNo = tempAddress.DoorNo;
                                CurrentAddress.Floor = tempAddress.Floor;
                                CurrentAddress.NeighborhoodID = Convert.ToInt64(tempAddress.NeighborhoodID);
                                CurrentAddress.NeighborhoodName = tempAddress.NeighborhoodName;
                                CurrentAddress.PostalCode = Convert.ToInt32(tempAddress.PostalCode);
                                CurrentAddress.ProvinceID = Convert.ToInt64(tempAddress.ProvinceID);
                                CurrentAddress.ProvinceName = tempAddress.ProvinceName;
                                CurrentAddress.RuralCode = Convert.ToInt64(tempAddress.RuralCode);
                                CurrentAddress.StreetID = Convert.ToInt64(tempAddress.StreetID);
                                CurrentAddress.StreetName = tempAddress.StreetName;
                            }
                            if (item.Customer.CorporateCustomerInfo != null)
                            {
                                CurrentAddress = db.Addresses.Find(item.Customer.CorporateCustomerInfo.CompanyAddressID);
                                if (CurrentAddress != null)
                                {
                                    CurrentAddress.AddressNo = Convert.ToInt64(tempAddress.AddressNo);
                                    CurrentAddress.AddressText = tempAddress.AddressText;
                                    CurrentAddress.ApartmentID = Convert.ToInt64(tempAddress.ApartmentID);
                                    CurrentAddress.ApartmentNo = tempAddress.ApartmentNo;
                                    CurrentAddress.BuildingName = tempAddress.BuildingName;
                                    CurrentAddress.DistrictID = Convert.ToInt64(tempAddress.DistrictID);
                                    CurrentAddress.DistrictName = tempAddress.DistrictName;
                                    CurrentAddress.DoorID = Convert.ToInt64(tempAddress.DoorID);
                                    CurrentAddress.DoorNo = tempAddress.DoorNo;
                                    CurrentAddress.Floor = tempAddress.Floor;
                                    CurrentAddress.NeighborhoodID = Convert.ToInt64(tempAddress.NeighborhoodID);
                                    CurrentAddress.NeighborhoodName = tempAddress.NeighborhoodName;
                                    CurrentAddress.PostalCode = Convert.ToInt32(tempAddress.PostalCode);
                                    CurrentAddress.ProvinceID = Convert.ToInt64(tempAddress.ProvinceID);
                                    CurrentAddress.ProvinceName = tempAddress.ProvinceName;
                                    CurrentAddress.RuralCode = Convert.ToInt64(tempAddress.RuralCode);
                                    CurrentAddress.StreetID = Convert.ToInt64(tempAddress.StreetID);
                                    CurrentAddress.StreetName = tempAddress.StreetName;
                                }
                            }
                            db.SaveChanges();
                            Console.WriteLine($"Address changed - {tempAddress.RadiusUsername}");
                        }
                        else
                        {
                            failCount++;
                            Console.WriteLine($"Address not found - {item.RadiusAuthorization.Username}");
                        }
                    }
                    progressBar1.Value++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception ------------");
                }

            }
            MessageBox.Show("Copy Address Completed. Fail Address Count : " + failCount + "");
        }

        private void radiusAuth_btn_Click(object sender, EventArgs e)
        {
            backgroundWorker4.RunWorkerAsync();
        }

        private void backgroundWorker4_DoWork(object sender, DoWorkEventArgs e)
        {
            DBConvertUtility convertUtility = new DBConvertUtility(Subscriptions, Groups, Bills, EArchives, EBills, NAS, Partners,
                PaymentNotes, SubscriptionNotes, SubscriptionExtras, Tariffs, AgentTariffs);
            var customers = convertUtility.GetCustomers();
            //using (var db = new SarnetDB.RadiusR_Netspeed_5Entities())
            //{
            //    db.Configuration.AutoDetectChangesEnabled = false;
            //    var radiusAuth = db.RadiusAuthorizations.ToList();
            //    var tempSubscriptions = new List<DatabaseModels.Subscriptions>();
            //    foreach (var item in customers)
            //    {
            //        tempSubscriptions.AddRange(item.Value);
            //    }
            //    for (int i = 0; i < radiusAuth.Count(); i++)
            //    {
            //        db.Database.ExecuteSqlCommand("update RadiusAuthorization set ExpirationDate=@expDate where SubscriptionID = @id",
            //            new[] { new SqlParameter("@expDate", tempSubscriptions[i].ExpirationDate), new SqlParameter("@id", radiusAuth[i].SubscriptionID) });
            //        //db.Database.ExecuteSqlCommand("update RadiusAuthorization set Username= @username where SubscriptionID = @id",
            //        //    new[] { new SqlParameter("@username", tempSubscriptions[i].RadiusUsername), new SqlParameter("@id", radiusAuth[i].SubscriptionID) });
            //        Console.WriteLine($"Radius Authorization Updated - {tempSubscriptions[i].RadiusUsername}");
            //    }
            //    Console.WriteLine("Finished");
            //}

            //payment day
            //using (var db = new SarnetDB.RadiusR_NetSpeed_6Entities())
            //{
            //    db.Configuration.AutoDetectChangesEnabled = false;
            //    var subscriptions = db.Subscriptions.ToList();
            //    var tempSubscriptions = new List<DatabaseModels.Subscriptions>();
            //    foreach (var item in customers)
            //    {
            //        tempSubscriptions.AddRange(item.Value);
            //    }
            //    for (int i = 0; i < subscriptions.Count(); i++)
            //    {
            //        db.Database.ExecuteSqlCommand("update Subscription set PaymentDay=@pDay where ID = @id",
            //            new[] { new SqlParameter("@pDay", ConvertParametersUtility.ConvertPaymentDay(tempSubscriptions[i].BillingPeriod)), new SqlParameter("@id", subscriptions[i].ID) });
            //        //db.Database.ExecuteSqlCommand("update RadiusAuthorization set Username= @username where SubscriptionID = @id",
            //        //    new[] { new SqlParameter("@username", tempSubscriptions[i].RadiusUsername), new SqlParameter("@id", radiusAuth[i].SubscriptionID) });
            //        Console.WriteLine($"Subscription Updated - {tempSubscriptions[i].RadiusUsername}");
            //    }
            //    Console.WriteLine("Finished");
            //}
            // ---------------             
        }
    }
}
