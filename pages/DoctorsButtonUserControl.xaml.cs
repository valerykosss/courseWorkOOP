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
using CLINICS.pages;
using CLINICS.windows;

namespace CLINICS
{
    /// <summary>
    /// Interaction logic for DoctorsButton.xaml
    /// </summary>
    public partial class DoctorsButtonUserControl : UserControl
    {
        private int __idOfChosenService { get; set; }
        private int __idOfChosenDoctor { get; set; }
        public DoctorsButtonUserControl(string CurrentName, int idOfChosenDoctor, string Image, int _idOfChosenService)
        {
            InitializeComponent();
            BitmapImage myBitmapImage = new BitmapImage(new Uri(Image));
            myBitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            currentDoctorImage.Source = myBitmapImage;
            surnameNamePatronymic.Text = CurrentName;
            __idOfChosenService = _idOfChosenService;
            __idOfChosenDoctor = idOfChosenDoctor;
        }
        public static string idOfChosenDoctor { get; set; }

        private void ChooseDoctor_Click(object sender, RoutedEventArgs e)
        {
            ChooseDateTime ChooseDateTime = new ChooseDateTime(__idOfChosenService, __idOfChosenDoctor);
            MainWindow.Frame.Navigate(ChooseDateTime);
        }
    }
}
