using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Controls;
using GoninDigital.Models;
using GoninDigital.Utils;
using GoninDigital.Views;
using ModernWpf.Controls;
using GoninDigital.Properties;
using System.Threading;

namespace GoninDigital.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Properties
        Window curWindow;

        private User _isExist;
        public User isExist 
        { 
            get { return _isExist; }
            set { _isExist = value; OnPropertyChanged(); }
        }

        private string art;
        public string Art
        {
            get { return art; }
            set { art = value; OnPropertyChanged(); }
        }
        public Action CloseAction { get; set; }
        private string _Usrname;
        public string UserName
        {
            get { return _Usrname; }
            set
            {
                _Usrname = value;
                OnPropertyChanged(UserName);
            }
        }
        private string _Passwrd;
        public string Password
        {
            get { return _Passwrd; }
            set
            {
                _Passwrd = value;
                OnPropertyChanged(Password);
            }
        }
        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        #endregion

        #region Constructor
        public LoginViewModel(Window window)
        {
            art = "/GoninDigital;component/Resources/Images/LoginImage.jpg";
            curWindow = window;
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { 
                
                LoginCommandExecute(); 
            });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
            RegisterCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { RegisterCommandExcute(); });
            ResetCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { ResetCommandExcute(); });
        }
        #endregion

        #region Private Methods
        
        private void LoginCommandExecute()
        {

            if (UserName == null || Password == null)
            {
                var content = new ContentDialog();
                content.Title = "Warning";
                content.Content = "Both username and password should be filled in.";
                content.PrimaryButtonText = "Ok";
                content.ShowAsync();
                return;
            }

            //initialize the splash screen and set it as the application main window
            WindowManager.ChangeWindowContent(Application.Current.MainWindow, "", "GoninDigital.Views.SplashScreenView");

            //in order to ensure the UI stays responsive, we need to
            //do the work on a different thread
            Task.Factory.StartNew(() =>
            {
                //we need to do the work in batches so that we can report progress
                GoninDigitalDBContext context = new();
                string passEncode = Cryptography.MD5Hash(Cryptography.Base64Encode(Password));
                isExist = context.Users.FirstOrDefault(x => x.UserName == UserName && x.Password == passEncode);

                //once we're done we need to use the Dispatcher
                //to create and show the main window
                Application.Current.Dispatcher.Invoke(() =>
                {
                    WindowManager.ChangeWindowContent(Application.Current.MainWindow, "", "GoninDigital.Views.LoginView");

                    //initialize the main window, set it as the application main window
                    //and close the splash screen
                    if (isExist != default)
                    {
                        var dashboardWindow = new DashBoard();
                        if (isExist.TypeId == 1) //admin
                        {
                            // Admin not save under resource setting
                            WindowManager.ChangeWindowContent(curWindow, dashboardWindow, Resources.AdminpageWindowTitle, Resources.AdminpageControlPath);
                        }
                        else //user
                        {
                            // save user under setting resource
                            Settings.Default.usrname = UserName.ToString();
                            Settings.Default.passwod = passEncode;

                            WindowManager.ChangeWindowContent(curWindow, dashboardWindow, Resources.HomepageWindowTitle, Resources.HomepageControlPath);
                        }
                    }
                    else //login
                    {
                        var content = new ContentDialog
                        {
                            Title = "Warning",
                            Content = "Invalid credentials.",
                            PrimaryButtonText = "Ok"
                        };
                        content.ShowAsync();
                    }
                });
            });
        }
        private void RegisterCommandExcute()
        {
            var registerWindow = new RegisterViewModel(curWindow);
            WindowManager.ChangeWindowContent(curWindow, registerWindow, Resources.RegisterAccountWindowTitle, Resources.RegisterAccountControlPath);
            if (registerWindow.CloseAction == null)
            {
                registerWindow.CloseAction = () => curWindow.Close();
            }
        }

        private void ResetCommandExcute()
        {
            var forgotpasswordWindow = new ForgotPasswordViewModel(curWindow);
            WindowManager.ChangeWindowContent(curWindow, forgotpasswordWindow, Resources.ForgotPasswordWindowTitle, Resources.ForgotPasswordControlPath);
            if (forgotpasswordWindow.CloseAction == null)
            {
                forgotpasswordWindow.CloseAction = () => curWindow.Close();
            }
        }
        #endregion
    }
}