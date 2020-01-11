using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrevisaoTempo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new PrevisaoTempo.Views.TempoPage();
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
