using GoninDigital.Properties;
using GoninDigital.Utils;
using GoninDigital.ViewModels.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GoninDigital.ViewModels
{
    internal class EmailNotification
    {
        public bool SendEmailCode(Window window, string dst_email, string emailSubject, string emailContent)
        {
            var confirmationCode = new Random().Next(100000, 999999).ToString();
            emailContent += confirmationCode;
            var Validator = new PersonalAccountValidator().ValidateEmail(dst_email, emailSubject, emailContent);
            if (dst_email == null || !Validator.IsValid)
            {
                MessageBox.Show(Validator.ValidationMessage);
                return false;
            }

            var confirmationCodeViewModel = new ConfirmationCodeViewModel(window, dst_email, confirmationCode);
            WindowManager.ChangeWindowContent(window, confirmationCodeViewModel, Resources.ConfirmationCodeWindowTitle, Resources.ConfirmationCodeControlPath);

            if (confirmationCodeViewModel.CloseAction == null)
            {
                confirmationCodeViewModel.CloseAction = () => window.Close();
            }
            return true;
        }
    }
}
