using MauiTankkausApp.Models;
using Newtonsoft.Json;
using System.Text;

namespace MauiTankkausApp;

public partial class EditTankkauksetPage : ContentPage
{
    private int _id;
    public EditTankkauksetPage(int id, string rekisterinumero, double ajokilometrit, double litraa, double euroa)
	{
		InitializeComponent();

        _id = id;
        rekisterinumeroKentta.Text = rekisterinumero;
        ajokilometritKentta.Text = ajokilometrit.ToString();
        litraaKentta.Text = litraa.ToString();
        euroaKentta.Text = euroa.ToString();
    }

    private async Task UpdateDataToRestAPI(int id, string rekisterinumero, double ajokilometrit, double litraa, double euroa)
    {
        try
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://restapibensa.azurewebsites.net/");

            // Luodaan uusi olio k�ytt�j�n sy�tt�mill� tiedoilla
            Tankkaus tankkaus = new Tankkaus
            {
                Id = id,
                Rekisterinumero = rekisterinumero,
                Ajokilometrit = ajokilometrit,
                Litraa = litraa,
                Euroa = euroa
            };

            // Muunnetaan Tankkaus-olio JSON-muotoon
            string json = JsonConvert.SerializeObject(tankkaus);

            // Luodaan uusi StringContent-olio JSON-datalle
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // L�hetet��n PUT-pyynt� REST-rajapintaan
            HttpResponseMessage response = await client.PutAsync($"api/tankkaus/{id}", content);

            // Tarkista vastauksen tila
            if (response.IsSuccessStatusCode)
            {
                // P�ivitys onnistui
                await DisplayAlert("P�ivitys", "Tiedot p�ivitetty onnistuneesti!", "OK");
            }
            else
            {
                // P�ivitys ep�onnistui
                await DisplayAlert("Virhe", "Tietojen p�ivitys ep�onnistui.", "OK");
            }
        }
        catch (Exception e)
        {
            await DisplayAlert("Virhe", e.Message.ToString(), "OK");
        }
    }

    private async void TallennaButton_Clicked(object sender, EventArgs e)
    {
        double ajokilometrit, litraa, euroa;
        if (double.TryParse(ajokilometritKentta.Text, out ajokilometrit) &&
            double.TryParse(litraaKentta.Text, out litraa) &&
            double.TryParse(euroaKentta.Text, out euroa))
        {
            // Kutsu p�ivit� metodia
            await App.Current.MainPage.Navigation.PopAsync(); // Sulje nykyinen sivu
            await UpdateDataToRestAPI(_id, rekisterinumeroKentta.Text, ajokilometrit, litraa, euroa);
        }
        else
        {
            await DisplayAlert("Virhe", "Sy�t� kelvolliset arvot.", "OK");
        }
    }

    private async void TakaisinButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TankkkauksetPage()); // Palaa edelliselle sivulle
    }
}