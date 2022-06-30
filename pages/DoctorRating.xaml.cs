using CLINICS.models;
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

namespace CLINICS.pages
{
    /// <summary>
    /// Interaction logic for DoctorRating.xaml
    /// </summary>
    public partial class DoctorRating : Page
    {
        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
            MainWindow.MenuStackPanel.Visibility = Visibility.Hidden;
        }
        public DoctorRating()
        {
            InitializeComponent();
            using (CLINICSEntities db = new CLINICSEntities())
            {
                List<DoctorRatingUserControl> DoctorsList = new List<DoctorRatingUserControl>();

                var allDoctors = (from doctor in db.DOCTORs select new
                {
                    DoctorID = doctor.DoctorID,
                    DoctorName = doctor.DoctorName,
                    DoctorSurname = doctor.DoctorSurname,
                    DoctorPatronymic = doctor.DoctorPatronymic,
                    DoctorImage = doctor.DoctorImage
                }).ToList();
                
                foreach(var doc in allDoctors)
                {
                    List<string> comments = new List<string>();
                    double totalRate=0;
                    int i=0;
                    double finalRating=0;
                    //var allCommentsRatings = (from rating in db.DOCTOR_RATING 
                    //                            where rating.DoctorID == doc.DoctorID
                    //                            select new
                    //                            {
                    //                                cliend_id=rating.ClientID,
                    //                                rate = rating.Rating,
                    //                                comment = rating.Comment
                    //                            }).ToList();

                    var allCommentsRatings = (from rating in db.DOCTOR_RATING
                                              where rating.DoctorID == doc.DoctorID
                                              join client in db.CLIENTs
                                              on rating.ClientID equals client.ClientID
                                              select new
                                              {
                                                  client_name = client.ClientName,
                                                  rate = rating.Rating,
                                                  comment = rating.Comment
                                              }).ToList();

                    foreach (var commentRate in allCommentsRatings )
                    {
                        totalRate += commentRate.rate;
                        comments.Add(commentRate.comment);
                        comments.Add(commentRate.client_name);
                        i++;
                    }
                    finalRating = totalRate / (double)i;
                    if (i == 0)
                    {
                        finalRating = 0;
                    }
                    DoctorRatingItemsControl.Items.Add(new DoctorRatingUserControl(doc.DoctorID, doc.DoctorSurname, doc.DoctorName, doc.DoctorPatronymic, doc.DoctorImage, comments, finalRating));
                }
            }
        }
    }
}
