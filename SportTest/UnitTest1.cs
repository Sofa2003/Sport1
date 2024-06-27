using HashPassword;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sport;
using Sport.Models;
using Sport.Pages;
using System;
using System.Linq;
using System.Runtime.InteropServices;


namespace SportTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            SportEntities sport = new SportEntities();
            var AuthoPage = new Auto();

            string Login = "Sofa2003";
            string Password = "123";
            Class1 hasher = new Class1();
            string hashingPassword = hasher.HashingPassword(Password.Trim());

            Polizovateli user = sport.Polizovateli.Where(u => u.LoginPolizovateli == Login && u.ParoliPolizovateli == hashingPassword).FirstOrDefault();

            Assert.IsNotNull(user);


        }
        [TestMethod]
        public void TestMethod2()
        {
            SportEntities sport = new SportEntities();
            var AuthoPage = new Auto();

            string Login = "Sofa2003";
            string Password = "123";
            Class1 hasher = new Class1();
            string hashingPassword = hasher.HashingPassword(Password.Trim());

            Polizovateli user = sport.Polizovateli.Where(u => u.LoginPolizovateli == Login && u.ParoliPolizovateli == hashingPassword).FirstOrDefault();
            DanniePersonal danne = sport.DanniePersonal.Where(u => u.KodPersonal == user.KodPolizovatieli).FirstOrDefault();
            Doljnosti doljnosti = sport.Doljnosti.Where(u => u.KodDolj == danne.DoljnostiPersonal).FirstOrDefault();

            Assert.AreEqual("Администратор ", doljnosti.NameDolj);


        }

    }
    
    
}
