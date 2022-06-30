using CLINICS.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace CLINICS.pages
{
    /// <summary>
    /// Interaction logic for Appointment.xaml
    /// </summary>
    public partial class ChooseService : Page
    {
        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
            MainWindow.MenuStackPanel.Visibility = Visibility.Hidden;
        }
        public ChooseService()
        {
            InitializeComponent();
            using (CLINICSEntities db = new CLINICSEntities())
            {
                var servicesButtons = from services in db.SERVICEs
                                      select new
                                      {
                                          ServiceName = services.ServiceName,
                                          ServiceId = services.ServiceID
                                      };

                foreach (var serv in servicesButtons)
                {
                    if (servicesButtons.Count()==0)
                    {
                        inCaseNullServ.Text = "Виды операций не добавлены!";
                        return;
                    }
                    var buf = new ServiceButtonUserControl(serv.ServiceName, serv.ServiceId);
                    ServicesItemsControl.Items.Add(buf);
                }
            }
        }
    }
}
