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

namespace CLINICS.pages
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Page
    {
        private CLIENT _currentClient = new CLIENT();
        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
            MainWindow.MenuStackPanel.Visibility = Visibility.Hidden;
        }
        public Profile()
        {
            InitializeComponent();
            ChangedName.Text = App.currentClient.ClientName;
            ChangedSurname.Text = App.currentClient.ClientSurname;
            TelInputChanged.Text = App.currentClient.TelephoneNumber;
            Password.Text = App.currentClient.Password;
            DataContext = _currentClient;
        }

        private void RenewPersonalInfoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name_u = ChangedName.Text.Trim();
                string sername_u = ChangedName.Text.Trim();
                string pass_u = Password.Text.Trim();

                string tel_u = TelInputChanged.Text.Trim();
                Regex r1 = new Regex(@"^\+375 \((25|29|33|44)\) [0-9]{3}-[0-9]{2}-[0-9]{2}$");
                Match m = r1.Match(tel_u);

                Regex r = new Regex(@"^[А-Я][а-я]+$");
                Match m1 = r.Match(name_u);
                Match m2 = r.Match(sername_u);

                Regex r2 = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])[A-Za-z0-9]{6,8}$");
                Match m3 = r2.Match(pass_u);


                int flag = 1;
                StringBuilder regexDataErrors = new StringBuilder();
                if (m1.Success == false)
                {
                    flag = 0;
                    ChangedName.ToolTip = "Имя пользователя должно быть на русском и начинаться с заглавной буквы";
                    ChangedName.Background = Brushes.LightGray;
                }
                else
                {
                    ChangedName.Background = Brushes.Transparent;
                }
                if (m2.Success == false)
                {
                    //regexDataErrors.AppendLine("Введите корректную фамилию (фамилия пользователя должна содержать кириллицу и начинаться с заглавной буквы)");
                    flag = 0;
                    ChangedSurname.ToolTip = " Фамилия пользователя должна быть на русском и начинаться с заглавной буквы";
                    ChangedSurname.Background = Brushes.LightGray;
                }

                else
                {
                    ChangedSurname.Background = Brushes.Transparent;
                }

                if (m.Success == false)
                {
                    flag = 0;
                    TelInputChanged.ToolTip = " Телефон должен быть записан в виде '+375 (44|29|33|25) ***-**-**, учитывая скобки, дефисы и пробелы'";
                    TelInputChanged.Background = Brushes.LightGray;
                }
                else
                {
                    TelInputChanged.Background = Brushes.Transparent;
                }
                if (m3.Success == false)
                {
                    flag = 0;
                    //regexDataErrors.AppendLine("Введите корректный пароль (пароль должен содержать 8-15 символов, минимум одно число и латинскую букву)");
                    Password.ToolTip = "Пароль должен содержать 8-15 символов, минимум одно число и латинскую букву";
                    Password.Background = Brushes.LightGray;
                }

                else
                {
                    Password.Background = Brushes.Transparent;
                }


                if (regexDataErrors.Length > 0)
                {
                    MessageBox.Show(regexDataErrors.ToString());
                }
                if (flag == 1)
                {
                    try
                    {
                        App.currentClient.ClientName = ChangedName.Text;
                        App.currentClient.ClientSurname = ChangedSurname.Text;
                        App.currentClient.TelephoneNumber = TelInputChanged.Text;
                        App.currentClient.Password = Password.Text;
                        
                        CLINICSEntities.GetContext().Entry(App.currentClient).State = System.Data.Entity.EntityState.Modified;

                        CLINICSEntities.GetContext().SaveChanges();
                        MessageBox.Show("Изменения внесены");
                    }
                    catch
                    {
                        MessageBox.Show("Информация не измененна");
                    }

                }
                else
                {
                    MessageBox.Show("Введите корректные данные");
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                {

                    foreach (DbValidationError err in validationError.ValidationErrors)
                    {
                        MessageBox.Show(err.ErrorMessage + " ");

                    }
                }
            }
        }
    }
}
