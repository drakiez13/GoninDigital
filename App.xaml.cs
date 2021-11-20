using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using GoninDigital.Properties;

namespace GoninDigital
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnExit(object sender, ExitEventArgs e)
        {
            Settings.Default.Save();
        }
    }
}
