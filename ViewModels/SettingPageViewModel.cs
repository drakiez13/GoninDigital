using GoninDigital.Properties;
using GoninDigital.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GoninDigital.ViewModels
{
    internal class SettingPageViewModel : BaseViewModel
    {
        public ICommand LogOutCommand { get; set; }

        public SettingPageViewModel()
        {
            LogOutCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { LogOutExcute(); });
        }
        void LogOutExcute()
        {
            // clear
            Settings.Default.usrname = "";
            Settings.Default.passwod = "";

            var loginWindow = new LoginViewModel(Application.Current.MainWindow);
            WindowManager.ChangeWindowContent(Application.Current.MainWindow, loginWindow, Resources.LoginWindowTitle, Resources.LoginControlPath);
        }
    }
}
