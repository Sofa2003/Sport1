using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Sport.Models;

namespace Sport.Pages
{
    /// <summary>
    /// Логика взаимодействия для Menedjer.xaml
    /// </summary>
    public partial class Menedjer : Window
    {
        public Menedjer(int id)
        {
            InitializeComponent();
            DateNow(id);
        }
        public void DateNow(int id)
        {
            var info = Helper.GetContext().DanniePersonal.Where(n => n.KodPersonal == id).FirstOrDefault();
            DateTime currentTime = DateTime.Now;


            if (currentTime.Hour >= 10 && currentTime.Hour < 12)
            {
                lbtext1.Content = $"Доброе утро {info.ImiPersonal} {info.FamiliaPersonala} {info.OthestvoPersonala}";

            }
            else if (currentTime.Hour >= 12 && currentTime.Hour <= 17)
            {
                lbtext1.Content = $"Добрый день {info.ImiPersonal} {info.FamiliaPersonala} {info.OthestvoPersonala}";

            }
            else if (currentTime.Hour > 17 && currentTime.Hour <= 19)
            {
                lbtext1.Content = $"Добрый вечер {info.ImiPersonal} {info.FamiliaPersonala} {info.OthestvoPersonala}";

            }
        }
    }
}
