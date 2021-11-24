using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using GoninDigital.Models;

namespace GoninDigital.ViewModels
{
    class HomePageViewModel : BaseViewModel
    {
        private string art;
        public string Art
        {
            get { return art; }
            set { art = value; OnPropertyChanged(); }
        }

        private List<Product> recommendedByEditor = null;
        public List<Product> RecommendedByEditor
        {
            get { return recommendedByEditor; }
            set { recommendedByEditor = value; OnPropertyChanged(); }
        }

        private string artGroup1;
        public string ArtGroup1
        {
            get => artGroup1;
        }

        private string artGroup2;
        public string ArtGroup2
        {
            get => artGroup2;
        }

        private void Init()
        {
            using (var db = new GoninDigitalDBContext())
            {
                RecommendedByEditor = db.Products.ToList();
            }
        }

        public HomePageViewModel()
        {
            art = "/Resources/Images/HomeBanner.jpg";
            artGroup1 = "/Resources/Images/HomeProductCardGroupBackground.png";
            artGroup2 = "/Resources/Images/HomeProductCardGroupBackground2.jpg";
            Thread thread = new Thread(Init);
            thread.Start();
        }
    }
}
