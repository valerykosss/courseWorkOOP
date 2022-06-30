using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for DoctorServiceTable.xaml
    /// </summary>
    public class Wrapper
    {
        public Wrapper(int docServeId, int docId, int serveId, string doctorSurname, string serviceName)
        {
            DocServeId = docServeId;
            DocId = docId;
            ServeId = serveId;
            DoctorSurname = doctorSurname;
            ServiceName = serviceName;
        }

        public int DocServeId { get; set; }
        public int DocId { get; set; }
        public int ServeId { get; set; }
        public string DoctorSurname { get; set; }
        public string  ServiceName { get; set; }

    }
    public partial class DoctorServiceTable : Page, INotifyPropertyChanged
    {
        private int currentDoctorId = 0;
        private int currentServiceId = 0;

        private DOCTOR_SERVICE _currentDoctorService = new DOCTOR_SERVICE();

        public List<DOCTOR_SERVICE> doctorServices = new List<DOCTOR_SERVICE>();
        public List<DOCTOR_SERVICE> DoctorServices
        {
            get { return doctorServices; }
            set
            {
                doctorServices = value;
                OnPropertyChanged("DoctorServices");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public DoctorServiceTable()
        {
            InitializeComponent();
            Load();

            DoctorIDCombobox.ItemsSource = CLINICSEntities.GetContext().DOCTORs.ToList(); ;
            ServiceIDCombobox.ItemsSource = CLINICSEntities.GetContext().SERVICEs.ToList();
            DoctorServices = CLINICSEntities.GetContext().DOCTOR_SERVICE.ToList();

            DataContext = _currentDoctorService;
        }

        private List<SERVICE> getServiceByDoctorID(int curDocId)
        {
            List<SERVICE> ownedService = DoctorServices.Where(docServ => docServ.DoctorID == curDocId).Select(ds => ds.SERVICE).ToList();
            List<SERVICE> allService = CLINICSEntities.GetContext().SERVICEs.ToList();
            List<SERVICE> result = allService.Where(s => ownedService.All(s2 => s2.ServiceID != s.ServiceID)).ToList();

            return result;
        }

        private List<DOCTOR> getDoctorByServiceID(int curServId)
        {
            //return DoctorServices.Where(docServ => docServ.ServiceID == curServId).Select(ds => ds.DOCTOR).ToList();
            List<DOCTOR> ownedDoctors = DoctorServices.Where(docServ => docServ.ServiceID == curServId).Select(ds => ds.DOCTOR).ToList();
            List<DOCTOR> allDoctors = CLINICSEntities.GetContext().DOCTORs.ToList();
            List<DOCTOR> result = allDoctors.Where(s => ownedDoctors.All(s2 => s2.DoctorID != s.DoctorID)).ToList();

            return result;
        }

        private void DoctorIDCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentDoctorId = (int)(sender as ComboBox).SelectedValue;
            ServiceIDCombobox.ItemsSource = getServiceByDoctorID(currentDoctorId);
        }

        private void ServiceIDCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentServiceId = (int)(sender as ComboBox).SelectedValue;
            DoctorIDCombobox.ItemsSource = getDoctorByServiceID(currentServiceId);
            if (ServiceIDCombobox.SelectedItem == null)
            {
                DoctorIDCombobox.IsEnabled = false;
            }
            else
            {
                DoctorIDCombobox.IsEnabled = true;

            }
            //DoctorIDCombobox.ItemsSource = getDoctorByServiceID((int)(sender as ComboBox).SelectedValue);
        }
        private void Load()
        {
            DoctorServiceDataGrid.ItemsSource = CLINICSEntities.GetContext().DOCTOR_SERVICE.ToList();

            //using (CLINICSEntities db = new CLINICSEntities())
            //{
            //    var doctorServiceDataGrid = db.DOCTOR_SERVICE.Join(
            //                                db.DOCTORs,
            //                                ds => ds.DoctorID,
            //                                d => d.DoctorID,
            //                                (ds, d) => new { ds, d }).Join(
            //                                db.SERVICEs,
            //                                res1 => res1.ds.ServiceID,
            //                                s => s.ServiceID,
            //                                (res1, s) => new
            //                                {
            //                                    docServId = res1.ds.DoctorServiceID,
            //                                    docId = res1.d.DoctorID,
            //                                    servId = s.ServiceID,
            //                                    doctrorSurname = res1.d.DoctorSurname,
            //                                    serviceName = s.ServiceName
            //                                }).ToList();
            //    List<Wrapper> w = new List<Wrapper>();
            //    foreach (var obj in doctorServiceDataGrid) w.Add(new Wrapper(obj.docServId, obj.docId, obj.servId, obj.doctrorSurname, obj.serviceName));
            //    DoctorServiceDataGrid.ItemsSource = w;
            //}
        }
        private void ClearTextBox()
        {
        }
        private void add_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder emptyDataErrors = new StringBuilder();

            if (DoctorIDCombobox.SelectedItem == null)
            {
                emptyDataErrors.AppendLine("Вы не выбрали фамилию врача");
            }
            if (ServiceIDCombobox.SelectedItem == null)
            {
                emptyDataErrors.AppendLine("Вы не выбрали телефон клиента");
            }
            if (emptyDataErrors.Length > 0)
            {
                MessageBox.Show(emptyDataErrors.ToString());
                return;
            }

            DOCTOR_SERVICE _currentDoctorService = new DOCTOR_SERVICE();

            CLINICSEntities.GetContext().DOCTOR_SERVICE.Add(_currentDoctorService);
            try
            {
                DOCTOR selectedDoctor = (DOCTOR)DoctorIDCombobox.SelectedItem;
                SERVICE selectedService = (SERVICE)ServiceIDCombobox.SelectedItem;
                _currentDoctorService.DoctorID = selectedDoctor.DoctorID;
                _currentDoctorService.ServiceID = selectedService.ServiceID;
            }
            catch
            {
                MessageBox.Show("Выберите фамилию врача и операцию");
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
            //var objectsToRemove = DoctorServiceDataGrid.SelectedItems.Cast<Wrapper>().ToList();
            //List<int> ids = new List<int>();
            //foreach (Wrapper w in objectsToRemove) ids.Add(w.DocServeId);

            var elementsToRemove = DoctorServiceDataGrid.SelectedItems.Cast<DOCTOR_SERVICE>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить {elementsToRemove.Count()} элемент(ов)?", "Внимание",
            //if (MessageBox.Show($"Вы точно хотите удалить {ids.Count()} элемент(ов)?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                try
                {
                    //foreach (int id in ids)
                    //{
                    //    var obj = CLINICSEntities.GetContext().DOCTOR_SERVICE.Single(ds => ds.DoctorServiceID == id);
                    //    if (obj != null)
                    //    {
                    //            CLINICSEntities.GetContext().DOCTOR_SERVICE.Attach(obj);
                    //            CLINICSEntities.GetContext().DOCTOR_SERVICE.Remove(obj);
                    //        try
                    //        {
                    //            CLINICSEntities.GetContext().SaveChanges();
                    //        }
                    //        catch (DbEntityValidationException ex)
                    //        {
                    //            //MessageBox.Show(ex.Message);
                    //            foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                    //            {
                    //                MessageBox.Show("Object: " + validationError.Entry.Entity.ToString());
                    //                MessageBox.Show(" ");

                    //                foreach (DbValidationError err in validationError.ValidationErrors)
                    //                {
                    //                    MessageBox.Show(err.ErrorMessage + " ");

                    //                }
                    //            }
                    //        }

                    //    }

                    //}

                    CLINICSEntities.GetContext().DOCTOR_SERVICE.RemoveRange(elementsToRemove);
                    try
                    {
                        CLINICSEntities.GetContext().SaveChanges();
                        MessageBox.Show("Данные удалены");
                        Load();
                    }
                    catch
                    {
                        MessageBox.Show("Удаление невозможно");
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

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            DOCTOR_SERVICE sel = DoctorServiceDataGrid.SelectedItem as DOCTOR_SERVICE;

            _currentDoctorService = sel;

            DoctorIDCombobox.SelectedValue = _currentDoctorService.DoctorID;
            ServiceIDCombobox.SelectedValue = _currentDoctorService.ServiceID;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                DOCTOR selectedDoctor = (DOCTOR)DoctorIDCombobox.SelectedItem;
                SERVICE selectedService = (SERVICE)ServiceIDCombobox.SelectedItem;
                _currentDoctorService.DoctorID = selectedDoctor.DoctorID;
                _currentDoctorService.ServiceID = selectedService.ServiceID;

                CLINICSEntities.GetContext().Entry(_currentDoctorService).State = System.Data.Entity.EntityState.Modified;

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
