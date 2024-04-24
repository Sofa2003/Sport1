using HashPassword;
using Registr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace Registr
{
    internal class Program
    {
        static void Main(string[] args)
        {
            


                try
                {
                    SportEntities sport = new SportEntities();


                    Console.WriteLine("Введите имя пользователя: ");
                    string Name = Console.ReadLine();
                    if (string.IsNullOrEmpty(Name))
                    {
                        Console.WriteLine("Значения не должны быть пустыми");
                        Console.ReadKey();
                        return;
                    }
                    Console.WriteLine("Введите фамилию пользователя: ");
                    string Surname = Console.ReadLine();
                    if (string.IsNullOrEmpty(Surname))
                    {
                        Console.WriteLine("Значения не должны быть пустыми");
                        Console.ReadKey();
                        return;
                    }
                    Console.WriteLine("Введите Отчество пользователя: ");
                    string Patronomyc = Console.ReadLine();
                    if (string.IsNullOrEmpty(Patronomyc))
                    {
                        Console.WriteLine("Значения не должны быть пустыми");
                        Console.ReadKey();
                        return;
                    }
                    Console.WriteLine("Введите дата рождения пользователя: ");
                    DateTime date = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Введите Телефонный номер пользователя: ");
                    string phonenumber =(Console.ReadLine());
                    Console.WriteLine("Введите должность  пользователя\n1-Администратор 2-Менеджер 3-Директор 4-Тренер: ");
                    int doljnosti = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Введите стаж пользователя: ");
                    int Staj = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Введите логин пользователя: ");
                    string login = Console.ReadLine();
                    if (string.IsNullOrEmpty(login))
                    {
                        Console.WriteLine("Значения не должны быть пустыми");
                        Console.ReadKey();
                        return;
                    }
                    Console.WriteLine("Введите пароль пользователя: ");
                    string password1 = Console.ReadLine();
                    if (string.IsNullOrEmpty(password1))
                    {
                        Console.WriteLine("Значения не должны быть пустыми");
                        Console.ReadKey();
                        return;
                    }

                    Class1 hasher = new Class1();
                    string hashpassword = hasher.HashingPassword(password1);
                    Console.WriteLine(hashpassword);
                    var emp = new Model.DanniePersonal
                    {
                        ImiPersonal = Name,
                        FamiliaPersonala = Surname,
                        OthestvoPersonala = Patronomyc,
                        DataRojdenia = date,
                        NomerTelefona = phonenumber,
                        DoljnostiPersonal = doljnosti,
                        StajRaboti = Staj


                    };
                    sport.DanniePersonal.Add(emp);
                    sport.SaveChanges();

                    var user = new Model.Polizovateli
                    {
                        KodPolizovatieli = emp.KodPersonal,
                        LoginPolizovateli = login,
                        ParoliPolizovateli = hashpassword
                    };

                    sport.Polizovateli.Add(user);
                    sport.SaveChanges();

                    Console.WriteLine("Учетная запись добавлена");
                    Console.WriteLine($"Логин сотрудника{login} Пароль сотрудника{hashpassword}");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Произошла ошибка\t" + ex.Message);
                    Console.ReadKey();
                }
        

        }

    }
}
