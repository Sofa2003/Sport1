using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Personal
{
    public class Program
    {
        static void Main(string[] args)
        {
           
        }
        public int KodPersonal { get; set; }
        public string Имя { get; set; }
        public string Фамилия { get; set; }
        public string Отчество { get; set; }
        public Nullable<System.DateTime> ДатаРождения { get; set; }
        public string НомерТелефона { get; set; }
        public int Должность { get; set; }
        public Nullable<int> Стаж { get; set; }
        public string Email { get; set; }
    }
}
