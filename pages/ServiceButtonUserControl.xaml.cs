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
    /// Interaction logic for ServiceButtonUserControl.xaml
    /// </summary>
    public partial class ServiceButtonUserControl : UserControl
    {
        public static string nameOfChosenService { get; set; }
        private int idOfChosenService { get; set; }
        public ServiceButtonUserControl(string ServiceName, int ServiceId)
        {
            InitializeComponent();
            serviceName.Text = ServiceName;
            idOfChosenService = ServiceId;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Frame.Navigate(new ChooseDoctorListView(idOfChosenService));
        }
    }
}
