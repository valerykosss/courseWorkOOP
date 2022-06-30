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
using CLINICS.windows;

namespace CLINICS.pages
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public static CLIENT _currentClient = new CLIENT();
        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window w in App.Current.Windows)
                w.Close();
        }
        void letters(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0))
            {
                e.Handled = true; //не обрабатывать введеный символ
            }
        }
        public RegistrationPage()
        {
            InitializeComponent();
            DataContext = _currentClient;
            Name.PreviewTextInput += new TextCompositionEventHandler(letters);
            Surname.PreviewTextInput += new TextCompositionEventHandler(letters);
            Patronymic.PreviewTextInput += new TextCompositionEventHandler(letters);
            App.currentClient = null;
        }

        private void MoveToLogIn(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }

        private void TelInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int flag = 1;
                StringBuilder emptyDataErrors = new StringBuilder();

                if (string.IsNullOrWhiteSpace(Name.Text))
                {
                    flag = 0;
                    Name.ToolTip = "Введите имя";
                    Name.BorderBrush = Brushes.Red;
                    Name.BorderThickness = new Thickness(1);

                }
                else
                {
                    Name.BorderThickness = new Thickness(0, 0, 0, 1);
                    Name.BorderBrush = Brushes.Black;
                }
                if (string.IsNullOrWhiteSpace(Surname.Text))
                {
                    flag = 0;
                    Surname.ToolTip = "Введите фамилию";
                    Surname.BorderBrush = Brushes.Red;
                    Surname.BorderThickness = new Thickness(1);

                }
                else
                {
                    Surname.BorderThickness = new Thickness(0, 0, 0, 1);
                    Surname.BorderBrush = Brushes.Black;
                }
                if (string.IsNullOrWhiteSpace(Patronymic.Text))
                {
                    flag = 0;
                    Patronymic.ToolTip = "Введите отчество";
                    Patronymic.BorderBrush = Brushes.Red;
                    Patronymic.BorderThickness = new Thickness(1);
                }
                else
                {
                    Patronymic.BorderThickness = new Thickness(0, 0, 0, 1);
                    Patronymic.BorderBrush = Brushes.Black;
                }
                if (string.IsNullOrWhiteSpace(TelInput.Text))
                {

                    flag = 0;
                    TelInput.ToolTip = "Введите телефон";
                    TelInput.BorderBrush = Brushes.Red;
                    TelInput.BorderThickness = new Thickness(1);
                }
                else
                {
                    TelInput.BorderThickness = new Thickness(0, 0, 0, 1);
                    TelInput.BorderBrush = Brushes.Black;
                }
                if (string.IsNullOrWhiteSpace(Password.Password))
                {
                    flag = 0;
                    Password.ToolTip = "Введите пароль";
                    Password.BorderBrush = Brushes.Red;
                    Password.BorderThickness = new Thickness(1);
                }
                else
                {
                    Password.BorderThickness = new Thickness(0, 0, 0, 1);
                    Password.BorderBrush = Brushes.Black;
                }
                if (string.IsNullOrWhiteSpace(RepeatPassword.Password))
                {
                    flag = 0;
                    RepeatPassword.ToolTip = "Введите пароль";
                    RepeatPassword.BorderBrush = Brushes.Red;
                    RepeatPassword.BorderThickness = new Thickness(1);
                }
                else
                {
                    RepeatPassword.BorderThickness = new Thickness(0, 0, 0, 1);
                    RepeatPassword.BorderBrush = Brushes.Black;
                }

                string name_u = Name.Text.Trim();
                string surname_u = Surname.Text.Trim();
                string patronymic_u = Patronymic.Text.Trim();
                string pass_u = Password.Password.Trim();

                string tel_u = TelInput.Text.Trim();
                Regex r1 = new Regex(@"^\+375 \((25|29|33|44)\) [0-9]{3}-[0-9]{2}-[0-9]{2}$");
                Match m = r1.Match(tel_u);

                Regex r = new Regex(@"^[А-Я][а-я]+$");
                Match m1 = r.Match(name_u);
                Match m2 = r.Match(surname_u);
                Match m4= r.Match(patronymic_u);

                Regex r2 = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])[A-Za-z0-9]{6,8}$");
                Match m3 = r2.Match(pass_u);

                if (m1.Success == false)
                {
                    flag = 0;

                    Name.ToolTip = "Имя пользователя должно быть на русском и начинаться с заглавной буквы";
                    Name.BorderBrush = Brushes.Red;
                    Name.BorderThickness = new Thickness(1);
                }
                else
                {
                    Name.BorderThickness = new Thickness(0, 0, 0, 1);
                    Name.BorderBrush = Brushes.Black;
                }
                if (m2.Success == false)
                {
                    flag = 0;
                    Surname.ToolTip = "Фамилия пользователя должна быть на русском и начинаться с заглавной буквы";
                    Surname.BorderBrush = Brushes.Red;
                    Surname.BorderThickness = new Thickness(1);
                }
                else
                {
                    Surname.BorderThickness = new Thickness(0, 0, 0, 1);
                    Surname.BorderBrush = Brushes.Black;
                }
                if (m4.Success == false)
                {
                    flag = 0;
                    Patronymic.ToolTip = "Отчество пользователя должно быть на русском и начинаться с заглавной буквы";
                    Patronymic.BorderBrush = Brushes.Red;
                    Patronymic.BorderThickness = new Thickness(1);
                }
                else
                {
                    Patronymic.BorderThickness = new Thickness(0, 0, 0, 1);
                    Patronymic.BorderBrush = Brushes.Black;
                }

                var repeat_tel = CLINICSEntities.GetContext().CLIENTs.Where(p => p.TelephoneNumber == tel_u).FirstOrDefault();
                if (repeat_tel != null)
                {
                    flag = 0;
                    TelInput.ToolTip = "Пользователь с таким номером уже существует";
                    TelInput.BorderBrush = Brushes.Red;
                    TelInput.BorderThickness = new Thickness(1);
                }
                if (m.Success == false)
                {
                    flag = 0;
                    TelInput.ToolTip = " Телефон должен быть записан в виде '+375 (44|29|33|25) ***-**-** с учеом пробелов, тире и скобок'";
                    TelInput.BorderBrush = Brushes.Red;
                    TelInput.BorderThickness = new Thickness(1);
                }
                else
                {
                    TelInput.BorderThickness = new Thickness(0, 0, 0, 1);
                    TelInput.BorderBrush = Brushes.Black;
                }
                if (m3.Success == false)
                {
                    flag = 0;
                    Password.ToolTip = "Пароль должен содержать 6-8 символов, минимум одно число и заглавную английскую букву";
                    Password.BorderBrush = Brushes.Red;
                    Password.BorderThickness = new Thickness(1);
                }

                else
                {
                    Password.BorderThickness = new Thickness(0, 0, 0, 1);
                    Password.BorderBrush = Brushes.Black;
                }

                if (Password.Password != RepeatPassword.Password)
                {
                    flag = 0;
                    RepeatPassword.ToolTip = "Пароли не совпадают, проверьте вводимые данные";
                    RepeatPassword.BorderBrush = Brushes.Red;
                    RepeatPassword.BorderThickness = new Thickness(1);
                }
                else
                {

                    RepeatPassword.ToolTip = "Пароли совпадают";
                    RepeatPassword.BorderThickness = new Thickness(0, 0, 0, 1);
                    RepeatPassword.BorderBrush = Brushes.Black;

                }

                if (flag == 1)
                {
                    if (_currentClient.ClientID == 0)
                    {
                        _currentClient.ClientName = Name.Text;
                        _currentClient.ClientSurname = Surname.Text;
                        _currentClient.ClientPatronymic = Patronymic.Text;
                        _currentClient.Password = Password.Password;
                        _currentClient.UserType = 0;
                        _currentClient.TelephoneNumber = TelInput.Text;

                        CLINICSEntities.GetContext().CLIENTs.Add(_currentClient);
                        try
                        {
                            CLINICSEntities.GetContext().SaveChanges();
                            App.currentClient = _currentClient;
                            AboutUs AboutUs = new AboutUs();
                            MainWindow.Frame.Navigate(AboutUs);
                            MainWindow.MenuStackPanel.Visibility = Visibility.Visible;


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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Name_GotFocus(object sender, RoutedEventArgs e)
        {
            Name.BorderThickness = new Thickness(0, 0, 0, 1);
        }

        private void Surname_GotFocus(object sender, RoutedEventArgs e)
        {
            Surname.BorderThickness = new Thickness(0, 0, 0, 1);
        }

        private void Patronymic_GotFocus(object sender, RoutedEventArgs e)
        {
            Patronymic.BorderThickness = new Thickness(0, 0, 0, 1);
        }

        private void TelInput_GotFocus(object sender, RoutedEventArgs e)
        {
            TelInput.BorderThickness = new Thickness(0, 0, 0, 1);
        }

        private void Password_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            Password.BorderThickness = new Thickness(0, 0, 0, 1);
        }

        private void RepeatPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            RepeatPassword.BorderThickness = new Thickness(0, 0, 0, 1);
        }
    }
}