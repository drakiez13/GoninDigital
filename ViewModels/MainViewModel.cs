using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using GoninDigital.Models;
using GoninDigital.Views;
using System.Windows.Input;
using GoninDigital.Utils;
using GoninDigital.Properties;
using GoninDigital.SharedControl;
using System.Windows.Media;

namespace GoninDigital.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand LoadedWidnowCommand { get; set; }
        public MainViewModel()
        {
            if(Settings.Default.accentColor != "")
            {
                ThemeManagerProxy.Current.AccentColor = (Color)ColorConverter.ConvertFromString(Settings.Default.accentColor);
            }
            if (!Settings.Default.systemTheme)
            {
                if (Settings.Default.theme)
                {
                    ThemeManagerProxy.Current.ApplicationTheme = ModernWpf.ApplicationTheme.Light;
                }
                else
                {
                    ThemeManagerProxy.Current.ApplicationTheme = ModernWpf.ApplicationTheme.Dark;
                }
            }
            LoadedWidnowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                if (Settings.Default.usrname != "")
                {
                    var dashboardWindow = new DashBoard();
                    WindowManager.ChangeWindowContent(p, dashboardWindow, Resources.HomepageWindowTitle, Resources.HomepageControlPath);
                }
                else //login
                {
                    //var loginWindow = new LoginViewModel(p);
                    WindowManager.ChangeWindowContent(p, Resources.LoginWindowTitle, Resources.LoginControlPath);
                }
            }); 
        }
    }
}
