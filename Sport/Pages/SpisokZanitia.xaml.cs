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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Sport.Pages
{
    /// <summary>
    /// Логика взаимодействия для SpisokZanitia.xaml
    /// </summary>
    public partial class SpisokZanitia : Page
    {
        public SpisokZanitia()
        {
            InitializeComponent();
            var spisok = Helper.GetContext().Zanitie.ToList();
            LViemProduct1.ItemsSource = spisok;
           
           

        }

        private void LViemProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }
        //Кнопка для печати 
        private void btnprint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                IDocumentPaginatorSource idp = focDoc;
                printDialog.PrintDocument(idp.DocumentPaginator, "FLow Document");
            }
        }
    }
}
