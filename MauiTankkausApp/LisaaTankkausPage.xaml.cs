using MauiTankkausApp.Models;
using Newtonsoft.Json;
using System.Text;

namespace MauiTankkausApp;

public partial class LisaaTankkausPage : ContentPage
{
    private string _rekisterinumero;
	public LisaaTankkausPage(string rekisterinumero)
	{
		InitializeComponent();
        _rekisterinumero = rekisterinumero;
        RekNroLabel.Text = rekisterinumero;
	}

    private async void EtusivuButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }

    private async void TakaisinButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
}