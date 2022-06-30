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
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class History : Page
    {
        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
            MainWindow.MenuStackPanel.Visibility = Visibility.Hidden;
        }
        private int ____idOfChosenDoctor { get; set; }
        private int ____idOfChosenService { get; set; }
        public static StackPanel InCaseNullBookingsStackpanel { get; set; }
        public static TextBlock InCaseNullBookings { get; set; }
        public static StackPanel BookingsUserHistory { get; set; }

        public History()
        {
            InitializeComponent();
            InCaseNullBookingsStackpanel = inCaseNullBookingsStackpanel;
            InCaseNullBookings = inCaseNullBookings;
            BookingsUserHistory = bookingsUserHistory;
            BookingsUserHistory.Children.Clear();

            using (CLINICSEntities db = new CLINICSEntities())
            {

                var bookings = (from r_time in db.REGISTRATION_TIME
                                join r_date in db.REGISTRATION_DATE on r_time.TimeID equals r_date.TimeID
                                join reg in db.REGISTRATIONs on r_date.DateID equals reg.DateID
                                join doc_serv in db.DOCTOR_SERVICE on reg.DoctorServiceID equals doc_serv.DoctorServiceID
                                join doc in db.DOCTORs on doc_serv.DoctorID equals doc.DoctorID
                                join serv in db.SERVICEs on doc_serv.ServiceID equals serv.ServiceID
                                join client in db.CLIENTs on reg.ClientID equals client.ClientID
                                where reg.ClientID == App.currentClient.ClientID
                                orderby r_date.Date ascending
                                //orderby reg.Status ascending
                                select new
                                {
                                    id = reg.RegistationID,
                                    service_id = serv.ServiceID,
                                    doctor_id = doc.DoctorID,
                                    date = r_date.Date,
                                    time = r_time.Time,
                                    service = serv.ServiceName,
                                    doctor = doc.DoctorSurname + " " + doc.DoctorName.Remove(1) + ". " + doc.DoctorPatronymic.Remove(1) + ".",
                                    status = "операция " + reg.Status
                                }).ToList();

                if (bookings.Count() == 0)
                {
                    InCaseNullBookingsStackpanel.Visibility = Visibility.Visible;
                    InCaseNullBookings.Text = "Вы еще никуда не записаны. \nЗапишитесь на операцию прямо сейчас!";
                    return;
                }
                else
                {
                    InCaseNullBookingsStackpanel.Visibility = Visibility.Hidden;
                }
                foreach (var bok in bookings)
                {
                    var buf = new RegistrationsHistoryUserControl(bok.date.ToString().Remove(10), bok.time.ToString().Remove(5),
                              bok.service, bok.doctor, bok.status, bok.service_id, bok.doctor_id, bok.id);
                    if (bok.status == "операция проведена")
                    {
                        RegistrationsHistoryUserControl.LeaveComment.Visibility = Visibility.Visible;
                        RegistrationsHistoryUserControl.CancelBooking.Visibility = Visibility.Collapsed;
                        History.BookingsUserHistory.Children.Add(buf);
                    }
                    else
                    {
                        if (bok.status == "операция оценена")
                        {
                            RegistrationsHistoryUserControl.LeaveComment.Visibility = Visibility.Collapsed;
                            RegistrationsHistoryUserControl.CancelBooking.Visibility = Visibility.Collapsed;
                            History.BookingsUserHistory.Children.Add(buf);
                        }
                        else if (bok.status == "операция запланирована")
                        {
                            RegistrationsHistoryUserControl.CancelBooking.Visibility = Visibility.Visible;
                            RegistrationsHistoryUserControl.LeaveComment.Visibility = Visibility.Collapsed;
                            History.BookingsUserHistory.Children.Add(buf);
                        }
                        else if (bok.status == "операция отменена")
                        {
                            RegistrationsHistoryUserControl.LeaveComment.Visibility = Visibility.Collapsed;
                            RegistrationsHistoryUserControl.CancelBooking.Visibility = Visibility.Collapsed;
                            History.BookingsUserHistory.Children.Add(buf);
                        }
                    }
                }
            };
        }
            public History(int ___idOfChosenDoctor, int ___idOfChosenService)
            {
            InitializeComponent();
            InCaseNullBookingsStackpanel = inCaseNullBookingsStackpanel;
            InCaseNullBookings = inCaseNullBookings;
            BookingsUserHistory = bookingsUserHistory;
            BookingsUserHistory.Children.Clear();
            using (CLINICSEntities db = new CLINICSEntities())
            {

                var bookings = (from r_time in db.REGISTRATION_TIME
                                join r_date in db.REGISTRATION_DATE on r_time.TimeID equals r_date.TimeID
                                join reg in db.REGISTRATIONs on r_date.DateID equals reg.DateID
                                join doc_serv in db.DOCTOR_SERVICE on reg.DoctorServiceID equals doc_serv.DoctorServiceID
                                join doc in db.DOCTORs on doc_serv.DoctorID equals doc.DoctorID
                                join serv in db.SERVICEs on doc_serv.ServiceID equals serv.ServiceID
                                join client in db.CLIENTs on reg.ClientID equals client.ClientID
                                where reg.ClientID == App.currentClient.ClientID
                                orderby r_date.Date ascending
                                //orderby reg.Status ascending
                                select new
                                {
                                    id = reg.RegistationID,
                                    service_id = serv.ServiceID,
                                    doctor_id = doc.DoctorID,
                                    date = r_date.Date,
                                    time = r_time.Time,
                                    service = serv.ServiceName,
                                    doctor = doc.DoctorSurname + " " + doc.DoctorName.Remove(1) + ". " + doc.DoctorPatronymic.Remove(1) + ".",
                                    status = "операция " + reg.Status
                                }).ToList();

                if (bookings.Count() == 0)
                {
                    InCaseNullBookingsStackpanel.Visibility = Visibility.Visible;
                    InCaseNullBookings.Text = "Вы еще никуда не записаны. \nЗапишитесь на операцию прямо сейчас!";
                    return;
                }
                else
                {
                    InCaseNullBookingsStackpanel.Visibility = Visibility.Hidden;
                }
                foreach (var bok in bookings)
                {
                    var buf = new RegistrationsHistoryUserControl(bok.date.ToString().Remove(10), bok.time.ToString().Remove(5),
                              bok.service, bok.doctor, bok.status, bok.service_id, bok.doctor_id, bok.id);
                    if (bok.status == "операция проведена")
                    {
                        RegistrationsHistoryUserControl.LeaveComment.Visibility = Visibility.Visible;
                        RegistrationsHistoryUserControl.CancelBooking.Visibility = Visibility.Collapsed;
                        History.BookingsUserHistory.Children.Add(buf);
                    }
                    else
                    {
                        if (bok.status == "операция оценена")
                        {
                            RegistrationsHistoryUserControl.LeaveComment.Visibility = Visibility.Collapsed;
                            RegistrationsHistoryUserControl.CancelBooking.Visibility = Visibility.Collapsed;
                            History.BookingsUserHistory.Children.Add(buf);
                        }
                        else if (bok.status == "операция запланирована")
                        {
                            RegistrationsHistoryUserControl.CancelBooking.Visibility = Visibility.Visible;
                            RegistrationsHistoryUserControl.LeaveComment.Visibility = Visibility.Collapsed;
                            History.BookingsUserHistory.Children.Add(buf);
                        }
                        else if (bok.status == "операция отменена")
                        {
                            RegistrationsHistoryUserControl.LeaveComment.Visibility = Visibility.Collapsed;
                            RegistrationsHistoryUserControl.CancelBooking.Visibility = Visibility.Collapsed;
                            History.BookingsUserHistory.Children.Add(buf);
                        }
                    }
                }
            };
        }

        private void moveToStartBooking_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ChooseService());
        }
    }
}
