using GoninDigital.Models;
using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GoninDigital.SharedControl
{
    /// <summary>
    /// Interaction logic for DeleteUserDialog.xaml
    /// </summary>
    public partial class DeleteUserDialog : UserControl
    {
        private int UserId;
        public DeleteUserDialog(int UserId)
        {
            InitializeComponent();
            this.UserId = UserId;
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            string reason = txb_Reason.Text;
            string enddate = dp_EndDate.Text;
            if (reason == "" | enddate == "")
            {
                ContentDialog content = new()
                {
                    Title = "Warning",
                    Content = "Please enter full information",
                    PrimaryButtonText = "Ok"
                };
                content.ShowAsync();
            }
            else
            {
                Ban ban = new Ban();
                ban.UserId = UserId;
                ban.Reason = reason;
                if (enddate[1] == '/')
                    enddate = "0" + enddate;
                if (enddate[4] == '/')
                    enddate = enddate.Insert(3, "0");
                Error.Text = enddate;
                ban.EndDate = DateTime.ParseExact(enddate, "mm/dd/yyyy",CultureInfo.InvariantCulture);
                using(var db=new GoninDigitalDBContext())
                {
                    db.Bans.Add(ban);
                    _ = db.SaveChanges();
                }
                ContentDialog content = new()
                {
                    Title = "Complete",
                    Content = "Deleted Successfully",
                    PrimaryButtonText = "Ok"
                };
                content.ShowAsync();

            }    
        }
    }
}
