using PrevisaoTempo.Models;
using PrevisaoTempo.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrevisaoTempo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TempoPage : ContentPage
    {
        public TempoPage()
        {
            InitializeComponent();
            this.Title = "QUAY - Previsão do Tempo";
            this.BindingContext = new Tempo();
        }

        private async void btnPrevisao_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(cidadeEntry.Text))
                {
                    Tempo prevTempo = await DataService.GetPrevisaoDoTempo(cidadeEntry.Text);
                    this.BindingContext = prevTempo;
                    btnPrevisao.Text = "Nova Previsão";
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro ", ex.Message, "OK");
            }
        }
    }
}