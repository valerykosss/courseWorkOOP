using CLINICS.models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CLINICS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>

    public partial class App : Application
    {
        public static CLIENT currentClient { get; set; }
        public App()
        {
            App.currentClient = new CLIENT();
        }
    }
}
