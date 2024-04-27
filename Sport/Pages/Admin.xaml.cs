using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using Microsoft.Office.Interop.Excel;
using Sport.Models;

namespace Sport.Pages
{
    /// <summary>
    /// Вывыод информации о всех работниках из базы и фильтрация по должности и фамилии
    /// </summary>
    public partial class Admin : System.Windows.Controls.Page
    {



        public Admin(int id)
        {


            InitializeComponent();
            DateNow(id);
            InitializeComponent();
            var personal = Helper.GetContext().DanniePersonal.ToList();
            LViemProduct.ItemsSource = personal;

        }
        //Метод который при иницилизации приветствует пользователя
        public void DateNow(int id)
        {
            SportEntities c = new SportEntities();
            Doljnosti doljnosti = new Doljnosti();

            comb1.Items.Clear();
            comb1.ItemsSource = Helper.GetContext().Doljnosti.Select(p => p.NameDolj).ToList();

            var info = Helper.GetContext().DanniePersonal.Where(n => n.KodPersonal == id).FirstOrDefault();
            DateTime currentTime = DateTime.Now;


            if (currentTime.Hour >= 10 && currentTime.Hour < 12)
            {
                lbtext.Content = $"Доброе утро {info.ImiPersonal} {info.FamiliaPersonala} {info.OthestvoPersonala}";

            }
            else if (currentTime.Hour >= 12 && currentTime.Hour <= 17)
            {
                lbtext.Content = $"Добрый день {info.ImiPersonal} {info.FamiliaPersonala} {info.OthestvoPersonala}";

            }
            else if (currentTime.Hour > 17 && currentTime.Hour <= 19)
            {
                lbtext.Content = $"Добрый вечер {info.ImiPersonal} {info.FamiliaPersonala} {info.OthestvoPersonala}";

            }
        }
        private void PanelEmp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        //Переход на страницу реаактирования данных пользователя 
        private void LViemProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var u = LViemProduct.SelectedItem as DanniePersonal;

            NavigationService.Navigate(new Redactorxaml(u));

        }
        //Фильтр пользователей по Фамилии и Должноси 
        private void btfilter_Click(object sender, RoutedEventArgs e)
        {
            var context = Helper.GetContext();

            var filteredData = context.DanniePersonal;

            if (!string.IsNullOrEmpty(tbsearch.Text) || comb1.SelectedIndex != null)
            {

                var filtered = Helper.GetContext().DanniePersonal.Where(item => item.FamiliaPersonala.ToLower().Contains(tbsearch.Text.ToLower()) ||
                    item.DoljnostiPersonal.Equals(comb1.SelectedIndex + 1)
                    ).ToList();
                LViemProduct.ItemsSource = filtered.ToList();
            }
            else
            {
                LViemProduct.ItemsSource = filteredData.ToList();
            }



        }
        //Переход на страницу добавления пользователя
        private void btadd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Redactorxaml(null));
        }

        private void tbsearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btspisok_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SpisokZanitia());
        }
        public class Danie
        {
            public int Kod { get; set; }
            public string Doljnosti { get; set; }
            public string ImiPersonal { get; set; }
            public string FamiliaPersonala { get; set; }
            public string OthestvoPersonala { get; set; }
            public string NomerTelefona { get; set; }
            public string E_mail { get; set; }
        }

        private void btnexcel_Click(object sender, RoutedEventArgs e)
        {
            var context = Helper.GetContext();
            //Извлечение из базы данных и преобразование в объекты Danie
            var empl = Helper.GetContext().DanniePersonal.Select(r => new Danie
            {
                Kod = (int)r.KodPersonal,
                Doljnosti = context.Doljnosti.Where(p => p.KodDolj == r.DoljnostiPersonal).Select(p => p.NameDolj).FirstOrDefault(),
                ImiPersonal = r.ImiPersonal,
                FamiliaPersonala = r.FamiliaPersonala,
                OthestvoPersonala = r.OthestvoPersonala,
                NomerTelefona = r.NomerTelefona,
                E_mail = r.E_mail,
            }).ToList();
            //Создание экземпляра, файла и листа
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            Workbook excelWorkbook = excelApp.Workbooks.Add(); 
            Worksheet excelWorksheet = excelWorkbook.Sheets[1];
            //Заполнение заголовков
            for (int i = 0; i < typeof(Danie).GetProperties().Length; i++)
            {
                excelWorksheet.Cells[1, i + 1] = typeof(Danie).GetProperties()[i].Name;
            }
           //Заполнение данных
            for (int i = 0; i < empl.Count; i++)
            {
                for (int j = 0; j < typeof(Danie).GetProperties().Length; j++)
                {
                    excelWorksheet.Cells[i + 2, j + 1] = typeof(Danie).GetProperties()[j].GetValue(empl[i]);
                }
            }

            excelWorkbook.SaveAs("C:\\Users\\user\\Desktop\\Sportsotrudnik.xlsx");//Сохранения файла на рабочий стол
            excelWorkbook.Close();
            excelApp.Quit();
            MessageBox.Show("Файл успешно создан и добавлен на рабочий стол!","Внимание",MessageBoxButton.OK);
        }


       
    }
    
}
