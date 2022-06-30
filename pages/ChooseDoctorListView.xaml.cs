using System;
using System.Collections.Generic;
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
namespace CLINICS.pages
{
    /// <summary>
    /// Interaction logic for ChooseDoctorListView.xaml
    /// </summary>
    public partial class ChooseDoctorListView : Page
    {
        private int _idOfChosenService;

        public ChooseDoctorListView(int idOfChosenService)
        {
            InitializeComponent();
            _idOfChosenService = idOfChosenService;
            using (CLINICSEntities db = new CLINICSEntities())
            {
                var doctorButtons = from doctorServices in db.DOCTOR_SERVICE
                                    join doctors in db.DOCTORs
                                    on doctorServices.DoctorID equals doctors.DoctorID
                                    where doctorServices.ServiceID.ToString() ==_idOfChosenService.ToString()
                                    select new
                                    {
                                        CurrentName = doctors.DoctorSurname + " " + doctors.DoctorName + " " + doctors.DoctorPatronymic,
                                        Id = doctors.DoctorID,
                                        Image = doctors.DoctorImage
                                    };

                if (doctorButtons.Count() == 0)
                {
                    inCaseNullDocs.Text = "Врачи на эту процедуру не добавлены!";
                    return;
                }
                foreach (var doc in doctorButtons)
                {
                    string currentUserFio = App.currentClient.ClientSurname + " " + App.currentClient.ClientName + " " + App.currentClient.ClientPatronymic;
                    if (currentUserFio == doc.CurrentName)
                    {
                        continue;
                    }
                    var buf = new DoctorsButtonUserControl(doc.CurrentName, doc.Id, doc.Image, _idOfChosenService);
                    DoctorsItemsControl.Items.Add(buf);
                }
            }
        }
        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
            MainWindow.MenuStackPanel.Visibility = Visibility.Hidden;
        }




















        //public static DOCTOR chosenDoctorButton;
        //public static SERVICE chosenServiceNameButtonCopy;

        //chosenServiceNameButtonCopy = chosenServiceNameButton;
        //var doctorService = CLINICSEntities.GetContext().DOCTOR_SERVICE.Where(p => p.ServiceID == chosenServiceNameButton.ServiceID).ToList();

        //List<DOCTOR> doctorsFromDOCTORlist = new List<DOCTOR>();
        //foreach (DOCTOR_SERVICE doctServ in doctorService)
        //{
        //    var doctorsFromDOCTOR = CLINICSEntities.GetContext().DOCTORs.Where(p => p.DoctorID == doctServ.DoctorID).FirstOrDefault();
        //    doctorsFromDOCTORlist.Add(doctorsFromDOCTOR);

        //DoctorsListView.ItemsSource = doctorsFromDOCTORlist;

        //chosenServiceNameButtonCopy.ServiceID
        //////////var doctors = CLINICSEntities.GetContext().DOCTORs.ToList();
        //////////var doctorServices = CLINICSEntities.GetContext().DOCTOR_SERVICE.ToList();

        //////////это статическая переменная, которая в себе содержит выбранный объект-сервис из прошлой страницы
        //////////с выбором услуг



        //SERVICE service = ChooseService.chosenServiceButton;


        //int id;

        //{
        //    id = serv.ServiceID;
        //}



        /*var doctorAndSpecialService = CLINICSEntities.GetContext().DOCTOR_SERVICE
            .Where(p => p.ServiceID == service.ServiceID).ToList();

        foreach(DOCTOR_SERVICE doc in doctorAndSpecialService)
        {
            DoctorsListView.SelectedItem = doc.DoctorID;
        }*/
    }
}
