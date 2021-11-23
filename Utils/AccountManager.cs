using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GoninDigital.Models;
using GoninDigital.Properties;
using GoninDigital.ViewModels;
using ModernWpf.Controls;

namespace GoninDigital.Utils
{
    class AccountManager
    {
        public static bool EmailExists(string email)
        {
            GoninDigitalDBContext context = new();
            return context.Users.FirstOrDefault(b => b.Email == email) != default;
        }

        public static void RegisterAccount(User new_usr)
        {
            GoninDigitalDBContext context = new();
            context.Users.Add(new_usr);
            context.SaveChanges();
        }

        public static bool AccountExists(string usrname, string email)
        {
            GoninDigitalDBContext context = new();
            return context.Users.FirstOrDefault(b => b.Email==email || b.UserName==usrname) != default;
        }

        public static void ChangePassword(Window window, string email, string newPassword)
        {
            try
            {
                GoninDigitalDBContext context = new();
                User user = context.Users.FirstOrDefault(b=>b.Email==email);
                if (user != default)
                {
                    user.Password = newPassword;
                    context.SaveChanges();
                }
                var content = new ContentDialog
                {
                    Title = "Congratz",
                    Content = "Password successfully changed.",
                    PrimaryButtonText = "Ok"
                };
                content.ShowAsync();
            }
            catch
            {
                var content = new ContentDialog
                {
                    Title = "Warning",
                    Content = "Password unsuccessfully changed.",
                    PrimaryButtonText = "Ok"
                };
                content.ShowAsync();
            }

            LoginViewModel loginViewModel = new(window);
            WindowManager.ChangeWindowContent(window, loginViewModel, Resources.LoginWindowTitle, Resources.LoginControlPath);
            if (loginViewModel.CloseAction == null)
            {
                loginViewModel.CloseAction = () => window.Close();
            }
        }
    }
}
