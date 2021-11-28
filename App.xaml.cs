using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using GoninDigital.Properties;
using GoninDigital.Models;
using System.Threading;

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
        private void InitAds()
        {
            using (var db = new GoninDigitalDBContext())
            {
                var Ads = db.Ads.OrderBy(o => Guid.NewGuid()).Take(3).ToList();
                List<List<Product>> _adProducts = new List<List<Product>>(3);
                for (int i = 0; i < 3; i++)
                {
                    _adProducts.Add(db.AdDetails.Where(o => o.AdId == Ads[i].Id).Select(ob => ob.Product).ToList());

                }
                _ = _adProducts;
            }
        }

        private void InitProducts()
        {
            using (var db = new GoninDigitalDBContext())
            {
                // Selection algorithm goes here
                _ = db.Products.OrderBy(o => Guid.NewGuid()).Take(6).ToList();

                _ = db.Products.OrderBy(o => Guid.NewGuid()).Take(6).ToList();

                _ = db.Products.OrderBy(o => Guid.NewGuid()).Take(6).ToList();
            }
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //initialize the splash screen and set it as the application main window
            var splashScreen = new GoninDigital.Views.SplashScreen();
            this.MainWindow = splashScreen;
            splashScreen.Show();
            //in order to ensure the UI stays responsive, we need to
            //do the work on a different thread
            Task.Factory.StartNew(() =>
            {
                //we need to do the work in batches so that we can report progress
                var ads = new List<Ad>(3);
                var adProducts = new List<List<Product>>(3);
                Thread thread1 = new Thread(InitAds);
                thread1.Start();
                Thread thread2 = new Thread(InitProducts);
                thread2.Start();
                Thread.Sleep(1000);
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
