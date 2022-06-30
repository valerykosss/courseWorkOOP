using CLINICS.models;
using CLINICS.pages;
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
using System.Windows.Shapes;

namespace CLINICS.windows
{
    /// <summary>
    /// Interaction logic for ConfirmBookingWindow.xaml
    /// </summary>
    public partial class ConfirmBookingWindow : Window
    {
        private int ____idOfChosenService { get; set; }
        private int ____idOfChosenDoctor { get; set; }
        public string _chosenDate { get; set; }
        public DateTime _fullDate { get; set; }

        public ConfirmBookingWindow(int ___idOfChosenService, int ___idOfChosenDoctor, string chosenDate, DateTime fullDate)
        {
            InitializeComponent();
            DOCTOR doc = CLINICSEntities.GetContext().DOCTORs.Where(d => d.DoctorID == ___idOfChosenDoctor).FirstOrDefault();
            SERVICE serv = CLINICSEntities.GetContext().SERVICEs.Where(s => s.ServiceID == ___idOfChosenService).FirstOrDefault();
            REGISTRATION_TIME timeOperation = CLINICSEntities.GetContext().REGISTRATION_TIME.Where(d => d.TimeID == TimeButtonUserControl.idOfChoosenTime).FirstOrDefault();
            ____idOfChosenService = ___idOfChosenService;
            ____idOfChosenDoctor = ___idOfChosenDoctor;
            _chosenDate = chosenDate;
            _fullDate = fullDate;


            doctorFIO.Text = doc.DoctorSurname + " " + doc.DoctorName.Remove(1)+". " + doc.DoctorPatronymic.Remove(1);
            serviceName.Text = serv.ServiceName;
            date.Text = _chosenDate;
            time.Text = timeOperation.Time.ToString().Remove(5);
        }

        private void bookOperation_Click(object sender, RoutedEventArgs e)
        {
            REGISTRATION _currentRegistration = new REGISTRATION();
            REGISTRATION_DATE _currentDateTime = new REGISTRATION_DATE();

            int countOfRepeatBookings = 0;
            try
            {
                using (CLINICSEntities db = new CLINICSEntities())
                {
                    var bookings = (from r_date in db.REGISTRATION_DATE
                                    join reg in db.REGISTRATIONs on r_date.DateID equals reg.DateID
                                    join docserv in db.DOCTOR_SERVICE on reg.DoctorServiceID equals docserv.DoctorServiceID
                                    where docserv.ServiceID == ____idOfChosenService && docserv.DoctorID == ____idOfChosenDoctor &&
                                    DateTime.Compare(r_date.Date, _fullDate) == 0 && r_date.TimeID == TimeButtonUserControl.idOfChoosenTime
                                    select new
                                    {
                                        date_id = r_date.DateID,
                                        date = r_date.Date,
                                        time_id = r_date.TimeID
                                    }).ToList();

                    foreach (var bok in bookings)
                    {
                        Console.WriteLine(bok);
                        countOfRepeatBookings++;
                    }
                    if (countOfRepeatBookings >= 1)
                    {
                        MessageBox.Show("Эти дата и время уже заняты другим пользователем");
                        this.Close();
                        return;
                    }
                    if (countOfRepeatBookings == 0)
                    {
                        CLINICSEntities.GetContext().REGISTRATION_DATE.Add(_currentDateTime);
                        _currentDateTime.Date = Convert.ToDateTime(_chosenDate);
                        _currentDateTime.TimeID = TimeButtonUserControl.idOfChoosenTime;
                        CLINICSEntities.GetContext().SaveChanges();
                    }
                }
            }
            catch
            {
                MessageBox.Show("запись по дате и времени не может быть добавлена");
            }

            try
            {
                CLINICSEntities.GetContext().REGISTRATIONs.Add(_currentRegistration);
                // _currentRegistration.ClientID = App.currentClient.ClientID;
                _currentRegistration.ClientID = App.currentClient.ClientID;

                DOCTOR_SERVICE docServ = CLINICSEntities.GetContext().DOCTOR_SERVICE.Where(r => r.ServiceID == ____idOfChosenService && r.DoctorID == ____idOfChosenDoctor).FirstOrDefault();
                _currentRegistration.DoctorServiceID = docServ.DoctorServiceID;
                _currentRegistration.Status = "запланирована";
                _currentRegistration.DoctorServiceID = docServ.DoctorServiceID;
                REGISTRATION_DATE reg = CLINICSEntities.GetContext().REGISTRATION_DATE.Where(t => t.Date == _fullDate && t.TimeID == TimeButtonUserControl.idOfChoosenTime).FirstOrDefault();
                _currentRegistration.DateID = reg.DateID;
                CLINICSEntities.GetContext().SaveChanges();

                MainWindow.Frame.Navigate(new History(____idOfChosenDoctor, ____idOfChosenService));
            }
            catch
            {
                MessageBox.Show("Запись не может быть добавлена");
            }
            this.Close();
        }
    }
}
