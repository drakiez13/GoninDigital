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

namespace GoninDigital.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand LoadedWidnowCommand { get; set; }

        public MainViewModel()
        {
            LoadedWidnowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                if (false)
                {
                    var dashboardWindow = new DashBoard();
                    if (true) //admin
                    {
                        WindowManager.ChangeWindowContent(p, dashboardWindow, "", "GoninDigital.Views.AdminView");
                    }
                    else //user
                    {
                        
                        WindowManager.ChangeWindowContent(p, dashboardWindow, "", "GoninDigital.Views.DashBoard");
                    }
                }
                else //login
                {
                    var loginWindow = new LoginViewModel(p);
                    WindowManager.ChangeWindowContent(p, loginWindow, "", "GoninDigital.Views.LoginView");
                }

            }); 
        }
    }
}
