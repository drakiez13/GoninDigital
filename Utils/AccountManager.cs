using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GoninDigital.Models;
using GoninDigital.Properties;
using GoninDigital.ViewModels;

namespace GoninDigital.Utils
{
    class AccountManager
    {
        public static bool EmailExists(string email)
        {
            return DataProvider.Instance.Db.Users.Find(email) != null;
        }

        public static void RegisterAccount(User new_usr)
        {
            _ = DataProvider.Instance.Db.Users.Add(new_usr);
            _ = DataProvider.Instance.Db.SaveChanges();
        }

        public static bool AccountExists(string usrname, string email)
        {
            int checkUsername = DataProvider.Instance.Db.Users.Where(x => x.UserName == usrname).Count();
            int checkEmail = DataProvider.Instance.Db.Users.Where(x => x.Email == email).Count();
            return checkUsername > 0 || checkUsername > 0;
        }

        public static void ChangePassword(Window window, string email, string newPassword)
        {
            try
            {
                User user = DataProvider.Instance.Db.Users.Find(email);
                if (user != null)
                {
                    user.Password = newPassword;
                    DataProvider.Instance.Db.SaveChanges();
                }

                MessageBox.Show("Password successfully changed");
            }
            catch
            {
                MessageBox.Show("Password unsuccessfully changed");
            }

            var loginViewModel = new LoginViewModel(window);
            WindowManager.ChangeWindowContent(window, loginViewModel, Resources.LoginWindowTitle, Resources.LoginControlPath);
            if (loginViewModel.CloseAction == null)
            {
                loginViewModel.CloseAction = () => window.Close();
            }
        }
    }
}
