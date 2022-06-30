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

namespace CLINICS.pages
{
    /// <summary>
    /// Interaction logic for DoctorHistory.xaml
    /// </summary>
    public partial class DoctorHistory : Page
    {
        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
            MainWindow.MenuStackPanel.Visibility = Visibility.Hidden;
        }
        public static StackPanel BookingDoctorHistory { get; set; }
        public static StackPanel InCaseNullBookingsDoc { get; set; }
        public DoctorHistory()
        {
            InitializeComponent();
            BookingDoctorHistory = bookingDoctorHistory;
            BookingDoctorHistory.Children.Clear();
            try
            {

                {
                    using (CLINICSEntities db = new CLINICSEntities())
                    {
                        var doctorBookings = (from r_time in db.REGISTRATION_TIME
                                              join r_date in db.REGISTRATION_DATE on r_time.TimeID equals r_date.TimeID
                                              join reg in db.REGISTRATIONs on r_date.DateID equals reg.DateID
                                              join doc_serv in db.DOCTOR_SERVICE on reg.DoctorServiceID equals doc_serv.DoctorServiceID
                                              join doc in db.DOCTORs on doc_serv.DoctorID equals doc.DoctorID
                                              join serv in db.SERVICEs on doc_serv.ServiceID equals serv.ServiceID
                                              join client in db.CLIENTs on reg.ClientID equals client.ClientID
                                              where reg.DoctorServiceID == doc_serv.DoctorServiceID &&
                                                    doc.DoctorSurname == MainWindow.clientSurname &&
                                                    doc.DoctorName == MainWindow.clientName &&
                                                    doc.DoctorPatronymic == MainWindow.clientPatronymic
                                              orderby reg.Status ascending
                                              select new
                                              {
                                                  date = r_date.Date,
                                                  time = r_time.Time,
                                                  service = serv.ServiceName,
                                                  client = client.ClientSurname + " " + client.ClientName,
                                                  status = "операция " + reg.Status,
                                                  reg_id = reg.RegistationID

                                              }).ToList();

                        BookingDoctorHistory.Children.Clear();
                        if (doctorBookings.Count() == 0)
                        {
                            inCaseNullBookingsDoc.Visibility = Visibility.Visible;
                            inCaseNullBookingsDoc.Text = "У вас еще нет пациентов!";
                            return;
                        }
                        foreach (var bok in doctorBookings)
                        {
                            var buf = new DoctorHistoryUserControl(bok.date.ToString().Remove(10), bok.time.ToString().Remove(5),
                                bok.service, bok.client, bok.status, bok.reg_id);
                            if (bok.status == "операция запланирована")
                            {
                                DoctorHistoryUserControl.GridButtonToCollapse.Visibility = Visibility.Visible;
                                DoctorHistory.BookingDoctorHistory.Children.Add(buf);
                            }
                            else
                            {
                                if ((bok.status == "операция проведена"))
                                {
                                    DoctorHistoryUserControl.GridButtonToCollapse.Visibility = Visibility.Collapsed;
                                    DoctorHistory.BookingDoctorHistory.Children.Add(buf);
                                }

                            }
                        }
                    };
                }
            }
            catch
            {

            }
        }
    }
}
