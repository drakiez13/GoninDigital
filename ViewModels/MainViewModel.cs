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
using Microsoft.EntityFrameworkCore;

namespace GoninDigital.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand LoadedWidnowCommand { get; set; }
        private async void Load(Window p)
        {
            if (Settings.Default.usrname != "" && Settings.Default.passwod != "")
            {
                using (var db = new GoninDigitalDBContext())
                {
                    if ((await db.Users.FirstOrDefaultAsync(o => o.UserName == Settings.Default.usrname)).TypeId == (int)Constants.UserType.ADMIN)
                    {
                        var adminWindow = new AdminView();
                        WindowManager.ChangeWindowContent(p, adminWindow, Resources.AdminpageWindowTitle, Resources.AdminpageControlPath);
                    }
                    else
                    {
                        DashBoard.PreLoad();
                        var dashboardWindow = new DashBoard();
                        await Task.Delay(3000);
                        WindowManager.ChangeWindowContent(p, dashboardWindow, Resources.HomepageWindowTitle, Resources.HomepageControlPath);
                    }
                }

            }
            else //login
            {
                //var loginWindow = new LoginViewModel(p);
                WindowManager.ChangeWindowContent(p, Resources.LoginWindowTitle, Resources.LoginControlPath);
            }
        }
        public MainViewModel()
        {
            if (Settings.Default.accentColor != "")
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
            LoadedWidnowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                WindowManager.ChangeWindowContent(p, "", "GoninDigital.Views.SplashScreenView");

                Load(p);
            });
        }
    }
}
