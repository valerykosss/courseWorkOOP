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
    /// Interaction logic for CommentInputUserControl.xaml
    /// </summary>
    public partial class CommentInputUserControl : UserControl
    {
        public CommentInputUserControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        public static DependencyProperty TitleDependencyProperty;
        public static DependencyProperty MaxLengthDependencyProperty;

        static CommentInputUserControl()
        {
            TitleDependencyProperty = DependencyProperty.Register("Title", typeof(string), typeof(CommentInputUserControl));

            MaxLengthDependencyProperty = DependencyProperty.Register("MaxLength", typeof(int), typeof(CommentInputUserControl));
        }
        public string Title
        {
            get { return (string)GetValue(TitleDependencyProperty); }
            set { SetValue(TitleDependencyProperty, value); }
        }
        public int MaxLength
        {
            get { return (int)GetValue(MaxLengthDependencyProperty); }
            set { SetValue(MaxLengthDependencyProperty, value); }
        }
    }
}
