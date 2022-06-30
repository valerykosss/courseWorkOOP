using CLINICS.models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CLINICS.adminpages
{
    /// <summary>
    /// Interaction logic for RegistrationTable.xaml
    /// </summary>
    public partial class RegistrationTable : Page
    {
        private int currentDateId = 0;

        private REGISTRATION _currentRegistration = new REGISTRATION();

        public List<DOCTOR_SERVICE> doctorServices = new List<DOCTOR_SERVICE>();
        public List<DOCTOR> doctors = new List<DOCTOR>();
        public List<CLIENT> clients = new List<CLIENT>();
        public List<REGISTRATION_DATE> regDateTimes = new List<REGISTRATION_DATE>();
        public List<REGISTRATION> Registrations = new List<REGISTRATION>();

        public RegistrationTable()
        {
            InitializeComponent();
            DoctorServiceIDCombobox.ItemsSource = CLINICSEntities.GetContext().DOCTOR_SERVICE.ToList();
            ClientIDCombobox.ItemsSource = CLINICSEntities.GetContext().CLIENTs.ToList();
            DateIDCombobox.ItemsSource = CLINICSEntities.GetContext().REGISTRATION_DATE.ToList();

            Registrations = CLINICSEntities.GetContext().REGISTRATIONs.ToList();

            Load();

            DataContext = _currentRegistration;
            //var doctorServices = CLINICSEntities.GetContext().DOCTOR_SERVICE.ToList();
            //var clients = CLINICSEntities.GetContext().CLIENTs.ToList();
            //var dateTimes = CLINICSEntities.GetContext().REGISTRATION_DATE.ToList();
        }
        private List<CLIENT> getClientsByDateID(int curDateId)
        {
            List<CLIENT> ownedClients = Registrations.Where(regs => regs.DateID == curDateId).Select(ds => ds.CLIENT).ToList();
            List<CLIENT> allClients = CLINICSEntities.GetContext().CLIENTs.ToList();
            List<CLIENT> result = allClients.Where(s => ownedClients.All(s2 => s2.ClientID != s.ClientID)).ToList();

            return result;
        }

        private List<DOCTOR_SERVICE> getDoctorServicesByDateID(int curDateId)
        {
            List<DOCTOR_SERVICE> ownedDoctorServices = Registrations.Where(regs => regs.DateID == curDateId).Select(ds => ds.DOCTOR_SERVICE).ToList();
            List<DOCTOR_SERVICE> allDoctorServices = CLINICSEntities.GetContext().DOCTOR_SERVICE.ToList();
            List<DOCTOR_SERVICE> result = allDoctorServices.Where(s => ownedDoctorServices.All(s2 => s2.DoctorServiceID != s.DoctorServiceID)).ToList();

            return result;
        }

        //private List<REGISTRATION_DATE> getDateIDByDoctorServices(int curDocServId)
        //{
        //    List<REGISTRATION_DATE> ownedDates = Registrations.Where(regs => regs.DoctorServiceID == curDocServId).Select(ds => ds.REGISTRATION_DATE).ToList();
        //    List<REGISTRATION_DATE> allDates = CLINICSEntities.GetContext().REGISTRATION_DATE.ToList();
        //    List<REGISTRATION_DATE> result = ownedDates.Where(s => allDates.All(s2 => s2.DateID != s.DateID)).ToList();

        //    return result;
        //}

        //private List<REGISTRATION_DATE> getDateIDByClients(int curClientId)
        //{
        //    List<REGISTRATION_DATE> ownedDates = Registrations.Where(regs => regs.ClientID == curClientId).Select(ds => ds.REGISTRATION_DATE).ToList();
        //    List<REGISTRATION_DATE> allDates = CLINICSEntities.GetContext().REGISTRATION_DATE.ToList();
        //    List<REGISTRATION_DATE> result = ownedDates.Where(s => allDates.All(s2 => s2.DateID != s.DateID)).ToList();

        //    return result;
        //}
        private void DateIDCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentDateId = (int)(sender as ComboBox).SelectedValue;
            DoctorServiceIDCombobox.ItemsSource = getDoctorServicesByDateID(currentDateId);
            ClientIDCombobox.ItemsSource = getClientsByDateID(currentDateId);
        }


        private void Load()
        {
            RegistrationDataGrid.ItemsSource = CLINICSEntities.GetContext().REGISTRATIONs.ToList();
        }
        private void ClearTextBox()
        {
        }
        private void add_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder emptyDataErrors = new StringBuilder();

            if (ClientIDCombobox.SelectedItem == null)
            {
                emptyDataErrors.AppendLine("Вы не выбрали телефон клиента");
            }
            if (DoctorServiceIDCombobox.SelectedItem == null)
            {
                emptyDataErrors.AppendLine("Вы не выбрали операцию и врача");
            }
            if (DateIDCombobox.SelectedItem == null)
            {
                emptyDataErrors.AppendLine("Вы не выбрали дату и время");
            }
            if (StatusCombobox.SelectedItem == null)
            {
                emptyDataErrors.AppendLine("Вы не выбрали статус операции");
            }
            if (emptyDataErrors.Length > 0)
            {
                MessageBox.Show(emptyDataErrors.ToString());
                return;
            }

            REGISTRATION _currentRegistration = new REGISTRATION();

            CLINICSEntities.GetContext().REGISTRATIONs.Add(_currentRegistration);
            try
            {
                CLIENT selectedClient = (CLIENT)ClientIDCombobox.SelectedItem;
                DOCTOR_SERVICE selectedService = (DOCTOR_SERVICE)DoctorServiceIDCombobox.SelectedItem;
                REGISTRATION_DATE selectedDate = (REGISTRATION_DATE)DateIDCombobox.SelectedItem;
                _currentRegistration.DoctorServiceID = selectedService.DoctorServiceID;
                _currentRegistration.ClientID = selectedClient.ClientID;
                _currentRegistration.DateID = selectedDate.DateID;
                _currentRegistration.Status = StatusCombobox.Text;
            }
            catch
            {
                MessageBox.Show("Errors!!!");
            }
            try
            {
                CLINICSEntities.GetContext().SaveChanges();
                MessageBox.Show("Успешно!");
                Load();
                ClearTextBox();
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
        private void delete_Click(object sender, RoutedEventArgs e)
        {
            var elementsToRemove = RegistrationDataGrid.SelectedItems.Cast<REGISTRATION>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить {elementsToRemove.Count()} элемент(ов)?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                try
                {
                    CLINICSEntities.GetContext().REGISTRATIONs.RemoveRange(elementsToRemove);
                    CLINICSEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    Load();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            REGISTRATION sel = RegistrationDataGrid.SelectedItem as REGISTRATION;

            _currentRegistration = sel;

            ClientIDCombobox.SelectedValue = _currentRegistration.ClientID;
            DoctorServiceIDCombobox.SelectedValue = _currentRegistration.DoctorServiceID;
            DateIDCombobox.SelectedValue = _currentRegistration.DateID;
            StatusCombobox.Text = _currentRegistration.Status;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                CLIENT selectedClient = (CLIENT)ClientIDCombobox.SelectedItem;
                DOCTOR_SERVICE selectedService = (DOCTOR_SERVICE)DoctorServiceIDCombobox.SelectedItem;
                REGISTRATION_DATE selectedDate = (REGISTRATION_DATE)DateIDCombobox.SelectedItem;
                _currentRegistration.DoctorServiceID = selectedService.DoctorServiceID;
                _currentRegistration.ClientID = selectedClient.ClientID;
                _currentRegistration.DateID = selectedDate.DateID;
                _currentRegistration.Status = StatusCombobox.Text;

                CLINICSEntities.GetContext().Entry(_currentRegistration).State = System.Data.Entity.EntityState.Modified;

                CLINICSEntities.GetContext().SaveChanges();
                MessageBox.Show("Изменения внесены");
                Load();
                ClearTextBox();

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
        }
    }
}
