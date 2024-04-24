using HashPassword;
using Sport.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
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
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Excel;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Sport.Pages
{
    /// <summary>
    /// Редактирование и добавление нового пользователя
    /// </summary>
    public partial class Redactorxaml : System.Windows.Controls.Page
    {//Переменная хранящая пользователя
        private DanniePersonal danne;
        public Redactorxaml(DanniePersonal danie)
        {
            InitializeComponent();
            //Если пользователь равен Null, очизаем поля и скрываем кнопку редактирования(добавление нового пользователя)
            if (danie == null)
            {
                btnword.Visibility = Visibility.Hidden;
                btredactor.Visibility = Visibility.Hidden;
                combodolj.Items.Clear();
                combodolj.ItemsSource = Helper.GetContext().Doljnosti.Select(p => p.NameDolj).ToList();

            }
            //Если пользователь не равен Null, заполняем поля его данными и скрывем кнопки сохранить и отчистка(для редактирования пользователя)
            else
            {
                btnochis.Visibility = Visibility.Hidden;
                btnsohri.Visibility = Visibility.Hidden;
                danne = Helper.GetContext().DanniePersonal.Find(danie.KodPersonal);
                txtboximi.Text = danie.ImiPersonal;
                txboxfami.Text = danie.FamiliaPersonala;
                txboxotch.Text = danie.OthestvoPersonala;
                DateTime data = Convert.ToDateTime(danie.DataRojdenia);
                datapick.Text = data.ToString();
                int nomer = Convert.ToInt32(danie.NomerTelefona);
                txboxtelefon.Text = nomer.ToString();
                combodolj.SelectedIndex = combodolj.Items.IndexOf(danie.Doljnosti.NameDolj);
                int staj = Convert.ToInt32(danie.StajRaboti);
                tbstaj.Text = staj.ToString();
                tblogin.Text = danie.Polizovateli.LoginPolizovateli;
                txtboxemail.Text = danie.E_mail;
                combodolj.Items.Clear();
                combodolj.ItemsSource = Helper.GetContext().Doljnosti.Select(p => p.NameDolj).ToList();

                


            }

        }
        //Отчистка всех полей 
        private void btnochis_Click(object sender, RoutedEventArgs e)
        {
            combodolj.SelectedIndex = 0;
            txtboximi.Text = "";
            txboxfami.Text = "";
            txboxotch.Text = "";
            txboxtelefon.Text = "";
            tbstaj.Text = "";
            tblogin.Text = "";
            tbparoli.Text = "";
            txtboxemail.Text = "";
        }
        //Метод сохранения пользователя
        public void Save()
        {
            try
            {
                SportEntities sport = new SportEntities();
                Class1 hash = new Class1();
                int posts = Convert.ToInt32(combodolj.SelectedIndex);
                int staj;
                if (tbstaj.Text != null)
                {
                    staj = Convert.ToInt32(tbstaj.Text);
                    tbstaj.Text = staj.ToString();
                }

                var emp = new DanniePersonal
                {
                    ImiPersonal = txtboximi.Text,
                    FamiliaPersonala = txboxfami.Text,
                    OthestvoPersonala = txboxotch.Text,
                    DataRojdenia = Convert.ToDateTime(datapick.Text),
                    NomerTelefona = txboxtelefon.Text,
                    DoljnostiPersonal = posts,
                    StajRaboti = Convert.ToInt32(tbstaj.Text),
                    E_mail = txtboxemail.Text,

                };
                //Валидация данных
                var content = new ValidationContext(emp);
                var result = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
                if (!Validator.TryValidateObject(emp, content, result, true))
                {
                    string errores = "";
                    foreach (var error in result)
                    {
                        errores += error.ErrorMessage + "\n";
                    }
                    MessageBox.Show($"Не удалось добавить пользователя по причине:\n{errores}");
                }
                sport.DanniePersonal.Add(emp);
                sport.SaveChanges();

                string hasspass = hash.HashingPassword(tbparoli.Text);
                var user = new Models.Polizovateli
                {
                    KodPolizovatieli = emp.KodPersonal,
                    LoginPolizovateli = tblogin.Text,
                    ParoliPolizovateli = hasspass
                };

                if (!Validator.TryValidateObject(user, content, result, true))
                {
                    string errores = "";
                    foreach (var error in result)
                    {
                        errores += error.ErrorMessage + "\n";
                    }
                    MessageBox.Show($"Не удалось добавить пользователя по причине:\n{errores}");
                }
                sport.Polizovateli.Add(user);
                sport.SaveChanges();
                System.Windows.MessageBox.Show("Пользователь добавлен в базу");
            }
            catch (Exception ex) { }
        }
        //Сохранения пользователя
        private void btnsohri_Click(object sender, RoutedEventArgs e)
        {//Метод сохранения
            Save();

        }
        //Редактирования пользователя
        private void btredactor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Class1 hasher = new Class1();
                string hashPass = hasher.HashingPassword(tbparoli.Text);

                string post = combodolj.Text;
                var info = Helper.GetContext().Doljnosti.Where(u => u.NameDolj == post).FirstOrDefault();


                danne.DoljnostiPersonal = info.KodDolj;
                danne.ImiPersonal = txtboximi.Text;
                danne.FamiliaPersonala = txboxfami.Text;
                danne.OthestvoPersonala = txboxotch.Text;
                danne.DataRojdenia = Convert.ToDateTime(datapick.Text);
                danne.NomerTelefona = txboxtelefon.Text;
                danne.StajRaboti = Convert.ToInt32(tbstaj.Text);
                danne.E_mail = txtboxemail.Text;

                danne.Polizovateli.LoginPolizovateli = tblogin.Text;


                if (tbparoli.Text != null)
                {
                    danne.Polizovateli.ParoliPolizovateli = hashPass;
                }
                Helper.GetContext().Entry(danne).State = EntityState.Modified;
                Helper.GetContext().SaveChanges();


            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

            MessageBox.Show("Пользователь успешно отредактирвоан");
        }

        private void btnword_Click(object sender, RoutedEventArgs e)
        { DateTime currentDate = DateTime.Now;
            string[] months = { "Января", "Февраля", "Марта", "Апреля", "Мая", "Июня", "Июля",
                "Августа", "Сентября", "Октября", "Ноября", "Декабря" };
            int daysInMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
            int adjustedDay = currentDate.Day + 14;
            int adjustedMonth = currentDate.Month;
            int adjustedYear = currentDate.Year;
            int monthNumb;
            if (adjustedDay > daysInMonth)
            {
                adjustedDay = adjustedDay - daysInMonth;
                adjustedMonth = currentDate.Month + 1;
                if (adjustedMonth > 12)
                {
                    adjustedMonth = 1;
                    adjustedYear = currentDate.Year + 1;
                }
            }
            if (danne.KodPersonal == 2)
            { monthNumb = 1; }
            else
            { monthNumb = 2; }
            string post = combodolj.Text;
            var info = Helper.GetContext().Doljnosti.Where(u => u.NameDolj == post).FirstOrDefault();
            danne.DoljnostiPersonal = info.KodDolj;
            var items = new Dictionary<string, string>()
            {
                {"number", $"{danne.KodPersonal}"},
                {"gorod", "Новосибирск"},
                {"day", $"{currentDate.Day}"},
                {"month", $"{months[currentDate.Month-1]}"},
                {"year", $"{currentDate.Year}" },
                {"director", "Боровкова София Валерьевна"},
                {"FIOPersonal", $"{danne.ImiPersonal+" "+danne.FamiliaPersonala+" "+danne.OthestvoPersonala}"},
                {"NazvanieKom", "Спорт комплекс 'Заря'"},
                {"Doljnosti", $"{combodolj.Text}"},
                {"AdressRaboti", "Красный проспект, д.54, офис 10"},
                {"StartRabotiD", $"{adjustedDay}"},
                {"StartRabotiM", $"{months[adjustedMonth-1]}"},
                {"StartRabotiY", $"{adjustedYear}"},
                {"NomerMesiz", $"{monthNumb}"},
                {"ZarplataY", $"{(420000):N2} руб. в год"},
                {"ZarplataM", $"{(35000):N2}"},
                {"SeriesPass", "5014"},
                {"NumPass", "306501"},
                {"PassIssued", "ГУ МВД РОССИИ ПО\nНОВОСИБИРСКОЙ ОБЛАСТИ  "},
                {"INN", "98765432109876"},
                {"KPP", "987654321"}
            };
            Microsoft.Office.Interop.Word.Application wordApp = null;
            Microsoft.Office.Interop.Word.Document wordDoc;
            try
            {
                wordApp = new Microsoft.Office.Interop.Word.Application();

                object missing = Type.Missing;
                object fileName = "C:\\Users\\user\\Desktop\\shablon.docx"; //Путь к шаблону документа

                wordDoc = wordApp.Documents.Open(ref fileName, ref missing, ref missing, ref missing); //открываем шаблон документа

                foreach (var item in items) /* Перебор всех тегов и значений словаря, с последующей
                                 заменой каждого тега на соответствующее для него значение */
                {
                    Find find = wordApp.Selection.Find;
                    find.Text = item.Key;
                    find.Replacement.Text = item.Value;

                    object wrap = WdFindWrap.wdFindContinue;
                    object replace = WdReplace.wdReplaceAll;

                    find.Execute(FindText: Type.Missing,
                        MatchCase: false, MatchWholeWord: false, MatchWildcards: false,
                        MatchSoundsLike: missing, MatchAllWordForms: false, Forward: true,
                        Wrap: wrap, Format: false, ReplaceWith: missing, Replace: replace);
                }
                object newFile = $"C:\\Users\\user\\Documents\\dogovor{danne.KodPersonal}.docx";
                wordDoc.SaveAs2(newFile); //сохранить заполненный данными шаблон как новый документ
                wordApp.ActiveDocument.Close(); //закрытие активного документа
                wordApp?.Quit(); //отключение от приложения для работы с документами типа Word
            }
            catch (Exception ex)
            {
                wordApp.ActiveDocument.Close(); //закрытие активного документа
                wordApp?.Quit();
                Console.WriteLine(ex.Message);
            }
        }
       
    }
      
    
}
    
