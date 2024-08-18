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

            // Luodaan uusi olio käyttäjän syöttämillä tiedoilla
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

            // Lähetetään PUT-pyyntö REST-rajapintaan
            HttpResponseMessage response = await client.PutAsync($"api/tankkaus/{id}", content);

            // Tarkista vastauksen tila
            if (response.IsSuccessStatusCode)
            {
                // Päivitys onnistui
                await DisplayAlert("Päivitys", "Tiedot päivitetty onnistuneesti!", "OK");
            }
            else
            {
                // Päivitys epäonnistui
                await DisplayAlert("Virhe", "Tietojen päivitys epäonnistui.", "OK");
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
            // Kutsu päivitä metodia
            await App.Current.MainPage.Navigation.PopAsync(); // Sulje nykyinen sivu
            await UpdateDataToRestAPI(_id, rekisterinumeroKentta.Text, ajokilometrit, litraa, euroa);
        }
        else
        {
            await DisplayAlert("Virhe", "Syötä kelvolliset arvot.", "OK");
        }
    }

    private async void TakaisinButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TankkkauksetPage()); // Palaa edelliselle sivulle
    }
}