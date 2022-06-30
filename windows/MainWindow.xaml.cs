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
using CLINICS.models;
using CLINICS.pages;
using CLINICS.windows;

namespace CLINICS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public static CLIENT ClientStatic {get; set;}
        public static string clientSurname { get; set; }
        public static string clientName { get; set; }
        public static string clientPatronymic { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Frame = mainFrame;
            //ClientStatic = null;
            MenuStackPanel = menuStackPanel;
            LoginPage LoginPage = new LoginPage();
            Frame.Navigate(LoginPage);

            /*Uri resourceLocater = new Uri("AboutUs.xaml", UriKind.Relative);
           if (this.forLogRegFrame.Pages.Source == resourceLocater)
           {
               //this.AboutUsIcon.Source = new BitmapImage(new Uri(@"/images/aboutuUsActive.png", UriKind.Relative));
           }*/
        }
        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window w in App.Current.Windows)
                w.Close();
        }
        public static Frame Frame { get; set; }
        public static StackPanel MenuStackPanel { get; set; }
        private void AboutUsButton_Click(object sender, RoutedEventArgs e)
        {
            AboutUs AboutUs = new AboutUs();
            Frame.Navigate(AboutUs);
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            Profile Profile = new Profile();
            Frame.Navigate(Profile);
        }

        private void ApointmentButton_Click(object sender, RoutedEventArgs e)
        {
            ChooseService ChooseService = new ChooseService();
            Frame.Navigate(ChooseService);
        }

        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            History History = new History();
            Frame.Navigate(History);
        }

        private void RatingButton_Click(object sender, RoutedEventArgs e)
        {
            DoctorRating DoctorRating = new DoctorRating();
            Frame.Navigate(DoctorRating);
        }
    }
}
