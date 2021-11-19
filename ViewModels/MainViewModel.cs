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

namespace GoninDigital.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand LoadedWidnowCommand { get; set; }

        public MainViewModel()
        {
            LoadedWidnowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                if (Settings.Default.usrname != "")
                {
                    var dashboardWindow = new DashBoard();
                    WindowManager.ChangeWindowContent(p, dashboardWindow, Resources.HomepageWindowTitle, Resources.HomepageControlPath);
                }
                else //login
                {
                    var loginWindow = new LoginViewModel(p);
                    WindowManager.ChangeWindowContent(p, loginWindow, Resources.LoginWindowTitle, Resources.LoginControlPath);
                }

            }); 
        }
    }
}
