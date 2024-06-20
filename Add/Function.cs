using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Add
{
    public static class Function
    {
        //יצירת מילון שיכיל את כלל התחלופות בין המופיע בקבצים לבין הנדרש כרגע
        public static Dictionary<string,string> n = new Dictionary<string,string>();
        static Function()
        {
            //הגדרות ברירת מחדל
            n.Add("Repository_Dal", "DAL");
            n.Add("Service_BLL", "BLL");
            n.Add("Common_DTO", "DTO");
            n.Add("WebApi", "WebApi");
        }

        public static string interfaceDal()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"files\IRepository.txt");
            string res = File.ReadAllText(path);
            foreach (var s in n)
            {
                res=res.Replace(s.Key, s.Value);
            }
            return res;
        }

        public static string functionDal()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"files\Repository.txt");
            string res = File.ReadAllText(path);
            foreach (var s in n)
            {
                res=res.Replace(s.Key, s.Value);
            }
            return res;
        }

        public static string interfaceBll()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"files\IService.txt");
            string res = File.ReadAllText(path);
            foreach (var s in n)
            {
                res=res.Replace(s.Key, s.Value);
            }
            return res;
        }

        public static string functionBll()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"files\Service.txt");
            string res = File.ReadAllText(path);
            foreach (var s in n)
            {
                res=res.Replace(s.Key, s.Value);
            }
            return res;
        }

        public static string controller()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"files\Controller.txt");
            string res = File.ReadAllText(path);
            foreach (var s in n)
            {
                res=res.Replace(s.Key, s.Value);
            }
            return res;
        }
    }
}
