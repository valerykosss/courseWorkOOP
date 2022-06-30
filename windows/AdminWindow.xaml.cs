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
using CLINICS.adminpages;

namespace CLINICS.windows
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            adminFrame.Navigate(new MainAdminPage());

        }
        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window w in App.Current.Windows)
                w.Close();
        }
        private void ClientButton_Click(object sender, RoutedEventArgs e)
        {
            adminFrame.Navigate(new ClientTable());
        }

        private void DoctorButton_Click(object sender, RoutedEventArgs e)
        {
            adminFrame.Navigate(new DoctorTable());
        }

        private void DoctorService_Click(object sender, RoutedEventArgs e)
        {
            adminFrame.Navigate(new DoctorServiceTable());
        }

        private void Service_Click(object sender, RoutedEventArgs e)
        {
            adminFrame.Navigate(new ServiceTable());
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            adminFrame.Navigate(new RegistrationTable());
        }

        private void RegistrationTime_Click(object sender, RoutedEventArgs e)
        {
            adminFrame.Navigate(new RegistrationTimeTable());
        }

        private void RegistartionDate_Click(object sender, RoutedEventArgs e)
        {
            adminFrame.Navigate(new RegistrationDateTable());
        }
        private void DoctorRating_Click(object sender, RoutedEventArgs e)
        {
            adminFrame.Navigate(new DoctorRatingTable());
        }

        //private void SearchButton_Click(object sender, RoutedEventArgs e)
        //{
        //    adminFrame.Navigate(new DBSearch());
        //}

    }
}
