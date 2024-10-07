using MauiTankkausApp.Models;
using Newtonsoft.Json;
using System.Text;

namespace MauiTankkausApp;

public partial class LisaaRekisteriNroPage : ContentPage
{
	public LisaaRekisteriNroPage()
	{
		InitializeComponent();
	}

    public LisaaRekisteriNroPage(string rekisterinumero, string merkki, string malli)
    {
        InitializeComponent();

        RekNroKentta.Text = rekisterinumero;
        MerkkiKentta.Text = merkki;
        MalliKentta.Text = malli;
    }

    private async Task AddDataToRestAPI(string rekisterinumero, string merkki, string malli)
    {
        try
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://restapibensa24.azurewebsites.net/");

            //Luodaan uusi olio k�ytt�j�n sy�tt�mill� tiedoilla
            Ajoneuvot ajoneuvot = new Ajoneuvot
            {
                Rekisterinumero = rekisterinumero,
                Merkki = merkki,
                Malli = malli
            };

            // Muunnetaan Tankkaus-olio JSON-muotoon
            string json = JsonConvert.SerializeObject(ajoneuvot);

            // Luodaan uusi StringContent-olio JSON-datalle
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // L�hetet��n POST-pyynt� REST-rajapintaan
            HttpResponseMessage response = await client.PostAsync($"api/ajoneuvot/", content);

            // Tarkista vastauksen tila
            if (response.IsSuccessStatusCode)
            {
                // Lis�ys onnistui
                await DisplayAlert("Lis�ys", "Tiedot lis�tty onnistuneesti!", "OK");

                RekNroKentta.Text = "";
                MerkkiKentta.Text = "";
                MalliKentta.Text = "";
            }
            else
            {
                // Lis�ys ep�onnistui
                await DisplayAlert("Virhe", "Tietojen lis�ys ep�onnistui.", "OK");
            }        
        }
        catch (Exception e)
        {
            await DisplayAlert("Virhe", e.Message.ToString(), "OK");
        }
    }

    private async void TallennaButton_Clicked(object sender, EventArgs e)
    {
        string rekisterinumero = RekNroKentta.Text;
        string merkki = MerkkiKentta.Text;
        string malli = MalliKentta.Text;

        await AddDataToRestAPI(rekisterinumero, merkki, malli);
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