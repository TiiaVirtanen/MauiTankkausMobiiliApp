using MauiTankkausApp.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace MauiTankkausApp;

public partial class LisaaTankkausPage : ContentPage
{
    private int _ajoneuvoId;
	public LisaaTankkausPage(int ajoneuvoId, string rekisterinumero)
	{
		InitializeComponent();
        _ajoneuvoId = ajoneuvoId;
        RekNroLabel.Text = rekisterinumero;
    }

    private async Task AddDataToRestAPI(string rekisterinumero, int ajokilometrit, decimal litraa, decimal euroa)
    {
        try
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://restapibensa24.azurewebsites.net/");

            //Luodaan uusi olio k�ytt�j�n sy�tt�mill� tiedoilla
            Tankkaus tankkaus = new Tankkaus
            {
                AjoneuvoId = _ajoneuvoId,
                Ajokilometrit = ajokilometrit,
                Litraa = litraa,
                Euroa = euroa,
                P�iv�m��r� = DateOnly.FromDateTime(DateTime.Now),
                Ajoneuvo = null
            };

            // Muunnetaan Tankkaus-olio JSON-muotoon
            string json = JsonConvert.SerializeObject(tankkaus);

            // Luodaan uusi StringContent-olio JSON-datalle
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // L�hetet��n POST-pyynt� REST-rajapintaan
            HttpResponseMessage response = await client.PostAsync($"api/tankkaus/", content);

            // Tarkista vastauksen tila
            if (response.IsSuccessStatusCode)
            {
                // Lis�ys onnistui
                await DisplayAlert("Lis�ys", "Tiedot lis�tty onnistuneesti!", "OK");

                await Navigation.PopAsync(); // Palaa edelliselle sivulle
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
        string rekisterinumero = RekNroLabel.Text;

        // Yritet��n muuntaa sy�tteet oikeiksi tyypeiksi
        if (int.TryParse(ajokilometritKentta.Text, out int ajokilometrit) &&
            decimal.TryParse(litraaKentta.Text, out decimal litraa) &&
            decimal.TryParse(euroaKentta.Text, out decimal euroa))
        {
            await AddDataToRestAPI(rekisterinumero, ajokilometrit, litraa, euroa);
        }
        else
        {
            await DisplayAlert("Virhe", "Tarkista sy�tt�m�si tiedot. Kaikkien kenttien tulee olla kelvollisia.", "OK");
        }
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