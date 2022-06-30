using CLINICS.models;
using CLINICS.pages;
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
using System.Windows.Shapes;

namespace CLINICS.windows
{
    /// <summary>
    /// Interaction logic for DoctorInfoWindow.xaml
    /// </summary>
    public partial class DoctorInfoWindow : Window
    {
        private List<string> _chosenDocComments = new List<string>();
        private int _chosenDocId { get; set; }

        private List<string> services = new List<string>();
        public DoctorInfoWindow(int chosenDocId, string chosenDocSurname, string chosenDocName, string chosenDocPatronymic, string chosenDocImage, List<string> chosenDocComments, double chosenDocRating)
        {
            InitializeComponent();
            foreach (var com in chosenDocComments)
            {
                _chosenDocComments.Add(com);
            }

            BitmapImage myBitmapImage = new BitmapImage(new Uri(chosenDocImage));
            myBitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            docImage.Source = myBitmapImage;

            doctorFIO.Text = chosenDocSurname + " " + chosenDocName.Remove(1) + ". " + chosenDocPatronymic.Remove(1) + ".";

            BitmapImage star = new BitmapImage(new Uri("E:/2 курс БГТУ/!КУРСАЧ ООП/CLINICS/icons/Rating.png"));
            star.CacheOption = BitmapCacheOption.OnLoad;
            ratingIcon.Source = star;
            rating.Text = chosenDocRating.ToString("0.##");

            foreach (var com in chosenDocComments)
            {
                commentItemsControl.Items.Add(com);
            }
            _chosenDocId = chosenDocId;

            using (CLINICSEntities db = new CLINICSEntities())
            {
                var allServicesByDoctorID = (from doc_serv in db.DOCTOR_SERVICE
                                             where doc_serv.DoctorID == _chosenDocId
                                             join serv in db.SERVICEs on
                                             doc_serv.ServiceID equals serv.ServiceID
                                             select new
                                             {
                                                service_id = serv.ServiceID,
                                                service = serv.ServiceName
                                             }).ToList();

                foreach (var serv in allServicesByDoctorID)
                {
                    services.Add(serv.service);
                    sevicesItemsControl.Items.Add(serv.service);
                }
            }
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
