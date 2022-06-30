using CLINICS.models;
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

namespace CLINICS
{
    /// <summary>
    /// Interaction logic for TimeButtonUserControl.xaml
    /// </summary>
    public partial class TimeButtonUserControl : UserControl
    {
        public static int idOfChoosenTime { get; set; }
        public TimeButtonUserControl(string Time)
        {
            InitializeComponent();
            time.Text = Time;
        }

        private void timeButton_Click(object sender, RoutedEventArgs e)
        {
            string timeFromButton = time.Text;
            REGISTRATION_TIME timeFromDB = CLINICSEntities.GetContext().REGISTRATION_TIME.Where(t => t.Time.ToString().Remove(5) == timeFromButton).FirstOrDefault();
            //timeId.Text = timeFromDB.TimeID.ToString();
            idOfChoosenTime = timeFromDB.TimeID;
        }
    }
}
