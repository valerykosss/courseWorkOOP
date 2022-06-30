using CLINICS.models;
using CLINICS.pages;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
    /// Interaction logic for LeaveCommentWindow.xaml
    /// </summary>
    public partial class LeaveCommentWindow : Window
    {
        private int _idOfChosenRegistration { get; set; }
        private int ______idOfChosenDoctor { get; set; }
        public LeaveCommentWindow(int _____idOfChosenDoctor, int idOfChosenRegistration)
        {
            ______idOfChosenDoctor = _____idOfChosenDoctor;
            _idOfChosenRegistration = idOfChosenRegistration;
            InitializeComponent();
        }
         private void CloseWindow_Click(object sender, RoutedEventArgs e)
         {
            this.Close();
         }

        private void submitComment_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder emptyDataErrors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(TextBoxInput.Text.Text))
            {
                emptyDataErrors.AppendLine("Введите комметарий");
            }
            if (sliderRating.Value == 0)
            {
                emptyDataErrors.AppendLine("Потяните слайдер, чтобы поставить оценку");
            }
            if (emptyDataErrors.Length > 0)
            {
                MessageBox.Show(emptyDataErrors.ToString());
                return;
            }
            DOCTOR_RATING _currentRating = new DOCTOR_RATING();
            _currentRating.ClientID = App.currentClient.ClientID;
            _currentRating.DoctorID = ______idOfChosenDoctor;
            _currentRating.Rating = sliderRating.Value;
            _currentRating.Comment = TextBoxInput.Text.Text;

            CLINICSEntities.GetContext().DOCTOR_RATING.Add(_currentRating);
            try
            {
                CLINICSEntities.GetContext().SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                //MessageBox.Show(ex.Message);
                foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                {
                    MessageBox.Show("Object: " + validationError.Entry.Entity.ToString());
                    MessageBox.Show(" ");

                    foreach (DbValidationError err in validationError.ValidationErrors)
                    {
                        MessageBox.Show(err.ErrorMessage + " ");

                    }
                }
            }
            try
            {
                REGISTRATION selected_registration = CLINICSEntities.GetContext().REGISTRATIONs.Where(p => p.RegistationID == _idOfChosenRegistration).FirstOrDefault();
                selected_registration.Status = "оценена";
                try
                {
                    CLINICSEntities.GetContext().Entry(selected_registration).State = System.Data.Entity.EntityState.Modified;
                    CLINICSEntities.GetContext().SaveChanges();

                }
                catch (DbEntityValidationException ex)
                {
                    //MessageBox.Show(ex.Message);
                    foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                    {
                        MessageBox.Show("Object: " + validationError.Entry.Entity.ToString());

                        foreach (DbValidationError err in validationError.ValidationErrors)
                        {
                            MessageBox.Show(err.ErrorMessage + " ");

                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                //MessageBox.Show(ex.Message);
                foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                {
                    MessageBox.Show("Object: " + validationError.Entry.Entity.ToString());
                    MessageBox.Show(" ");

                    foreach (DbValidationError err in validationError.ValidationErrors)
                    {
                        MessageBox.Show(err.ErrorMessage + " ");

                    }
                }
            }
            try
            {
                History.BookingsUserHistory.Children.Clear();
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
                        History.InCaseNullBookingsStackpanel.Visibility = Visibility.Visible;
                        History.InCaseNullBookings.Text = "Вы еще никуда не записаны. \nЗапишитесь на операцию прямо сейчас!";
                        return;
                    }
                    else
                    {
                        History.InCaseNullBookingsStackpanel.Visibility = Visibility.Hidden;
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
                }
            }
            catch
            {

            }
            this.Close();
        }
            private void sliderRating_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
            {
                ((Slider)sender).SelectionEnd = e.NewValue;
            }
    }
}
