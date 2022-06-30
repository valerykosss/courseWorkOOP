using System;
using System.Collections.Generic;
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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window w in App.Current.Windows)
                w.Close();
        }
        public LoginPage()
        {
            InitializeComponent();
            App.currentClient = null;
        }
        private void MoveToRegistration(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                int flag = 1;
                StringBuilder emptyDataErrors = new StringBuilder();

                if (string.IsNullOrWhiteSpace(TelInput.Text))
                {

                    flag = 0;
                    TelInput.ToolTip = "Введите телефон";
                    TelInput.BorderBrush = Brushes.Red;
                    TelInput.BorderThickness = new Thickness(1);
                }
                else
                {
                    TelInput.BorderThickness = new Thickness(0,0,0,1);
                    TelInput.BorderBrush = Brushes.Black;
                }
                if (string.IsNullOrWhiteSpace(PasswordInput.Password))
                {
                    flag = 0;
                    PasswordInput.ToolTip = "Введите пароль";
                    PasswordInput.BorderBrush = Brushes.Red;
                    PasswordInput.BorderThickness = new Thickness(1);
                }
                else
                {
                    PasswordInput.BorderThickness = new Thickness(0, 0, 0, 1);
                    PasswordInput.BorderBrush = Brushes.Black;
                }

                string tel_u = TelInput.Text.Trim();
                Regex r1 = new Regex(@"^\+375 \((25|29|33|44)\) [0-9]{3}-[0-9]{2}-[0-9]{2}$");
                Match m = r1.Match(tel_u);

                string pass_u = PasswordInput.Password.Trim();
                Regex r2 = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])[A-Za-z0-9]{6,8}$");
                Match m3 = r2.Match(pass_u);

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
                    PasswordInput.ToolTip = "Пароль должен содержать 6-8 символов, минимум одно число и заглавную английскую букву";
                    PasswordInput.BorderBrush = Brushes.Red;
                    PasswordInput.BorderThickness = new Thickness(1);
                }

                else
                {

                    PasswordInput.BorderThickness = new Thickness(0, 0, 0, 1);
                    PasswordInput.BorderBrush = Brushes.Black;
                }


                AboutUs AboutUs = new AboutUs();
                CLIENT authuser = null;
                if (flag == 1)
                {
                    using (CLINICSEntities db = new CLINICSEntities())
                    {
                        authuser = db.CLIENTs.Where(b => b.TelephoneNumber == TelInput.Text && b.Password == PasswordInput.Password.ToString()).FirstOrDefault();
                        if (authuser != null)
                        {
                            if (authuser.UserType == 0)
                            {
                                App.currentClient = authuser;
                                MainWindow.clientSurname = App.currentClient.ClientSurname;
                                MainWindow.clientName = App.currentClient.ClientName;
                                MainWindow.clientPatronymic = App.currentClient.ClientPatronymic;
                                MainWindow.Frame.Navigate(AboutUs);
                                MainWindow.MenuStackPanel.Visibility = Visibility.Visible;
                            }
                            if (authuser.UserType == 1)
                            {
                                App.currentClient = authuser;
                                MainWindow.clientSurname = App.currentClient.ClientSurname;
                                MainWindow.clientName = App.currentClient.ClientName;
                                MainWindow.clientPatronymic = App.currentClient.ClientPatronymic;
                                MainWindow.Frame.Navigate(new DoctorHistory());
                                MainWindow.MenuStackPanel.Visibility = Visibility.Hidden;
                                //doctorWindow.Show();
                            }

                            if (authuser.UserType == 2)
                            {
                                App.currentClient = authuser;
                                MainWindow.clientSurname = App.currentClient.ClientSurname;
                                MainWindow.clientName = App.currentClient.ClientName;
                                MainWindow.clientPatronymic = App.currentClient.ClientPatronymic;
                                AdminWindow adminWindow = new AdminWindow();
                                adminWindow.Show();
                                App.Current.MainWindow.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Пользователь не найден, проверьте введенные данные или зарегистрируйтесь");
                        }
                    }
                }
            }
            //Application.Current.MainWindow.Close();
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void PasswordInput_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordInput.BorderThickness = new Thickness(0, 0, 0, 1);
            PasswordInput.BorderBrush = Brushes.Black;
        }

        private void TelInput_GotFocus(object sender, RoutedEventArgs e)
        {
            TelInput.BorderThickness = new Thickness(0, 0, 0, 1);
            TelInput.BorderBrush = Brushes.Black;
        }
    }
}