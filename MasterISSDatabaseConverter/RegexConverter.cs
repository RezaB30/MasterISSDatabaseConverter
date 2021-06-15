using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MasterISSDatabaseConverter
{
    public static class RegexConverter
    {
        public static List<string> GetDatas(string plainText, TableType? patternType)
        {
            try
            {
                var pattern = "";
                switch (patternType)
                {
                    case TableType.Partners:
                        {
                            pattern = @"(\""(.*?)"")|(NULL)|((?<=,|^),)|(,(?=$))";
                        }
                        break;
                    case TableType.EArchive:
                        {
                            plainText = plainText.Replace("Kontrol: Yetki kontrolÃ¼ yapÄ±lamadÄ±! LÃ¼tfen, daha sonra tekrar deneyiniz...  ", "Kontrol: Yetki kontrolÃ¼ yapÄ±lamadÄ±! LÃ¼tfen daha sonra tekrar deneyiniz...  ");
                            plainText = plainText.Replace("InnerException, if present,", "InnerException if present");
                            plainText = plainText.Replace("Lütfen, daha sonra tekrar deneyiniz", "Lütfen daha sonra tekrar deneyiniz");
                            pattern = @"((.*?),)|(\""(.*?)"",)|(NULL)|((?<=,|^),)|(,(?=$))";
                        }
                        break;
                    case TableType.EBill:
                        {
                            plainText = plainText.Replace("Kontrol: Yetki kontrolÃ¼ yapÄ±lamadÄ±! LÃ¼tfen, daha sonra tekrar deneyiniz...  ", "Kontrol: Yetki kontrolÃ¼ yapÄ±lamadÄ±! LÃ¼tfen daha sonra tekrar deneyiniz...  ");
                            plainText = plainText.Replace("InnerException, if present,", "InnerException if present");
                            plainText = plainText.Replace("Lütfen, daha sonra tekrar deneyiniz", "Lütfen daha sonra tekrar deneyiniz");
                            pattern = @"((.*?),)|(\""(.*?)"",)|(NULL)|((?<=,|^),)|(,(?=$))";
                        }
                        break;
                    case TableType.Bill:
                        {
                            return plainText.Split(',').Select(s=>s.Replace("\"","")).ToList();
                        }
                    case TableType.Group:
                        {
                            pattern = @"(\""(.*?)"")|(NULL)|((?<=,|^),)|(,(?=$))";
                        }
                        break;
                    case TableType.SubscriptionNotes:
                        {
                            pattern = @"(\""(.*?)"")|(NULL)|((?<=,|^),)|(,(?=$))";
                        }
                        break;
                    case TableType.NAS:
                        {
                            pattern = @"(\""(.*?)"")|(NULL)|((?<=,|^),)|(,(?=$))";
                        }
                        break;
                    case TableType.PaymentNotes:
                        {
                            pattern = @"(\""(.*?)"")|(NULL)|((?<=,|^),)|(,(?=$))";
                        }
                        break;
                    case TableType.Tariffs:
                        {
                            pattern = @"(\""(.*?)"")|(NULL)|((?<=,|^),)|(,(?=$))";
                        }
                        break;
                    case TableType.Subscriptions:
                        {
                            pattern = @"(\""(.*?)"",)|(NULL)|((?<=,|^),)|(,(?=$))";
                        }
                        break;
                    case TableType.SubscriptionsExtra:
                        {
                            pattern = @"(\""(.*?)"")|(NULL)|((?<=,|^),)|(,(?=$))";
                        }
                        break;
                    case TableType.RadCheck:
                        {
                            pattern = @"(\""(.*?)"")|(NULL)|((?<=,|^),)|(,(?=$))";
                        }
                        break;
                    case TableType.AgentTariff:
                        {
                            pattern = @"(\""(.*?)"")|(NULL)|((?<=,|^),)|(,(?=$))";
                        }
                        break;
                    default:
                        {
                            pattern = @"(\""(.*?)"")|(NULL)|((?<=,|^),)|(,(?=$))";
                        }
                        break;
                }
                var matches = Regex.Matches(plainText, pattern).Cast<Match>().ToList();
                var values = matches.Select(m => m.Groups[2]).ToList();
                return values.Select(v => v.Value).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
