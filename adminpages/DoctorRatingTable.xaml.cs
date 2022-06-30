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
    /// Interaction logic for DoctorRatingTable.xaml
    /// </summary>
    public partial class DoctorRatingTable : Page
    {
        private int currentDoctorId = 0;
        private int currenClientId = 0;

        private DOCTOR_RATING _currentDoctorRating = new DOCTOR_RATING();


        public DoctorRatingTable()
        {
            InitializeComponent();
            Load();
            DataContext = _currentDoctorRating;
            var doctors = CLINICSEntities.GetContext().DOCTORs.ToList();
            var clients = CLINICSEntities.GetContext().CLIENTs.ToList();
            ClientIDCombobox.ItemsSource = clients;
        }

        private List<CLIENT> getClientsByDoctorID(int curDocId)
        {
            using (CLINICSEntities db = new CLINICSEntities())
            {
                var clientsByDoc = (from client in db.CLIENTs
                                    join regs in db.REGISTRATIONs on client.ClientID equals regs.ClientID
                                    join docServ in db.DOCTOR_SERVICE on regs.DoctorServiceID equals docServ.DoctorServiceID
                                    join docs in db.DOCTORs on docServ.DoctorID equals docs.DoctorID
                                    where client.ClientID == curDocId
                                    select new
                                    {
                                        ClientID = client.ClientID,
                                        ClientName = client.ClientName,
                                        ClientSurname = client.ClientSurname,
                                        Password = client.Password,
                                        UserType = client.UserType,
                                        TelephoneNumber = client.TelephoneNumber,

                                    }).ToList();
                var result = clientsByDoc.Select(clents => new CLIENT()
                {
                    ClientID = clents.ClientID,
                    ClientName = clents.ClientName,
                    ClientSurname = clents.ClientSurname,
                    Password = clents.Password,
                    UserType = clents.UserType,
                    TelephoneNumber = clents.TelephoneNumber
                }).ToList();
                var noDuplicResult = result.GroupBy(x => x.ClientID).Select(y => y.FirstOrDefault()).ToList();
                return noDuplicResult;

                //using (CLINICSEntities db = new CLINICSEntities())
                //{
                //    var clientsByDoctor = (from client in db.CLIENTs
                //                           join regs in db.REGISTRATIONs on client.ClientID equals regs.ClientID
                //                           join docServ in db.DOCTOR_SERVICE on regs.DoctorServiceID equals docServ.DoctorServiceID
                //                           join docs in db.DOCTORs on docServ.DoctorID equals docs.DoctorID
                //                           where docs.DoctorID == curDocId
                //                           select new
                //                           {
                //                               client_id = client.ClientID,
                //                               client_name = client.ClientName,
                //                               client_surname = client.ClientSurname,
                //                               client_password = client.Password,
                //                               client_userType = client.UserType,
                //                               client_tel = client.TelephoneNumber

                //                           });

                //    List<object> listInBetween = clientsByDoctor.ToList<object>();
                //    List<CLIENT> result = listInBetween.OfType<CLIENT>().ToList();
                //    //List<CLIENT> result = listInBetween.Cast<CLIENT>().ToList();
                //    return result;
                //}


                //using (CLINICSEntities db = new CLINICSEntities())
                //{
                //    var clientsByDoctor = from client in db.CLIENTs
                //                       join regs in db.REGISTRATIONs on client.ClientID equals regs.ClientID
                //                       join docServ in db.DOCTOR_SERVICE on regs.DoctorServiceID equals docServ.DoctorServiceID
                //                       join docs in db.DOCTORs on docServ.DoctorID equals docs.DoctorID
                //                       where client.ClientID == curDocId
                //                          select client;
                //    var result = clientsByDoctor.ToList();
                //    return result;
                //}

            }
        }
        private void DoctorIDCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentDoctorId = (int)(sender as ComboBox).SelectedValue;
            //ClientIDCombobox.ItemsSource=getClientsByDoctorID(currentDoctorId);
        }


        private List<DOCTOR> getDoctorsByClientID(int curClientId)
        {
            using (CLINICSEntities db = new CLINICSEntities())
            {
                var docsByClient = (from client in db.CLIENTs
                                    join regs in db.REGISTRATIONs on client.ClientID equals regs.ClientID
                                    join docServ in db.DOCTOR_SERVICE on regs.DoctorServiceID equals docServ.DoctorServiceID
                                    join docs in db.DOCTORs on docServ.DoctorID equals docs.DoctorID
                                    where client.ClientID == curClientId
                                    select new
                                    {
                                        DoctorID = docs.DoctorID,
                                        DoctorName = docs.DoctorName,
                                        DoctorSurname = docs.DoctorSurname,
                                        DoctorPatronymic = docs.DoctorPatronymic,
                                        DoctorImage = docs.DoctorImage,

                                    }).ToList();
                var result = docsByClient.Select(docs => new DOCTOR()
                {
                    DoctorID = docs.DoctorID,
                    DoctorName = docs.DoctorName,
                    DoctorSurname = docs.DoctorSurname,
                    DoctorPatronymic = docs.DoctorPatronymic,
                    DoctorImage = docs.DoctorImage,
                }).ToList();

                var noDuplicResult = result.GroupBy(x => x.DoctorID).Select(y => y.FirstOrDefault()).ToList();
                return noDuplicResult;
            }

            //using (CLINICSEntities db = new CLINICSEntities())
            //{
            //    var docsByClient = from client in db.CLIENTs
            //                        join regs in db.REGISTRATIONs on client.ClientID equals regs.ClientID
            //                        join docServ in db.DOCTOR_SERVICE on regs.DoctorServiceID equals docServ.DoctorServiceID
            //                        join docs in db.DOCTORs on docServ.DoctorID equals docs.DoctorID
            //                        where client.ClientID == curClientId
            //                        select docs;
            //    var result = docsByClient.ToList();
            //    return result;
            //}

            //List<DOCTOR> doctors = Doctors.Where(cl => Registrations.All(reg => DoctorServices.All(docServs => Doctors.All(doc => doc.DoctorID == docServs.DoctorID)))).ToList();

            //using (CLINICSEntities db = new CLINICSEntities())
            //{
            //    var docsByClient = (from client in db.CLIENTs
            //                        join regs in db.REGISTRATIONs on client.ClientID equals regs.ClientID
            //                        join docServ in db.DOCTOR_SERVICE on regs.DoctorServiceID equals docServ.DoctorServiceID
            //                        join docs in db.DOCTORs on docServ.DoctorID equals docs.DoctorID
            //                        where client.ClientID == curClientId
            //                        select new
            //                        {
            //                            docs_id = docs.DoctorID,
            //                            docs_name = docs.DoctorName,
            //                            docs_surname = docs.DoctorSurname,
            //                            docs_patronymic = docs.DoctorPatronymic,
            //                            docs_image = docs.DoctorImage,

            //                        });
            //    List<DOCTOR> result = docsByClient.ToList<object>().OfType<DOCTOR>().ToList();
            //    //List<DOCTOR> result = docsByClient.ToList<object>().Cast<DOCTOR>().ToList();
            //    return result;
            //}

            //using (CLINICSEntities db = new CLINICSEntities())
            //{
            //    var docsByClient = (from client in db.CLIENTs
            //                        join regs in db.REGISTRATIONs on client.ClientID equals regs.ClientID
            //                        join docServ in db.DOCTOR_SERVICE on regs.DoctorServiceID equals docServ.DoctorServiceID
            //                        join docs in db.DOCTORs on docServ.DoctorID equals docs.DoctorID
            //                        where client.ClientID == curClientId
            //                        select new DOCTOR
            //                        {
            //                            DoctorID = docs.DoctorID,
            //                            DoctorName = docs.DoctorName,
            //                            DoctorSurname = docs.DoctorSurname,
            //                            DoctorPatronymic = docs.DoctorPatronymic,
            //                            DoctorImage = docs.DoctorImage,

            //                        });
            //    var result = docsByClient.ToList<DOCTOR>();
            //    return result;
            //}
        }

        private void ClientIDCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currenClientId = (int)(sender as ComboBox).SelectedValue;
            DoctorIDCombobox.ItemsSource = getDoctorsByClientID(currenClientId);
        }

        private void Load()
        {
            DoctorRatingDataGrid.ItemsSource = CLINICSEntities.GetContext().DOCTOR_RATING.ToList();
        }
        private void ClearTextBox()
        {
            Comment.Clear();
        }
        private void add_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder emptyDataErrors = new StringBuilder();

            if (DoctorIDCombobox.SelectedItem == null)
            {
                emptyDataErrors.AppendLine("Вы не выбрали фамилию врача");
            }
            if (ClientIDCombobox.SelectedItem == null)
            {
                emptyDataErrors.AppendLine("Вы не выбрали телефон клиента");
            }
            if (RatingListBox.SelectedItem == null)
            {
                emptyDataErrors.AppendLine("Вы не поставили оценку");
            }
            if (string.IsNullOrWhiteSpace(Comment.Text))
            {
                emptyDataErrors.AppendLine("Введите комментарий");
            }
            if (emptyDataErrors.Length > 0)
            {
                MessageBox.Show(emptyDataErrors.ToString());
                return;
            }

            DOCTOR_RATING _currentDoctorRating = new DOCTOR_RATING();

            CLINICSEntities.GetContext().DOCTOR_RATING.Add(_currentDoctorRating);
            try
            {
                DOCTOR selectedDoctor = (DOCTOR)DoctorIDCombobox.SelectedItem;
                CLIENT selectedClient = (CLIENT)ClientIDCombobox.SelectedItem;
                _currentDoctorRating.Comment = Comment.Text;
                int listboxValue = Convert.ToInt32(String.Join(" ", RatingListBox.SelectedItems.Cast<System.Windows.Controls.ListBoxItem>().Select(x => x.Content)));
                _currentDoctorRating.Rating = listboxValue;

                _currentDoctorRating.DoctorID = selectedDoctor.DoctorID;
                _currentDoctorRating.ClientID = selectedClient.ClientID;
            }
            catch
            {
                MessageBox.Show("Errors");
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
                    MessageBox.Show(" ");

                    foreach (DbValidationError err in validationError.ValidationErrors)
                    {
                        MessageBox.Show(err.ErrorMessage + " ");

                    }
                }
            }
        }
        private void delete_Click(object sender, RoutedEventArgs e)
        {
            var elementsToRemove = DoctorRatingDataGrid.SelectedItems.Cast<DOCTOR_RATING>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить {elementsToRemove.Count()} элемент(ов)?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                try
                {
                    CLINICSEntities.GetContext().DOCTOR_RATING.RemoveRange(elementsToRemove);
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
            DOCTOR_RATING sel = DoctorRatingDataGrid.SelectedItem as DOCTOR_RATING;

            _currentDoctorRating = sel;

            DoctorIDCombobox.SelectedValue = _currentDoctorRating.DoctorID;
            ClientIDCombobox.SelectedValue = _currentDoctorRating.ClientID;
            RatingListBox.SelectedValuePath = _currentDoctorRating.Rating.ToString();
            Comment.Text = _currentDoctorRating.Comment;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                _currentDoctorRating.Comment = Comment.Text;
                int listboxValue = Convert.ToInt32(String.Join(" ", RatingListBox.SelectedItems.Cast<System.Windows.Controls.ListBoxItem>().Select(x => x.Content)));
                _currentDoctorRating.Rating = listboxValue;
                DOCTOR selectedDoctor = (DOCTOR)DoctorIDCombobox.SelectedItem;
                CLIENT selectedClient = (CLIENT)ClientIDCombobox.SelectedItem;
                _currentDoctorRating.DoctorID = selectedDoctor.DoctorID;
                _currentDoctorRating.ClientID = selectedClient.ClientID;

                CLINICSEntities.GetContext().Entry(_currentDoctorRating).State = System.Data.Entity.EntityState.Modified;

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
