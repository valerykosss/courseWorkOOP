using CLINICS.windows;
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
    /// Interaction logic for DoctorRatingUserControl.xaml
    /// </summary>
    public partial class DoctorRatingUserControl : UserControl
    {
        /*doc.DoctorSurname, doc.DoctorName, doc.DoctorPatronymic, doc.DoctorImage, comments, finalRating, clientsId*/
        private int chosenDocId { get; set; }
        private string chosenDocSurname { get; set; }
        private string chosenDocName { get; set; }
        private string chosenDocPatronymic { get; set; }
        private string chosenDocImage { get; set; }
        private List<string> chosenDocComments = new List<string>();
        private double chosenDocRating { get; set; }
        public DoctorRatingUserControl(int DoctorID, string DoctorSurname, string DoctorName, string DoctorPatronymic, string DoctorImage, List<string> Comments, double Rating)
        {
            InitializeComponent();
            BitmapImage myBitmapImage = new BitmapImage(new Uri(DoctorImage));
            myBitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            doctorImage.Source = myBitmapImage;
            docFIO.Text = DoctorSurname + " " + DoctorName.Remove(1) + ". " + DoctorPatronymic.Remove(1) + ".";
            rating.Text = Rating.ToString("0.##");
            if (Comments.Count == 0)
            {
                commentItemsControl.Visibility = Visibility.Collapsed;
            }
            commentItemsControl.Items.Add(Comments.FirstOrDefault());
            BitmapImage star = new BitmapImage(new Uri("E:/2 курс БГТУ/!КУРСАЧ ООП/CLINICS/icons/Rating.png"));
            star.CacheOption = BitmapCacheOption.OnLoad;
            ratingIcon.Source = star;

            chosenDocSurname = DoctorSurname;
            chosenDocName = DoctorName;
            chosenDocPatronymic = DoctorPatronymic;
            chosenDocImage = DoctorImage;
            chosenDocId = DoctorID;
            chosenDocRating = Rating;

            foreach (var com in Comments)
            {
                chosenDocComments.Add(com);
            }
        }

        private void chosenDoctorInfo_Click(object sender, RoutedEventArgs e)
        {
            DoctorInfoWindow doctorInfoWindow = new DoctorInfoWindow(chosenDocId, chosenDocSurname, chosenDocName, chosenDocPatronymic, chosenDocImage, chosenDocComments, chosenDocRating);
            doctorInfoWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            doctorInfoWindow.Show();
        }
    }
}
