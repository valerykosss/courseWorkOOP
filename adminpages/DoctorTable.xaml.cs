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
    /// Interaction logic for DoctorTable.xaml
    /// </summary>
    public partial class DoctorTable : Page
    {
        private DOCTOR _currentDoctor = new DOCTOR();
        void letters(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0))
            {
                e.Handled = true; //не обрабатывать введеный символ
            }
        }
        void lettersAndNumbers(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetterOrDigit(e.Text, 0))
            {
                e.Handled = true; //не обрабатывать введеный символ
            }
        }
        public DoctorTable()
        {
            InitializeComponent();
            Load();
            DataContext = _currentDoctor;
            DoctorName.PreviewTextInput += new TextCompositionEventHandler(letters);
            DoctorPatronymic.PreviewTextInput += new TextCompositionEventHandler(letters);
            DoctorSurname.PreviewTextInput += new TextCompositionEventHandler(letters);
        }
        private void Load()
        {
            DoctorDataGrid.ItemsSource = CLINICSEntities.GetContext().DOCTORs.ToList();
        }
        private void ClearTextBox()
        {
            DoctorName.Clear();
            DoctorSurname.Clear();
            DoctorPatronymic.Clear();

        }
        private void add_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder emptyDataErrors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(DoctorName.Text))
            {
                emptyDataErrors.AppendLine("Введите имя доктора");
            }
            if (string.IsNullOrWhiteSpace(DoctorSurname.Text))
            {
                emptyDataErrors.AppendLine("Введите фамилию доктора");
            }
            if (string.IsNullOrWhiteSpace(DoctorPatronymic.Text))
            {
                emptyDataErrors.AppendLine("Введите отчество доктора");
            }
            if (emptyDataErrors.Length > 0)
            {
                MessageBox.Show(emptyDataErrors.ToString());
                return;
            }
            string doctorName = DoctorName.Text.Trim();
            string doctorSurname = DoctorSurname.Text.Trim();
            string doctorPatronymic = DoctorPatronymic.Text.Trim();

            Regex r = new Regex(@"^[А-Я][а-я]+$");
            Match m1 = r.Match(doctorName);
            Match m2 = r.Match(doctorSurname);
            Match m3 = r.Match(doctorPatronymic);

            int flag1 = 1;
            StringBuilder regexDataErrors = new StringBuilder();
            if (!m1.Success)
            {
                flag1 = 0;
                regexDataErrors.AppendLine("Введите корректое имя");
                DoctorName.Background = Brushes.Gray;
            }
            else
            {
                DoctorName.Background = Brushes.White;
            }

            int flag2 = 1;
            if (!m2.Success)
            {
                flag2 = 0;
                regexDataErrors.AppendLine("Введите корректную фамилию");
                DoctorSurname.Background = Brushes.Gray;
            }
            else
            {
                DoctorSurname.Background = Brushes.White;
            }

            int flag3 = 1;
            if (!m3.Success)
            {
                flag3 = 0;
                regexDataErrors.AppendLine("Введите корректное отчество");
                DoctorPatronymic.Background = Brushes.Gray;
            }
            else
            {
                DoctorPatronymic.Background = Brushes.White;
            }

            if (regexDataErrors.Length > 0)
            {
                MessageBox.Show(regexDataErrors.ToString());
                return;
            }

            if (flag1 == 1 && flag1 == 1 && flag3 == 1)
            {
                DOCTOR _currentDoctor = new DOCTOR();
                _currentDoctor.DoctorName = DoctorName.Text;
                _currentDoctor.DoctorSurname = DoctorSurname.Text;
                _currentDoctor.DoctorPatronymic = DoctorPatronymic.Text;

                CLINICSEntities.GetContext().DOCTORs.Add(_currentDoctor);
                try
                {
                    CLINICSEntities.GetContext().SaveChanges();
                    MessageBox.Show("Успешно!");
                    DoctorName.Background = Brushes.White;
                    DoctorSurname.Background = Brushes.White;
                    DoctorPatronymic.Background = Brushes.White;
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
            var elementsToRemove = DoctorDataGrid.SelectedItems.Cast<DOCTOR>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить {elementsToRemove.Count()} элемент(ов)?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                try
                {
                    CLINICSEntities.GetContext().DOCTORs.RemoveRange(elementsToRemove);
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
            DoctorName.Background = Brushes.White;
            DoctorSurname.Background = Brushes.White;
            DoctorPatronymic.Background = Brushes.White;

            DOCTOR sel = DoctorDataGrid.SelectedItem as DOCTOR;

            _currentDoctor = sel;

            DoctorName.Text = _currentDoctor.DoctorName;
            DoctorSurname.Text = _currentDoctor.DoctorSurname;
            DoctorPatronymic.Text = _currentDoctor.DoctorPatronymic;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder emptyDataErrors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(DoctorName.Text))
            {
                emptyDataErrors.AppendLine("Введите имя доктора");
            }
            if (string.IsNullOrWhiteSpace(DoctorSurname.Text))
            {
                emptyDataErrors.AppendLine("Введите фамилию доктора");
            }
            if (string.IsNullOrWhiteSpace(DoctorPatronymic.Text))
            {
                emptyDataErrors.AppendLine("Введите отчество доктора");
            }
            if (emptyDataErrors.Length > 0)
            {
                MessageBox.Show(emptyDataErrors.ToString());
                return;
            }
            string doctorName = DoctorName.Text.Trim();
            string doctorSurname = DoctorSurname.Text.Trim();
            string doctorPatronymic = DoctorPatronymic.Text.Trim();

            Regex r = new Regex(@"^[А-Я][а-я]+$");
            Match m1 = r.Match(doctorName);
            Match m2 = r.Match(doctorSurname);
            Match m3 = r.Match(doctorPatronymic);

            int flag1 = 1;
            StringBuilder regexDataErrors = new StringBuilder();
            if (!m1.Success)
            {
                flag1 = 0;
                regexDataErrors.AppendLine("Введите корректое имя");
                DoctorName.Background = Brushes.Gray;
            }
            else
            {
                DoctorName.Background = Brushes.White;
            }

            int flag2 = 1;
            if (!m2.Success)
            {
                flag2 = 0;
                regexDataErrors.AppendLine("Введите корректную фамилию");
                DoctorSurname.Background = Brushes.Gray;
            }
            else
            {
                DoctorSurname.Background = Brushes.White;
            }

            int flag3 = 1;
            if (!m3.Success)
            {
                flag3 = 0;
                regexDataErrors.AppendLine("Введите корректное отчество");
                DoctorPatronymic.Background = Brushes.Gray;
            }
            else
            {
                DoctorPatronymic.Background = Brushes.White;
            }

            if (regexDataErrors.Length > 0)
            {
                MessageBox.Show(regexDataErrors.ToString());
                return;
            }

            if (flag1 == 1 && flag1 == 1 && flag3 == 1)
            {
                _currentDoctor.DoctorName = DoctorName.Text;
                _currentDoctor.DoctorSurname = DoctorSurname.Text;
                _currentDoctor.DoctorPatronymic = DoctorPatronymic.Text;

                CLINICSEntities.GetContext().Entry(_currentDoctor).State = System.Data.Entity.EntityState.Modified;
                try
                {
                    CLINICSEntities.GetContext().SaveChanges();
                    MessageBox.Show("Изменения внесены");
                    DoctorName.Background = Brushes.White;
                    DoctorSurname.Background = Brushes.White;
                    DoctorPatronymic.Background = Brushes.White;
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

        private void DoctorDataGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DOCTOR selectedDoctor = new DOCTOR();
            selectedDoctor = DoctorDataGrid.SelectedItem as DOCTOR;
            if (selectedDoctor != null)
            {
                string link = selectedDoctor.DoctorImage;
                BitmapImage myBitmapImage = new BitmapImage(new Uri(link));
                myBitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                DocImage.Source = myBitmapImage;
            }
            else
            {
                MessageBox.Show("Не выбрана запись, чтобы отобразить фото");
            }
        }

        private void LoadImage_Click(object sender, RoutedEventArgs e)
        {
            DOCTOR selectedDoctor = new DOCTOR();
            selectedDoctor = DoctorDataGrid.SelectedItem as DOCTOR;
            try
            {
                DOCTOR currentDoctor = CLINICSEntities.GetContext().DOCTORs.Where(m => m.DoctorID == selectedDoctor.DoctorID).FirstOrDefault();
               
                Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
                if (openFileDlg.ShowDialog() == true)
                    link.Text = openFileDlg.FileName;
                DocImage.Source = new BitmapImage(new Uri(link.Text));
                currentDoctor.DoctorImage = link.Text.Trim();
                if (currentDoctor != null)
                {
                    try
                    {
                        CLINICSEntities.GetContext().SaveChanges();
                        DoctorDataGrid.ItemsSource = CLINICSEntities.GetContext().DOCTORs.ToList();
                    }
                    catch
                    {
                        MessageBox.Show("Необходимо выбрать запись для загрузки фото!");
                    }
                }
            }
            catch { }
        }

        private void surname_TextChanged(object sender, TextChangedEventArgs e)
        {
                try
                {
                DoctorDataGrid.ItemsSource = CLINICSEntities.GetContext().DOCTORs.Where(c => c.DoctorSurname.ToString().Trim() == surname.Text.Trim()).ToList();
                    if (id.Text.Trim().Length == 0)
                    {
                    DoctorDataGrid.ItemsSource = CLINICSEntities.GetContext().DOCTORs.ToList();
                    }
                }
                catch { }
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DoctorDataGrid.ItemsSource = CLINICSEntities.GetContext().DOCTORs.ToList();
                for (int i = 0; i < DoctorDataGrid.Items.Count; i++)
                {
                    string param = id.Text;
                    DoctorDataGrid.ScrollIntoView(DoctorDataGrid.Items[i]);
                    DataGridRow row = (DataGridRow)DoctorDataGrid.ItemContainerGenerator.ContainerFromIndex(i);
                    TextBlock cellContentID = DoctorDataGrid.Columns[0].GetCellContent(row) as TextBlock;
                    if (cellContentID != null && cellContentID.Text.ToLower().Trim().Equals(param.ToLower()))
                    {
                        object item = DoctorDataGrid.Items[i];
                        DoctorDataGrid.SelectedItem = item;
                        DoctorDataGrid.ScrollIntoView(item);
                        row.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                        DOCTOR selectedDoctor = DoctorDataGrid.Items[i] as DOCTOR;
                        if (selectedDoctor != null)
                        {
                            DoctorName.Text = selectedDoctor.DoctorName.ToString().Trim();
                            DoctorSurname.Text = selectedDoctor.DoctorSurname.ToString().Trim();
                            DoctorPatronymic.Text = selectedDoctor.DoctorPatronymic.ToString().Trim();
                            id.Clear();
                            break;
                        }

                    }
                    else
                    {

                        DoctorName.Text = null;
                        DoctorSurname.Text = null;
                        DoctorPatronymic.Text = null;
                        DoctorDataGrid.SelectedItem = null;
                    }
                }
            }
            catch { }

        }
    }
}
