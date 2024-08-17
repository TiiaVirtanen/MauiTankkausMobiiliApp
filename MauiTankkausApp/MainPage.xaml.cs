using System.Collections.ObjectModel;
using System.Text;
using MauiTankkausApp.Models;
using Newtonsoft.Json;

namespace MauiTankkausApp
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            LoadDataFromRestAPI();

            tanklataus.Text = "Ladataan viimeisimpiä tankkauksia...";

        }

        async void LoadDataFromRestAPI()
        {
            try
            {

                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri("https://restapibensa.azurewebsites.net/");
                string json = await client.GetStringAsync("api/tankkaus");

                IEnumerable<Tankkaus> tank = JsonConvert.DeserializeObject<Tankkaus[]>(json);
                // Muuttujan alustaminen
                ObservableCollection<Tankkaus> datat = new ObservableCollection<Tankkaus>();
                datat = new ObservableCollection<Tankkaus>(tank);

                // Asetetaan datat näkyviin xaml tiedostossa olevalle listalle
                tankList.ItemsSource = datat;

                // Tyhjennetään latausilmoitus label
                tanklataus.Text = "";

            }

            catch (Exception e)
            {
                await DisplayAlert("Virhe", e.Message.ToString(), "OK");

            }
        }

        async void SaveDataToRestAPI(string rekisterinumero, double ajokilometrit, double litraa, double euroa)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://restapibensa.azurewebsites.net/");

                // Luodaan uusi olio käyttäjän syöttämillä tiedoilla
                Tankkaus tankkaus = new Tankkaus
                {
                    Rekisterinumero = rekisterinumero,
                    Ajokilometrit = ajokilometrit,
                    Litraa = litraa,
                    Euroa = euroa
                };

                // Muunnetaan Tankkaus-olio JSON-muotoon
                string json = JsonConvert.SerializeObject(tankkaus);

                // Luodaan uusi StringContent-olio JSON-datalle
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Lähetetään POST-pyyntö REST-rajapintaan
                HttpResponseMessage response = await client.PostAsync("api/tankkaus", content);

                // Tarkista vastauksen tila
                if (response.IsSuccessStatusCode)
                {
                    // Tallennus onnistui
                    await DisplayAlert("Tallennus", "Tiedot tallennettu onnistuneesti!", "OK");
                }
                else
                {
                    // Tallennus epäonnistui
                    await DisplayAlert("Virhe", "Tietojen tallennus epäonnistui.", "OK");
                }
            }
            catch (Exception e)
            {
                await DisplayAlert("Virhe", e.Message.ToString(), "OK");
            }
        }


        private void Lisaa_Clicked(object sender, EventArgs e)
        {
            //Kerätään tarvittavat tiedot käyttäjältä
            string rekisteritunnus = inputKentta1.Text;
            double ajokilometrit, litraa, euroa;

            // Muunnetaan syötetty teksti double-muotoon
            if (!double.TryParse(inputKentta2.Text, out ajokilometrit) ||
                !double.TryParse(inputKentta3.Text, out litraa) ||
                !double.TryParse(inputKentta4.Text, out euroa))
            {
                DisplayAlert("Virhe", "Tarkista syötetyt tiedot.", "OK");
                return;
            }

            // Kutsutaan metodia
            SaveDataToRestAPI(rekisteritunnus, ajokilometrit, litraa, euroa);

            inputKentta1.Text = "";
            inputKentta2.Text = "";
            inputKentta3.Text = "";
            inputKentta4.Text = "";
        }

        private async void EtusivuButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        private async void TankkausButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TankkkauksetPage());
        }
    }

}
