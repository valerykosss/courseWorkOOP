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
    /// Interaction logic for RegistrationTimeTable.xaml
    /// </summary>
    public partial class RegistrationTimeTable : Page
    {
        private REGISTRATION_TIME _currentRegistrationTime = new REGISTRATION_TIME();
        public RegistrationTimeTable()
        {
            InitializeComponent();
            Load();
            DataContext = _currentRegistrationTime;
        }
        private void Load()
        {
            RegistrationTimeDataGrid.ItemsSource = CLINICSEntities.GetContext().REGISTRATION_TIME.ToList();
        }
        private void ClearTextBox()
        {
            Time.Clear();
        }
        private void add_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder emptyDataErrors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(Time.Text))
            {
                emptyDataErrors.AppendLine("Введите время");
            }
            if (emptyDataErrors.Length > 0)
            {
                MessageBox.Show(emptyDataErrors.ToString());
                return;
            }
            int flag1 = 1;

            StringBuilder ifValidErrors = new StringBuilder();
            if (!TimeSpan.TryParse(Time.Text, out TimeSpan result1))
            {
                flag1 = 0;
                ifValidErrors.AppendLine("Введите время корректно(чч:мм:сс)");
                Time.Background = Brushes.Gray;
            }

            TimeSpan? time = TimeSpan.Parse(Time.Text);
            REGISTRATION_TIME regTime = CLINICSEntities.GetContext().REGISTRATION_TIME.FirstOrDefault(p => p.Time == time);//ToString("hh\\:mm")

            int flag2 = 1;
            if (TimeSpan.TryParse(Time.Text, out TimeSpan result2) && regTime != null)
            {
                flag2 = 0;
                Time.Background = Brushes.Gray;
                MessageBox.Show("Такое время уже имеется в таблице");
            }
            if (TimeSpan.TryParse(Time.Text, out TimeSpan result3) && regTime == null)
            {
                flag2 = 1;
                Time.Background = Brushes.White;
            }
            if (ifValidErrors.Length > 0)
            {
                MessageBox.Show(ifValidErrors.ToString());
                return;
            }

            if (flag1 == 1 && flag2 == 1)
            {
                REGISTRATION_TIME _currentRegistrationTime = new REGISTRATION_TIME();
                _currentRegistrationTime.Time = TimeSpan.Parse(Time.Text);

                CLINICSEntities.GetContext().REGISTRATION_TIME.Add(_currentRegistrationTime);

                try
                {
                    CLINICSEntities.GetContext().SaveChanges();
                    MessageBox.Show("Успешно!");
                    Time.Background = Brushes.White;
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
            var elementsToRemove = RegistrationTimeDataGrid.SelectedItems.Cast<REGISTRATION_TIME>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить {elementsToRemove.Count()} элемент(ов)?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                try
                {
                    CLINICSEntities.GetContext().REGISTRATION_TIME.RemoveRange(elementsToRemove);
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
            Time.Background = Brushes.White;
            REGISTRATION_TIME sel = RegistrationTimeDataGrid.SelectedItem as REGISTRATION_TIME;

            _currentRegistrationTime = sel;

            Time.Text = _currentRegistrationTime.Time.ToString();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder emptyDataErrors = new StringBuilder();

                if (string.IsNullOrWhiteSpace(Time.Text))
                {
                    emptyDataErrors.AppendLine("Введите время");
                }
                if (emptyDataErrors.Length > 0)
                {
                    MessageBox.Show(emptyDataErrors.ToString());
                    return;
                }

                StringBuilder ifValidErrors = new StringBuilder();
                int flag1 = 1;

                if (!TimeSpan.TryParse(Time.Text, out TimeSpan result1))
                {
                    flag1 = 0;
                    ifValidErrors.AppendLine("Введите время корректно(чч:мм:сс)");
                    Time.Background = Brushes.Gray;
                }
                int flag2 = 1;
                try
                {
                    TimeSpan? time = TimeSpan.Parse(Time.Text);
                    REGISTRATION_TIME regTime = CLINICSEntities.GetContext().REGISTRATION_TIME.FirstOrDefault(p => p.Time == time);//ToString("hh\\:mm")
                    if (TimeSpan.TryParse(Time.Text, out TimeSpan result4) && regTime != null && Time.Text != _currentRegistrationTime.Time.ToString())
                    {
                        flag2 = 0;
                        Time.Background = Brushes.Gray;
                        ifValidErrors.AppendLine("Такое время уже имеется в таблице");
                    }
                    if (TimeSpan.TryParse(Time.Text, out TimeSpan result6) && regTime != null && Time.Text == _currentRegistrationTime.Time.ToString())
                    {
                        flag2 = 1;
                        Time.Background = Brushes.White;
                    }
                    if (TimeSpan.TryParse(Time.Text, out TimeSpan result5) && regTime == null)
                    {
                        Time.Background = Brushes.White;
                    }
                    if (ifValidErrors.Length > 0)
                    {
                        MessageBox.Show(ifValidErrors.ToString());
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("Время введено некорретно");
                }

                if (flag2 == 1 && flag1 == 1)
                {
                    _currentRegistrationTime.Time = TimeSpan.Parse(Time.Text);

                    CLINICSEntities.GetContext().Entry(_currentRegistrationTime).State = System.Data.Entity.EntityState.Modified;
                    try
                    {
                        CLINICSEntities.GetContext().SaveChanges();
                        MessageBox.Show("Изменения внесены");
                        Load();
                        Time.Background = Brushes.White;
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

