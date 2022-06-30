using CLINICS.windows;
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

namespace CLINICS.pages
{
    /// <summary>
    /// Interaction logic for DoctorHistoryUserControl.xaml
    /// </summary>
    public partial class DoctorHistoryUserControl : UserControl
    {
        public int idOfChosenRegistration { get; set; }
        public static Grid GridButtonToCollapse { get; set; }
        public static Button ButtonToCollapse { get; set; }
        public DoctorHistoryUserControl(string Date, string Time, string Service, string Client, string Status, int Id)
        {
            InitializeComponent();
            date.Text = Date;
            time.Text = Time;
            service.Text = Service;
            client.Text = Client;
            status.Text = Status;
            idOfChosenRegistration = Id;
            GridButtonToCollapse = buttonGrid;
            ButtonToCollapse = endService;
        }

        public static EndServiceByDoctor EndServiceByDoctor { get; set; }
        private void endService_Click(object sender, RoutedEventArgs e)
        {
            EndServiceByDoctor endServiceByDoctor = new EndServiceByDoctor(idOfChosenRegistration);
            EndServiceByDoctor = endServiceByDoctor;
            EndServiceByDoctor.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            EndServiceByDoctor.Show();
        }
    }
}
