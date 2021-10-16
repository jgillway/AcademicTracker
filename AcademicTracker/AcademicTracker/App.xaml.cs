using System;
using Xamarin.Forms;
using AcademicTracker.Persistance; 
using Xamarin.Forms.Xaml;

namespace AcademicTracker
{
    public partial class App : Application
    {
        public static string FilePath; 
        public App()
        {
            InitializeComponent();
            

            MainPage = new NavigationPage(new Views.MainPage());
        }
        public App(string filePath)
        {
            FilePath = filePath;
            InitializeComponent();
            DBH.InitializeDB();

            MainPage = new NavigationPage(new Views.MainPage());

            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
