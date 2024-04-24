using HashPassword;
using Sport.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using System.Data.Entity;


namespace Sport.Pages
{
    /// <summary>
    /// Ввод логина и пароля и проверка на соответсвии с базой
    /// </summary>
    public partial class Auto : Page
    {

        public Auto()
        {
            InitializeComponent();
            txtboxCaptcha.Visibility = Visibility.Hidden;
            txtBlockCaptcha.Visibility = Visibility.Hidden;
            Label labelzabil = new Label();
            labelzabil.MouseLeftButtonUp += ZabilClick;


        }
        //Метод отправляющий код для входа в систему
        private async void SendEmails(int Kod, string emailes)
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
        //Кнопка войти как гость. Перход на с траницу клиента
        private void btnEnterGuests_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Client());
        }
        //Количество ошибок в коде 
        private int countUnsuccessfull = 0;



        //Авторизация, каптча
        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            Class1 hasher = new Class1();
            Polizovateli users = new Polizovateli();
            DanniePersonal employee = new DanniePersonal();
            DateTime data = DateTime.Now;

            string login = txtbLogin.Text.Trim();
            string password = pswbPassword.Password.Trim();
            string email = employee.E_mail;
            //Проверка на вход в рабочее время
            //if (data.Hour >= 10 && data.Hour < 19)
            //{
                if (login.Length > 0 && password.Length > 0)
                {
                    if (countUnsuccessfull < 1)
                    {
                        string hashPassw = hasher.HashingPassword(password);
                        var ema = Helper.GetContext().DanniePersonal.Where(u => u.E_mail == email).Select(i => i.KodPersonal).FirstOrDefault();
                        var id = Helper.GetContext().Polizovateli.Where(u => u.LoginPolizovateli == login && u.ParoliPolizovateli == hashPassw).Select(i => i.KodPolizovatieli).FirstOrDefault();
                        var useri = Helper.GetContext().Polizovateli.Where(u => u.LoginPolizovateli == login && u.ParoliPolizovateli == hashPassw).FirstOrDefault();
                        //Проверка пользователя базы данных по логину и паролю
                        if (useri != null)
                        {
                            var employer = Helper.GetContext().DanniePersonal.FirstOrDefault(emp => emp.KodPersonal == useri.KodPolizovatieli);

                            NavigationService.Navigate(new Sport.Pages.ZabilParol());
                            //Проверка на корректность введеных данных и переход на двухфакторную аутификацию
                            //if (employer != null /*&& employer.DoljnostiPersonal != 0*/)
                            //{
                            //    string emilsid = employer.E_mail;

                            //    Random random = new Random();
                            //    Cod = random.Next(10000, 99999);
                            //    SendEmails(Cod, emilsid);
                            //    KodPochta pochta = new KodPochta(Cod, employer);
                            //    NavigationService.Navigate(pochta)};
                            




                                switch (employer.DoljnostiPersonal)
                                {
                                    case 1:
                                        NavigationService.Navigate(new Sport.Pages.Admin(id));
                                        break;

                                    case 2:
                                        NavigationService.Navigate(new Sport.Pages.Menedjer(id));
                                        break;
                                }
                            






                        }
                        else
                        {
                            MessageBox.Show("Пользователь с таким логином или паролем не существует", "Ошибка", MessageBoxButton.OK);
                            countUnsuccessfull++;
                            GenerateCaptcha();
                        }



                    }
                    //Генерация каптчи
                    else
                    {
                        string hashPassw = hasher.HashingPassword(password);
                        var useri = Helper.GetContext().Polizovateli.Where(u => u.LoginPolizovateli == login && u.ParoliPolizovateli == hashPassw).FirstOrDefault();
                        string capctha = txtboxCaptcha.Text.Trim();
                        if (useri != null && capctha == txtBlockCaptcha.Text)
                        {
                            txtBlockCaptcha.Visibility = Visibility.Hidden;
                            txtboxCaptcha.Visibility = Visibility.Hidden;
                            txtboxCaptcha.Text = "";
                            countUnsuccessfull = 0;
                        }
                        //Проверка на количество ошибок вввода
                        if (countUnsuccessfull <= 2)
                        {
                            MessageBox.Show("Капча введена неверно", "Ошибка", MessageBoxButton.OK);
                            countUnsuccessfull++;
                            GenerateCaptcha();
                        }
                        //Блокировка ввода если каптча была 2 раза неправильной
                        else
                        {
                            MessageBox.Show("Капча введена неверно более двух раз", "Ошибка", MessageBoxButton.OK);
                            countUnsuccessfull++;
                            GenerateCaptcha();
                            //Метод для блокировки кнопок ввода на 1 минуту
                            wait();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Не все обязательные поля были заполнены! Проверьте заполненность и повторите попытку!", "Внимание!", MessageBoxButton.OK);
                }
            //}

            //else
            //{
            //    MessageBox.Show("Не рабочее время", "Ошибка", MessageBoxButton.OK);
            //}

        }
        //Метод генерации каптчи
        private void GenerateCaptcha()
        {
            txtboxCaptcha.Visibility = Visibility.Visible;
            txtBlockCaptcha.Visibility = Visibility.Visible;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            string captcha = new string(Enumerable.Repeat(chars, 6)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            txtBlockCaptcha.Text = captcha;
            txtBlockCaptcha.TextDecorations = TextDecorations.Strikethrough;
        }
        //Метод для блокировки кнопок ввода на 1 минуту 
        private async void wait()
        {
            lblTime.Visibility = Visibility.Visible;
            int remainingTime = 10;
            btnEnterGuests.IsEnabled = false;
            btnEnter.IsEnabled = false;
            txtbLogin.IsEnabled = false;
            pswbPassword.IsEnabled = false;
            txtboxCaptcha.IsEnabled = false;

            while (remainingTime > 0)
            {
                lblTime.Content = $"До конца блокировки {remainingTime} сек";
                await Task.Delay(1000);
                remainingTime--;
            }
            lblTime.Content = "Блокировка завершена!";
            btnEnterGuests.IsEnabled = true;
            btnEnter.IsEnabled = true;
            txtbLogin.IsEnabled = true;
            pswbPassword.IsEnabled = true;
            txtboxCaptcha.IsEnabled = true;
            await Task.Delay(2000);
            lblTime.Content = " ";

        }
        //Переход на новое окно через lable "Забыл пароль?"
        private void ZabilClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ZabilParol zabil = new ZabilParol();
            NavigationService.Navigate(zabil);
            //zabil.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //zabil.Show();
        }



    }

}