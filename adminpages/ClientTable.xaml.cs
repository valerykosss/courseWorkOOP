using CLINICS.models;
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

namespace CLINICS.adminpages
{
    /// <summary>
    /// Interaction logic for ClientTable.xaml
    /// </summary>
    public partial class ClientTable : Page
    {

        private CLIENT _currentClient = new CLIENT();
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
        public ClientTable()
        {
            InitializeComponent();
            Load();
            DataContext = _currentClient;
            ClientName.PreviewTextInput += new TextCompositionEventHandler(letters);
            ClientSurname.PreviewTextInput += new TextCompositionEventHandler(letters);
            ClientPatronymic.PreviewTextInput += new TextCompositionEventHandler(letters);
            Password.PreviewTextInput += new TextCompositionEventHandler(lettersAndNumbers);
        }
        private void Load()
        {
            ClientDataGrid.ItemsSource = CLINICSEntities.GetContext().CLIENTs.ToList();
        }
        private void ClearTextBox()
        {
            ClientName.Clear();
            ClientSurname.Clear();
            ClientPatronymic.Clear();
            TelephoneNumber.Clear();
            Password.Clear();

        }
        private void add_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder emptyDataErrors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(ClientName.Text))
            {
                emptyDataErrors.AppendLine("Введите имя пользователя");
            }
            if (string.IsNullOrWhiteSpace(ClientSurname.Text))
            {
                emptyDataErrors.AppendLine("Введите фамилию пользователя");
            }
            if (string.IsNullOrWhiteSpace(ClientPatronymic.Text))
            {
                emptyDataErrors.AppendLine("Введите отчество пользователя");
            }
            if (string.IsNullOrWhiteSpace(TelephoneNumber.Text))
            {
                emptyDataErrors.AppendLine("Введите номер телефона пользователя");
            }
            if (string.IsNullOrWhiteSpace(Password.Text))
            {
                emptyDataErrors.AppendLine("Введите пароль пользователя");
            }
            if (UserTypeListBox.SelectedItem == null)
            {
                emptyDataErrors.AppendLine("Выберите тип аккаунта");
            }
            if (emptyDataErrors.Length > 0)
            {
                MessageBox.Show(emptyDataErrors.ToString());
                return;
            }
            string clientName = ClientName.Text.Trim();
            string clientSurname = ClientSurname.Text.Trim();
            string clientPatronymic = ClientPatronymic.Text.Trim();
            string telNumber = TelephoneNumber.Text.Trim();

            Regex r = new Regex(@"^[А-Я][а-я]+$");
            Match m1 = r.Match(clientName);

            int flag1 = 1;
            StringBuilder regexDataErrors = new StringBuilder();
            if (!m1.Success)
            {
                flag1 = 0;
                regexDataErrors.AppendLine("Введите корректое имя");
                ClientName.Background = Brushes.Gray;
            }
            else
            {
                ClientName.Background = Brushes.White;
            }

            int flag2 = 1;
            Match m2 = r.Match(clientSurname);
            if (!m2.Success)
            {
                flag2 = 0;
                regexDataErrors.AppendLine("Введите корректную фамилию");
                ClientSurname.Background = Brushes.Gray;
            }
            else
            {
                ClientSurname.Background = Brushes.White;
            }

            int flag22 = 1;
            Match m22 = r.Match(clientPatronymic);
            if (!m2.Success)
            {
                flag22 = 0;
                regexDataErrors.AppendLine("Введите корректное отчество");
                ClientPatronymic.Background = Brushes.Gray;
            }
            else
            {
                ClientPatronymic.Background = Brushes.White;
            }

            int flag3 = 1;
            if (telNumber.Length < 19 || telNumber.Length > 19)
            {
                flag3 = 0;
                regexDataErrors.AppendLine("Для номера необходимо ввести 19 символов");
                TelephoneNumber.Background = Brushes.Gray;
            }
            else
            {
                Regex r1 = new Regex(@"^\+375 \((25|29|33|44)\) [0-9]{3}-[0-9]{2}-[0-9]{2}$");
                Match mt = r1.Match(telNumber);
                CLIENT client = CLINICSEntities.GetContext().CLIENTs.FirstOrDefault(p => p.TelephoneNumber == telNumber);
                if (!mt.Success)
                {
                    flag3 = 0;
                    regexDataErrors.AppendLine("Введите телефон в виде +375 (33|25|44|29) 000-00-00");
                    TelephoneNumber.Background = Brushes.Gray;
                }
                if (mt.Success && client != null)
                {
                    flag3 = 0;
                    regexDataErrors.AppendLine("Такой номер телефона уже существует в бд");
                    TelephoneNumber.Background = Brushes.Gray;
                }
                if (mt.Success && client == null)
                {
                    TelephoneNumber.Background = Brushes.White;
                }

            }

            int flag4 = 1;
            string password = Password.Text.Trim();
            Regex r2 = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])[A-Za-z0-9]{6,8}$");
            Match mt2 = r2.Match(password);
            if (!mt2.Success)
            {
                flag4 = 0;
                regexDataErrors.AppendLine("Введите корректный пароль от 6 до 8 символов, " +
                    "включающие большие и маленькие латинские буквы, цифры)");
                Password.Background = Brushes.Gray;
            }
            else
            {
                Password.Background = Brushes.White;
            }


            if (regexDataErrors.Length > 0)
            {
                MessageBox.Show(regexDataErrors.ToString());
                return;
            }
            if (flag1 == 1 && flag2 == 1 && flag3 == 1 && flag4 == 1 && flag22 == 1)
            {
                CLIENT _currentClient = new CLIENT();
                _currentClient.ClientName = ClientName.Text;
                _currentClient.ClientSurname = ClientSurname.Text;
                _currentClient.Password = Password.Text;
                _currentClient.TelephoneNumber = TelephoneNumber.Text;
                _currentClient.ClientPatronymic = ClientPatronymic.Text;
                int listboxValue = Convert.ToInt32(String.Join(" ", UserTypeListBox.SelectedItems.Cast<System.Windows.Controls.ListBoxItem>().Select(x => x.Content)));
                _currentClient.UserType = listboxValue;

                CLINICSEntities.GetContext().CLIENTs.Add(_currentClient);
                try
                {
                    CLINICSEntities.GetContext().SaveChanges();
                    MessageBox.Show("Успешно!");
                    ClientName.Background = Brushes.White;
                    ClientPatronymic.Background = Brushes.White;
                    ClientSurname.Background = Brushes.White;
                    TelephoneNumber.Background = Brushes.White;
                    Password.Background = Brushes.White;
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
            var elementsToRemove = ClientDataGrid.SelectedItems.Cast<CLIENT>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить {elementsToRemove.Count()} элемент(ов)?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                try
                {
                    CLINICSEntities.GetContext().CLIENTs.RemoveRange(elementsToRemove);
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
            ClientName.Background = Brushes.White;
            ClientSurname.Background = Brushes.White;
            TelephoneNumber.Background = Brushes.White;
            Password.Background = Brushes.White;
            ClientPatronymic.Background = Brushes.White;

            CLIENT sel = ClientDataGrid.SelectedItem as CLIENT;

            _currentClient = sel;

            ClientName.Text = _currentClient.ClientName;
            ClientSurname.Text = _currentClient.ClientSurname;
            Password.Text = _currentClient.Password;
            TelephoneNumber.Text = _currentClient.TelephoneNumber;
            ClientPatronymic.Text = _currentClient.ClientPatronymic;
            UserTypeListBox.SelectedValuePath = _currentClient.UserType.ToString();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder emptyDataErrors = new StringBuilder();

                if (string.IsNullOrWhiteSpace(ClientName.Text))
                {
                    emptyDataErrors.AppendLine("Введите имя пользователя");
                }
                if (string.IsNullOrWhiteSpace(ClientSurname.Text))
                {
                    emptyDataErrors.AppendLine("Введите фамилию пользователя");
                }
                if (string.IsNullOrWhiteSpace(ClientPatronymic.Text))
                {
                    emptyDataErrors.AppendLine("Введите отчество пользователя");
                }
                if (string.IsNullOrWhiteSpace(TelephoneNumber.Text))
                {
                    emptyDataErrors.AppendLine("Введите номер телефона пользователя");
                }
                if (string.IsNullOrWhiteSpace(Password.Text))
                {
                    emptyDataErrors.AppendLine("Введите пароль пользователя");
                }
                if (UserTypeListBox.SelectedItem == null)
                {
                    emptyDataErrors.AppendLine("Выберите тип аккаунта");
                }
                if (emptyDataErrors.Length > 0)
                {
                    MessageBox.Show(emptyDataErrors.ToString());
                    return;
                }
                string clientName = ClientName.Text.Trim();
                string clientPatronymic = ClientPatronymic.Text.Trim();
                string clientSurname = ClientSurname.Text.Trim();
                string telNumber = TelephoneNumber.Text.Trim();

                Regex r = new Regex(@"^[А-Я][а-я]+$");
                Match m1 = r.Match(clientName);

                int flag1 = 1;
                StringBuilder regexDataErrors = new StringBuilder();
                if (!m1.Success)
                {
                    flag1 = 0;
                    regexDataErrors.AppendLine("Введите корректое имя");
                    ClientName.Background = Brushes.Red;

                }
                else
                {
                    ClientName.Background = Brushes.White;
                }

                int flag2 = 1;
                Match m2 = r.Match(clientSurname);
                if (!m2.Success)
                {
                    flag2 = 0;
                    regexDataErrors.AppendLine("Введите корректную фамилию");
                    ClientSurname.Background = Brushes.Gray;
                }
                else
                {
                    ClientSurname.Background = Brushes.White;
                }

                int flag22 = 1;
                Match m22 = r.Match(clientPatronymic);
                if (!m22.Success)
                {
                    flag22 = 0;
                    regexDataErrors.AppendLine("Введите корректное отчество");
                    ClientPatronymic.Background = Brushes.Gray;
                }
                else
                {
                    ClientPatronymic.Background = Brushes.White;
                }

                int flag3 = 1;
                if (telNumber.Length < 19 || telNumber.Length > 19)
                {
                    flag3 = 0;
                    regexDataErrors.AppendLine("Для номера необходимо ввести 19 символов");
                    TelephoneNumber.Background = Brushes.Gray;
                }
                else
                {
                    Regex r1 = new Regex(@"^\+375 \((25|29|33|44)\) [0-9]{3}-[0-9]{2}-[0-9]{2}$");
                    Match mt = r1.Match(telNumber);

                    CLIENT client = CLINICSEntities.GetContext().CLIENTs.FirstOrDefault(p => p.TelephoneNumber == telNumber);
                    if (!mt.Success)
                    {
                        flag3 = 0;
                        regexDataErrors.AppendLine("Введите телефон в виде +375 (33|25|44|29) 000-00-00");
                        TelephoneNumber.Background = Brushes.Gray;
                    }
                    //чтобы при изменении можно было ввести тот же номер, что и в ячейке до изменения, но нельзя написать номер из другой ячейки 
                    if (mt.Success && client != null && TelephoneNumber.Text != _currentClient.TelephoneNumber)
                    {

                        flag3 = 0;
                        regexDataErrors.AppendLine("Такой номер телефона уже существует в бд");
                        TelephoneNumber.Background = Brushes.Gray;
                    }

                    if (mt.Success && client == null)
                    {
                        TelephoneNumber.Background = Brushes.White;
                    }

                }

                int flag4 = 1;
                string password = Password.Text.Trim();
                Regex r2 = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])[A-Za-z0-9]{6,8}$");
                Match mt2 = r2.Match(password);
                if (!mt2.Success)
                {
                    flag4 = 0;
                    regexDataErrors.AppendLine("Введите корректный пароль от 6 до 8 символов, " +
                        "включающие большие и маленькие латинские буквы, цифры)");
                    Password.Background = Brushes.Gray;
                }
                else
                {
                    Password.Background = Brushes.White;
                }


                if (regexDataErrors.Length > 0)
                {
                    MessageBox.Show(regexDataErrors.ToString());
                    return;
                }
                if (flag1 == 1 && flag2 == 1 && flag3 == 1 && flag4 == 1  && flag22==1)
                {
                    _currentClient.ClientName = ClientName.Text;
                    _currentClient.ClientSurname = ClientSurname.Text;
                    _currentClient.Password = Password.Text;
                    _currentClient.TelephoneNumber = TelephoneNumber.Text;
                    _currentClient.ClientPatronymic = ClientPatronymic.Text;
                    int listboxValue = Convert.ToInt32(String.Join(" ", UserTypeListBox.SelectedItems.Cast<System.Windows.Controls.ListBoxItem>().Select(x => x.Content)));
                    _currentClient.UserType = listboxValue;

                    CLINICSEntities.GetContext().Entry(_currentClient).State = System.Data.Entity.EntityState.Modified;
                    try
                    {
                        CLINICSEntities.GetContext().SaveChanges();
                        MessageBox.Show("Изменения внесены");
                        ClientName.Background = Brushes.White;
                        ClientSurname.Background = Brushes.White;
                        TelephoneNumber.Background = Brushes.White;
                        Password.Background = Brushes.White;
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
