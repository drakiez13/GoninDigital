using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using GoninDigital.Properties;

namespace GoninDigital
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnExit(object sender, ExitEventArgs e)
        {
            Settings.Default.Save();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //initialize the splash screen and set it as the application main window
            var splashScreen = new GoninDigital.Views.SplashScreenWindow();
            this.MainWindow = splashScreen;
            splashScreen.Show();
            //in order to ensure the UI stays responsive, we need to
            //do the work on a different thread
            _ = Task.Factory.StartNew(() =>
              {
                //we need to do the work in batches so that we can report progress
                Models.GoninDigitalDBContext db = new Models.GoninDigitalDBContext();
                  db.Products.ToList();
                  db.Brands.ToList();
                  db.AdDetails.ToList();
                  db.Ads.ToList();
                  db.Brands.ToList();
                  db.Favorites.ToList();
                  db.InvoiceDetails.ToList();
                  db.Invoices.ToList();
                  db.InvoiceStatuses.ToList();
                  db.ProductCategories.ToList();
                  db.ProductImages.ToList();
                  db.ProductImages.ToList();
                  db.ProductSpecDetails.ToList();
                  db.ProductSpecs.ToList();
                  db.Ratings.ToList();
                  db.Vendors.ToList();
                //once we're done we need to use the Dispatcher
                //to create and show the main window
                this.Dispatcher.Invoke(() =>
                  {
                    //initialize the main window, set it as the application main window
                    //and close the splash screen
                    var mainWindow = new MainWindow();
                      this.MainWindow = mainWindow;
                      mainWindow.Show();
                      splashScreen.Close();
                  });
              });
        }
    }
}
