using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterISSDatabaseConverter
{
    public static class FileUtility
    {
        public static List<string> ReadFile(string path , bool hasHeader = true)
        {
            using (var reader = new StreamReader(path))
            {
                List<string> tempList = new List<string>();
                while (!reader.EndOfStream)
                {                    
                    tempList.Add(reader.ReadLine());
                }
                if (hasHeader)
                {
                    tempList.RemoveAt(0);
                }
                return tempList;
            }
        }
    }
}
