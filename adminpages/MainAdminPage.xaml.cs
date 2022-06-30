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

namespace CLINICS.adminpages
{
    /// <summary>
    /// Interaction logic for MainAdminPage.xaml
    /// </summary>
    public partial class MainAdminPage : Page
    {
        //private string CurrentName { get; set; }
        public MainAdminPage()
        {
            InitializeComponent();
            //CurrentName = MainWindow.clientName;
            adminName.Text = MainWindow.clientName;
        }
    }
}
