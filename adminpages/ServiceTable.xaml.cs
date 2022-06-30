using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for ServiceTable.xaml
    /// </summary>
    public partial class ServiceTable : Page
    {
        private SERVICE _currentService = new SERVICE();
        private void Load()
        {
            ServiceDataGrid.ItemsSource = CLINICSEntities.GetContext().SERVICEs.ToList();
        }
        void letters(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0))
            {
                e.Handled = true; //не обрабатывать введеный символ
            }
        }
        public ServiceTable()
        {
            InitializeComponent();
            Load();
            DataContext = _currentService;
            ServiceName.PreviewTextInput += new TextCompositionEventHandler(letters);
        }
        private void ClearTextBox()
        {
            ServiceName.Clear();
        }
        private void add_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder emptyDataErrors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(ServiceName.Text))
            {
                emptyDataErrors.AppendLine("Введите название операции");
            }
            if (emptyDataErrors.Length > 0)
            {
                MessageBox.Show(emptyDataErrors.ToString());
                return;
            }

            string serviceName = ServiceName.Text.Trim();
            Regex r = new Regex(@"^[А-Я][а-я ]+$");
            Match m1 = r.Match(serviceName);

            StringBuilder regexDataErrors = new StringBuilder();

            int flag1 = 1;
            if (!m1.Success)
            {
                flag1 = 0;
                regexDataErrors.AppendLine("Введите корректое название операции");
                ServiceName.Background = Brushes.Gray;
            }
            SERVICE service = CLINICSEntities.GetContext().SERVICEs.FirstOrDefault(p => p.ServiceName == serviceName);
            int flag2 = 1;
            if (m1.Success && service != null)
            {
                flag2 = 0;
                regexDataErrors.AppendLine("Такая операция уже существует");
                ServiceName.Background = Brushes.Gray;
            }
            if (m1.Success && service == null)
            {
                ServiceName.Background = Brushes.White;
            }
            if (regexDataErrors.Length > 0)
            {
                MessageBox.Show(regexDataErrors.ToString());
                return;
            }
            if (flag1 == 1 && flag2 == 1)
            {
                SERVICE _currentService = new SERVICE();
                _currentService.ServiceName = ServiceName.Text;

                CLINICSEntities.GetContext().SERVICEs.Add(_currentService);
                try
                {
                    CLINICSEntities.GetContext().SaveChanges();
                    MessageBox.Show("Успешно!");
                    ServiceName.Background = Brushes.White;
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
            var elementsToRemove = ServiceDataGrid.SelectedItems.Cast<SERVICE>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить {elementsToRemove.Count()} элемент(ов)?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                try
                {
                    CLINICSEntities.GetContext().SERVICEs.RemoveRange(elementsToRemove);
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
            ServiceName.Background = Brushes.White;
            SERVICE sel = ServiceDataGrid.SelectedItem as SERVICE;

            _currentService = sel;

            ServiceName.Text = _currentService.ServiceName;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder emptyDataErrors = new StringBuilder();

                if (string.IsNullOrWhiteSpace(ServiceName.Text))
                {
                    emptyDataErrors.AppendLine("Введите название операции");
                }
                if (emptyDataErrors.Length > 0)
                {
                    MessageBox.Show(emptyDataErrors.ToString());
                    return;
                }

                string serviceName = ServiceName.Text.Trim();

                Regex r = new Regex(@"^[А-Я][а-я ]+$");
                Match m1 = r.Match(serviceName);

                StringBuilder regexDataErrors = new StringBuilder();

                int flag1 = 1;
                if (!m1.Success)
                {
                    flag1 = 0;
                    regexDataErrors.AppendLine("Введите корректое название операции");
                    ServiceName.Background = Brushes.Gray;
                }
                SERVICE service = CLINICSEntities.GetContext().SERVICEs.FirstOrDefault(p => p.ServiceName == serviceName);
                int flag2 = 1;
                if (m1.Success && service != null && ServiceName.Text != _currentService.ServiceName)
                {
                    flag2 = 0;
                    regexDataErrors.AppendLine("Такая операция уже существует");
                    ServiceName.Background = Brushes.Gray;
                }
                if (m1.Success && service != null && ServiceName.Text == _currentService.ServiceName)
                {
                    flag2 = 1;
                    ServiceName.Background = Brushes.White;
                }
                if (m1.Success && service == null)
                {
                    ServiceName.Background = Brushes.White;
                }
                if (regexDataErrors.Length > 0)
                {
                    MessageBox.Show(regexDataErrors.ToString());
                    return;
                }
                if (flag1 == 1 && flag2 == 1)
                {
                    _currentService.ServiceName = ServiceName.Text;

                    CLINICSEntities.GetContext().Entry(_currentService).State = System.Data.Entity.EntityState.Modified;
                    try
                    {
                        CLINICSEntities.GetContext().SaveChanges();
                        MessageBox.Show("Изменения внесены");
                        ServiceName.Background = Brushes.White;
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
