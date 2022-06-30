using CLINICS.models;
using CLINICS.windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ChooseDateTime.xaml
    /// </summary>
    public partial class ChooseDateTime : Page
    {
        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
            MainWindow.MenuStackPanel.Visibility = Visibility.Hidden;
        }
        private int ___idOfChosenService { get; set; }
        private int ___idOfChosenDoctor { get; set; }

        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public List<string> allTimeSlots = new List<string>() { "13:00", "14:00", "15:00", "16:00", "17:00", "18:00" };

        public string chosenDate { get; set; }
        public DateTime fullDate { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public ChooseDateTime(int __idOfChosenService, int __idOfChosenDoctor)
        {
            ___idOfChosenService = __idOfChosenService;
            ___idOfChosenDoctor = __idOfChosenDoctor;
            InitializeComponent();
            DataContext = this;
            calendar.DisplayDateStart = DateTime.Today;
        }

        public void CreateTimeSlot()
        {
            DateTime selectedDate = new DateTime();
            if (calendar.SelectedDate != null)
            {
                string selectedDateStr = calendar.SelectedDate.ToString();
                if (calendar.SelectedDate.HasValue)
                {
                    selectedDate = calendar.SelectedDate.Value;
                }
                chosenDate = selectedDateStr.Remove(10);

                fullDate = calendar.SelectedDate.Value;
            }
            using (CLINICSEntities db = new CLINICSEntities())
            {
                var bookings = (from r_time in db.REGISTRATION_TIME
                                join r_date in db.REGISTRATION_DATE on r_time.TimeID equals r_date.TimeID
                                join reg in db.REGISTRATIONs on r_date.DateID equals reg.DateID
                                join doc_serv in db.DOCTOR_SERVICE on reg.DoctorServiceID equals doc_serv.DoctorServiceID
                                where DateTime.Compare(r_date.Date, selectedDate) == 0 && doc_serv.DoctorID == ___idOfChosenDoctor
                                && doc_serv.ServiceID == ___idOfChosenService
                                select new
                                {
                                    time = r_time.Time,
                                    time_id = r_time.TimeID,
                                    reg = reg.RegistationID,
                                    date_id = r_date.DateID,
                                    date = r_date.Date
                                }).ToList();
                //foreach(var bok in bookings)
                //{
                //    Console.WriteLine(bok);
                //}

                itemsControlTime.Items.Clear();
                List<TimeButtonUserControl> timeItemsList = new List<TimeButtonUserControl>();
                allTimeSlots.ForEach(time_slot =>
                {
                    if (bookings.Exists(t => t.time.ToString().Remove(5).Equals(time_slot)))
                    {

                    }
                    if ((!bookings.Exists(t => t.time.ToString().Remove(5).Equals(time_slot))))
                    {
                        timeItemsList.Add(new TimeButtonUserControl(time_slot));
                    }
                });
                if (timeItemsList.Count() == 0)
                {
                    inCaseNullTime.Visibility = Visibility.Visible;
                    inCaseNullTime.Text = "На это время нет свободных дат. \nВыберите другой день";
                    return;
                }
                if (timeItemsList.Count() != 0)
                {
                    inCaseNullTime.Visibility = Visibility.Hidden;
                }
                foreach (var item in timeItemsList)
                {
                    itemsControlTime.Items.Add(item);
                }
            }
        }

        private void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            CreateTimeSlot();
        }

        private void EndService_Click(object sender, RoutedEventArgs e)
        {
            ConfirmBookingWindow confirmBookingWindow = new ConfirmBookingWindow(___idOfChosenService, ___idOfChosenDoctor, chosenDate, fullDate);
            confirmBookingWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            confirmBookingWindow.Show();
            //    REGISTRATION _currentRegistration = new REGISTRATION();
            //    REGISTRATION_DATE _currentDateTime = new REGISTRATION_DATE();

            //    int countOfRepeatBookings = 0;
            //    try
            //    {
            //        using (CLINICSEntities db = new CLINICSEntities())
            //        {
            //            var bookings = (from r_date in db.REGISTRATION_DATE
            //                            where DateTime.Compare(r_date.Date, fullDate) == 0 && r_date.TimeID == TimeButtonUserControl.idOfChoosenTime
            //                            select new
            //                            {
            //                                date_id = r_date.DateID,
            //                                date = r_date.Date,
            //                                time_id = r_date.TimeID
            //                            }).ToList();

            //            foreach (var bok in bookings)
            //            {
            //                Console.WriteLine(bok);
            //                countOfRepeatBookings++;
            //            }
            //            if (countOfRepeatBookings == 0)
            //            {
            //                CLINICSEntities.GetContext().REGISTRATION_DATE.Add(_currentDateTime);
            //                _currentDateTime.Date = Convert.ToDateTime(chosenDate);
            //                _currentDateTime.TimeID = TimeButtonUserControl.idOfChoosenTime;
            //                CLINICSEntities.GetContext().SaveChanges();
            //            }
            //            if (countOfRepeatBookings >= 1)
            //            {

            //            }
            //        }
            //    }
            //    catch
            //    {
            //        MessageBox.Show("запись по дате и времени не может быть добавлена");
            //    }

            //    try
            //    {
            //        CLINICSEntities.GetContext().REGISTRATIONs.Add(_currentRegistration);
            //        // _currentRegistration.ClientID = App.currentClient.ClientID;
            //        _currentRegistration.ClientID = App.currentClient.ClientID;

            //        DOCTOR_SERVICE docServ = CLINICSEntities.GetContext().DOCTOR_SERVICE.Where(r => r.ServiceID == ___idOfChosenService && r.DoctorID == ___idOfChosenDoctor).FirstOrDefault();
            //        _currentRegistration.DoctorServiceID = docServ.DoctorServiceID;
            //        _currentRegistration.Status = "запланирована";
            //        _currentRegistration.DoctorServiceID = docServ.DoctorServiceID;
            //        REGISTRATION_DATE reg = CLINICSEntities.GetContext().REGISTRATION_DATE.Where(t => t.Date == fullDate && t.TimeID == TimeButtonUserControl.idOfChoosenTime).FirstOrDefault();
            //        _currentRegistration.DateID = reg.DateID;
            //        CLINICSEntities.GetContext().SaveChanges();

            //        MainWindow.Frame.Navigate(new History(___idOfChosenDoctor, ___idOfChosenService));
            //    }
            //    catch
            //    {
            //        MessageBox.Show("Запись не может быть добавлена");
            //    }

            //}
        }
    }
}
