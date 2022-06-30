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
    /// Interaction logic for RegistrationsHistoryUserControl.xaml
    /// </summary>
    public partial class RegistrationsHistoryUserControl : UserControl
    {
        public int idOfChosenRegistration { get; set; }
        private int _____idOfChosenDoctor { get; set; }
        private int _____idOfChosenService { get; set; }
        public static Button LeaveComment { get; set; }
        public static Button CancelBooking { get; set; }
        public RegistrationsHistoryUserControl(string Date, string Time, string Service, string Doctor, string Status, int ServiceId, int DoctorId, int ID)
        {
            InitializeComponent();
            date.Text = Date;
            time.Text = Time;
            service.Text = Service;
            doctor.Text = Doctor;
            status.Text = Status;
            _____idOfChosenDoctor = DoctorId;
            _____idOfChosenService = ServiceId;
            LeaveComment = leaveComment;
            CancelBooking = cancelBooking;
            idOfChosenRegistration = ID;

    }

    private void leaveComment_Click(object sender, RoutedEventArgs e)
        {
            LeaveCommentWindow leaveCommentWindow = new LeaveCommentWindow(_____idOfChosenDoctor, idOfChosenRegistration);
            leaveCommentWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            leaveCommentWindow.Show();
        }

        private void cancelBooking_Click(object sender, RoutedEventArgs e)
        {
            CancelBookingWindow cancelBookingWindow = new CancelBookingWindow(idOfChosenRegistration);
            cancelBookingWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            cancelBookingWindow.Show();
        }
    }
}
