using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
using System.Drawing.Drawing2D;

using System.Data.Entity;
using System.Windows.Navigation;
using HashPassword;

namespace Sport.Pages
{
    /// <summary>
    /// Проверка на код отправленый на почту, ввод нового пароля 
    /// </summary>
    public partial class ZabilParol : Page
    {//Переменная для заполнения введеного Email
        public string emailbox = "";
        public ZabilParol()
        {
            InitializeComponent();

        }
        //Метод отправляющий код для восстановления пароля на почту пользователя 
        private async void SendEmail(int Kod, string emailes)
        {
            await Task.Run(() =>
            {
                string smtpServer = "smtp.mail.ru";
                int smtpPort = 587;
                string smtpUsername = "yulya_feduseva@mail.ru";
                string smtpPassword = "b1cGrafcE5dMcHUqN5Ra";
                SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort);
                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                smtpClient.EnableSsl = true;
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(smtpUsername);
                mailMessage.To.Add(emailes);
                mailMessage.Subject = "Код двухфакторной аутентификации";
                mailMessage.Body = $"Код двухфакторной вутентификации {Kod}";
                try
                {
                    smtpClient.Send(mailMessage);
                    MessageBox.Show("Сообщение отправлено на почту");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка отправки сообщения: {ex.Message}");
                }
            });
        }
        //Создание переменныой которая будет хранить код для сообщения на Email
        public int Cod = 0;

        // Метод Отправки кода на почту 
        private void OtpraviClick(object sender, RoutedEventArgs e)
        { 
            var emp = Helper.GetContext().DanniePersonal.Where(u => u.E_mail == tbemail.Text).FirstOrDefault();
            if (emp != null)
            {
                emailbox = emp.E_mail;
                Random random = new Random();
                Cod = random.Next(10000, 99999);
                SendEmail(Cod, emailbox);
                tbemail.Text = "";
                lbpochta.Visibility = Visibility.Hidden;
                btnotpravi.Visibility = Visibility.Hidden;
                tbemail.Visibility = Visibility.Hidden;
                textkod.Visibility = Visibility.Visible;
                btnsver.Visibility = Visibility.Visible;
                lbkod.Visibility = Visibility.Visible;

            }
            else
            { MessageBox.Show("Такого пользователя нет"); }

        }
        // При нажатии на кнопку идет сверка кодв отправленым на почту с кодом введеным в textbox
        private void btnsver_Click(object sender, RoutedEventArgs e)
        {
            if (textkod.Text == Convert.ToString(Cod))
            {

                textkod.Visibility = Visibility.Hidden;
                btnsver.Visibility = Visibility.Hidden;
                lbnowiparo.Visibility = Visibility.Visible;
                btparored.Visibility = Visibility.Visible;
                tbnoviparoli.Visibility = Visibility.Visible;
                tbnoviparoli2.Visibility = Visibility.Visible;
                lbnowiparo2.Visibility = Visibility.Visible;
                lbkod.Visibility = Visibility.Hidden;
                tbemail.Text = "";

            }
            else
            {
                tbemail.Text = "";
                MessageBox.Show("Код введён неправильно, попробуйте ещё раз");
            }
        }

        //Ввод нового пароля, проверка на ввод нового пароля с прошлым, проверка на одинаковые введеные поля
        private void btparored_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var emp = Helper.GetContext().DanniePersonal.Where(u => u.E_mail == emailbox).FirstOrDefault();
                var user = Helper.GetContext().Polizovateli.Where(u => u.KodPolizovatieli == emp.KodPersonal).FirstOrDefault();
                Class1 hasher = new Class1();

                if (tbnoviparoli.Text != "")
                {
                    if (tbnoviparoli.Text == tbnoviparoli2.Text)

                    {
                        string hashPass = hasher.HashingPassword(tbnoviparoli.Text);
                        
                        if (hashPass == user.ParoliPolizovateli)
                        {
                            MessageBox.Show("Новый пароль не должен совпадать с текущим");
                        }
                        else
                        {emp.Polizovateli.ParoliPolizovateli = hashPass;
                            Helper.UpdateUsers(user);
                            //Helper.GetContext().Entry(emp).State = EntityState.Modified;
                            //Helper.GetContext().SaveChanges();
                            MessageBox.Show($"Пароль успешно изменён!");
                            NavigationService.Navigate(new Sport.Pages.Auto());


                        }


                    }
                    else
                    {
                        MessageBox.Show("Пароли не совпадают!");
                    }


                }
                else
                {


                    MessageBox.Show("Поле пароль не должно быть пустым!");
                }

            }
            catch (Exception ex) { }


        }


    }

}
