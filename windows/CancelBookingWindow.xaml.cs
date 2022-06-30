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
    /// Interaction logic for CancelBookingWindow.xaml
    /// </summary>
    public partial class CancelBookingWindow : Window
    {
        private int _idOfChosenRegistration { get; set; }
        public CancelBookingWindow(int idOfChosenRegistration)
        {
            InitializeComponent();
            _idOfChosenRegistration = idOfChosenRegistration;
        }

        private void cancelBookingRenewStatus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                REGISTRATION selected_registration = CLINICSEntities.GetContext().REGISTRATIONs.Where(p => p.RegistationID == _idOfChosenRegistration).FirstOrDefault();
                selected_registration.Status = "отменена";
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
                successText.Visibility = Visibility.Visible;
                okShowWithNewStatus.Visibility = Visibility.Visible;
                cancelBookingRenewStatus.Visibility = Visibility.Hidden;
                clickText1.Visibility = Visibility.Hidden;
                clickText2.Visibility = Visibility.Hidden;
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

        private void okShowWithNewStatus_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Frame.Content = null;
            History history = new History();
            MainWindow.Frame.Content= history;
            this.Close();
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
