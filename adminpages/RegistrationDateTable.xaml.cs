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
using CLINICS.models;

namespace CLINICS.adminpages
{
    /// <summary>
    /// Interaction logic for RegistrationDateTable.xaml
    /// </summary>
    public partial class RegistrationDateTable : Page
    {
        private REGISTRATION_DATE _currentRegistrationDate = new REGISTRATION_DATE();
        public RegistrationDateTable()
        {
            InitializeComponent();
            Load();
            DataContext = _currentRegistrationDate;
            var times = CLINICSEntities.GetContext().REGISTRATION_TIME.ToList();

            TimeIDCombobox.ItemsSource = times;
        }
        private void Load()
        {
            RegistrationDateDataGrid.ItemsSource = CLINICSEntities.GetContext().REGISTRATION_DATE.ToList();
        }
        private void ClearTextBox()
        {
            Date.Clear();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder emptyDataErrors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(Date.Text))
            {
                emptyDataErrors.AppendLine("Введите дату");
            }

            if (TimeIDCombobox.SelectedItem == null)
            {
                emptyDataErrors.AppendLine("Вы не выбрали время");
            }

            if (emptyDataErrors.Length > 0)
            {
                MessageBox.Show(emptyDataErrors.ToString());
                return;
            }
            int flag1 = 1;

            StringBuilder ifValidErrors = new StringBuilder();
            if (!DateTime.TryParse(Date.Text, out DateTime result))
            {
                ifValidErrors.AppendLine("Введите дату корректно");
                Date.Background = Brushes.Gray;
                flag1 = 0;
            }
            if (DateTime.TryParse(Date.Text, out DateTime result11))
            {
                Date.Background = Brushes.White;
            }

            int flag2 = 1;
            if (DateTime.TryParse(Date.Text, out DateTime result1) && result1 < DateTime.Now)
            {
                ifValidErrors.AppendLine("Вы не можете выбрать прошедшую дату");
                Date.Background = Brushes.Gray;
                flag2 = 0;
            }
            int flag3 = 1;
            foreach (REGISTRATION_DATE toCheck in RegistrationDateDataGrid.Items)
            {
                string dateFromDataGrid = toCheck.Date.ToString().Remove(10);
                REGISTRATION_TIME registrTime = CLINICSEntities.GetContext().REGISTRATION_TIME.Where(r => r.TimeID.ToString() == toCheck.TimeID.ToString()).Single();
                string timeFromDataGrid = registrTime.Time.ToString();
                string resultForDateAndTime = dateFromDataGrid + timeFromDataGrid;

                string resultFromInput = Date.Text + TimeIDCombobox.Text;
                if (resultForDateAndTime.Equals(resultFromInput))
                {
                    MessageBox.Show(resultForDateAndTime);
                    MessageBox.Show(resultFromInput);
                    flag3 = 0;

                }
            }

            if (ifValidErrors.Length > 0)
            {
                MessageBox.Show(ifValidErrors.ToString());
                return;
            }

            if (flag1 == 1 && flag2 == 1 && flag3 == 1)
            {
                REGISTRATION_DATE currentRegistrationDate = new REGISTRATION_DATE();
                currentRegistrationDate.Date = Convert.ToDateTime(Date.Text);

                CLINICSEntities.GetContext().REGISTRATION_DATE.Add(currentRegistrationDate);
                try
                {
                    REGISTRATION_TIME selectedTime = (REGISTRATION_TIME)TimeIDCombobox.SelectedItem;
                    currentRegistrationDate.TimeID = selectedTime.TimeID;
                }
                catch
                {
                    MessageBox.Show("");
                }
                try
                {
                    CLINICSEntities.GetContext().SaveChanges();
                    MessageBox.Show("Успешно!");
                    Date.Background = Brushes.White;
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

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            var elementsToRemove = RegistrationDateDataGrid.SelectedItems.Cast<REGISTRATION_DATE>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить {elementsToRemove.Count()} элемент(ов)?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                try
                {
                    CLINICSEntities.GetContext().REGISTRATION_DATE.RemoveRange(elementsToRemove);
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
            REGISTRATION_DATE sel = RegistrationDateDataGrid.SelectedItem as REGISTRATION_DATE;

            _currentRegistrationDate = sel;

            TimeIDCombobox.SelectedValue = _currentRegistrationDate.TimeID;
            Date.Text = _currentRegistrationDate.Date.ToString().Remove(10);
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                REGISTRATION_TIME selectedTime = (REGISTRATION_TIME)TimeIDCombobox.SelectedItem;
                _currentRegistrationDate.TimeID = selectedTime.TimeID;
               _currentRegistrationDate.Date = Convert.ToDateTime(Date.Text);

                CLINICSEntities.GetContext().Entry(_currentRegistrationDate).State = System.Data.Entity.EntityState.Modified;

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
