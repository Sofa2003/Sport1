using Sport.Models;
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

namespace Sport.Pages
{
    /// <summary>
    /// Двухфакторка. Ввод кода и переход на страницы по должности.
    /// </summary>
    public partial class KodPochta : Page
    {
        int codi;
        DanniePersonal person;
        public KodPochta(int Cod, DanniePersonal employer)
        {
            InitializeComponent();
            codi = Cod;
            person = employer;
        }
        //При нажатии на кнопку сверяет код и при успешной проверки
        private void btnsverka_Click(object sender, RoutedEventArgs e)
        {
            //Проверка на правильно введеный код в textbox с отправленым на почту 
            if (txboxkodi.Text == Convert.ToString(codi))
            {
                switch (person.DoljnostiPersonal)
                {
                    case 1:
                        NavigationService.Navigate(new Sport.Pages.Admin(person.KodPersonal));
                        break;



                    case 2:
                        NavigationService.Navigate(new Sport.Pages.Menedjer(person.KodPersonal));
                        break;
                }


            }
            else
            {

                MessageBox.Show("Код введён неправильно, попробуйте ещё раз");
            }
        }
    }
}
