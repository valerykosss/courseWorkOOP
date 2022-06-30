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
    /// Interaction logic for EndServiceByDoctor.xaml
    /// </summary>
    public partial class EndServiceByDoctor : Window
    {
        //public static Frame DoctorFrame { get; set; }
        private int id;
        public EndServiceByDoctor(int idOfChosenRegistration)
        {
            InitializeComponent();
            //doctorFrame.Content = new MakeSureToEndService();
            //DoctorFrame = doctorFrame;
            id = idOfChosenRegistration;
        }
        private void renewOperationStatus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                REGISTRATION selected_registration = CLINICSEntities.GetContext().REGISTRATIONs.Where(p => p.RegistationID == id).FirstOrDefault();
                selected_registration.Status = "проведена";
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
                //EndServiceByDoctor.DoctorFrame.Content = new ServiceCompletedDoctor();
                //одновить список
                //try
                //{
                //    using (CLINICSEntities db = new CLINICSEntities())
                //    {
                //        var doctorBookings = (from r_time in db.REGISTRATION_TIME
                //                              join r_date in db.REGISTRATION_DATE on r_time.TimeID equals r_date.TimeID
                //                              join reg in db.REGISTRATIONs on r_date.DateID equals reg.DateID
                //                              join doc_serv in db.DOCTOR_SERVICE on reg.DoctorServiceID equals doc_serv.DoctorServiceID
                //                              join doc in db.DOCTORs on doc_serv.DoctorID equals doc.DoctorID
                //                              join serv in db.SERVICEs on doc_serv.ServiceID equals serv.ServiceID
                //                              join client in db.CLIENTs on reg.ClientID equals client.ClientID
                //                              where reg.DoctorServiceID == doc_serv.DoctorServiceID &&
                //                                    doc.DoctorSurname == MainWindow.clientSurname
                //                              orderby reg.RegistationID descending
                //                              select new
                //                              {
                //                                  date = r_date.Date,
                //                                  time = r_time.Time,
                //                                  service = serv.ServiceName,
                //                                  client = client.ClientSurname + " " + client.ClientName,
                //                                  status = "операция " + reg.Status,
                //                                  reg_id = reg.RegistationID

                //                              }).ToList();

                //        DoctorHistory.BookingDoctorHistory.Children.Clear();
                //        foreach (var bok in doctorBookings)
                //        {
                //            var buf = new DoctorHistoryUserControl(bok.date.ToString().Remove(10), bok.time.ToString().Remove(5),
                //                bok.service, bok.client, bok.status, bok.reg_id);
                //            if (bok.status == "операция запланирована")
                //            {
                //                DoctorHistoryUserControl.GridButtonToCollapse.Visibility = Visibility.Visible;
                //                DoctorHistory.BookingDoctorHistory.Children.Add(buf);
                //            }
                //            else
                //            {
                //                if ((bok.status == "операция проведена"))
                //                {
                //                    DoctorHistoryUserControl.GridButtonToCollapse.Visibility = Visibility.Collapsed;
                //                    DoctorHistory.BookingDoctorHistory.Children.Add(buf);
                //                }

                //            }
                //        }
                //    };
                //}
                //catch
                //{

                //}
                //ServiceCompletedDoctorWindow serviceCompleted = new ServiceCompletedDoctorWindow();
                //serviceCompleted.Show();
                //this.Close();
                successText.Visibility = Visibility.Visible;
                okShowWithNewStatus.Visibility = Visibility.Visible;
                renewOperationStatus.Visibility = Visibility.Hidden;
                clickText1.Visibility = Visibility.Hidden;
                clickText2.Visibility=Visibility.Hidden;
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
        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            //DoctorHistoryUserControl.EndServiceByDoctor.Close();
            this.Close();
        }

        private void okShowWithNewStatus_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Frame.Content = null;
            DoctorHistory doctorHistory = new DoctorHistory();
            MainWindow.Frame.Content = doctorHistory;
            //try
            //{
            //    using (CLINICSEntities db = new CLINICSEntities())
            //    {
            //        var doctorBookings = (from r_time in db.REGISTRATION_TIME
            //                              join r_date in db.REGISTRATION_DATE on r_time.TimeID equals r_date.TimeID
            //                              join reg in db.REGISTRATIONs on r_date.DateID equals reg.DateID
            //                              join doc_serv in db.DOCTOR_SERVICE on reg.DoctorServiceID equals doc_serv.DoctorServiceID
            //                              join doc in db.DOCTORs on doc_serv.DoctorID equals doc.DoctorID
            //                              join serv in db.SERVICEs on doc_serv.ServiceID equals serv.ServiceID
            //                              join client in db.CLIENTs on reg.ClientID equals client.ClientID
            //                              where reg.DoctorServiceID == doc_serv.DoctorServiceID &&
            //                                    doc.DoctorSurname == MainWindow.clientSurname
            //                              orderby reg.RegistationID descending
            //                              select new
            //                              {
            //                                  date = r_date.Date,
            //                                  time = r_time.Time,
            //                                  service = serv.ServiceName,
            //                                  client = client.ClientSurname + " " + client.ClientName,
            //                                  status = "операция " + reg.Status,
            //                                  reg_id = reg.RegistationID

            //                              }).ToList();

            //        DoctorHistory.BookingDoctorHistory.Children.Clear();
            //        foreach (var bok in doctorBookings)
            //        {
            //            var buf = new DoctorHistoryUserControl(bok.date.ToString().Remove(10), bok.time.ToString().Remove(5),
            //                bok.service, bok.client, bok.status, bok.reg_id);
            //            if (bok.status == "операция запланирована")
            //            {
            //                DoctorHistoryUserControl.GridButtonToCollapse.Visibility = Visibility.Visible;
            //                DoctorHistory.BookingDoctorHistory.Children.Add(buf);
            //            }
            //            else
            //            {
            //                if ((bok.status == "операция проведена"))
            //                {
            //                    DoctorHistoryUserControl.GridButtonToCollapse.Visibility = Visibility.Hidden;
            //                    DoctorHistory.BookingDoctorHistory.Children.Add(buf);
            //                }

            //            }
            //        }
            //    };
            //}
            //catch
            //{

            //}
            this.Close();
        }
    }
}
