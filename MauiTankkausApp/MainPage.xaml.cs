using System.Collections.ObjectModel;
using System.Text;
using MauiTankkausApp.Models;
using Newtonsoft.Json;

namespace MauiTankkausApp
{
    public partial class MainPage : ContentPage
    {
        private int _ajoneuvoId;

        public MainPage()
        {
            InitializeComponent();
            LoadDataFromRestAPI();


        rekPicker.SelectedIndexChanged += RekPicker;
        tanklataus.Text = "Ladataan viimeisimpiä tankkauksia...";
        }

        private async void LoadDataFromRestAPI()
        {
            try
            {

                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri("https://restapibensa24.azurewebsites.net/");
                string json = await client.GetStringAsync("api/ajoneuvot");

                IEnumerable<Ajoneuvot> ajon = JsonConvert.DeserializeObject<Ajoneuvot[]>(json);
                // Muuttujan alustaminen
                ObservableCollection<Ajoneuvot> data = new ObservableCollection<Ajoneuvot>();
                data = new ObservableCollection<Ajoneuvot>(ajon);

                // Asetetaan Picker komponenton tietolähde ja DisplayMemberPath
                rekPicker.ItemsSource = data;
                rekPicker.ItemDisplayBinding = new Binding("Rekisterinumero");

            }

            catch (Exception e)
            {
                await DisplayAlert("Virhe", e.Message.ToString(), "OK");

            }
        }

        private async Task LoadDataFromRestAPI2(Ajoneuvot ajoneuvo)
        {
            try
            {

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://restapibensa24.azurewebsites.net/");

                string requestUrl = $"api/tankkaus/ajoneuvo/{ajoneuvo.AjoneuvoId}";
                string json = await client.GetStringAsync(requestUrl);

                IEnumerable<Tankkaus> tank = JsonConvert.DeserializeObject<Tankkaus[]>(json);

                // Muuttujan alustaminen
                ObservableCollection<Tankkaus> datat = new ObservableCollection<Tankkaus>(tank);

                // Asetetaan datat näkyviin XAML-tiedostossa olevalle listalle
                tankList.ItemsSource = datat;

                // Tyhjennetään latausilmoitus label
                tanklataus.Text = "";

            }

            catch (Exception e)
            {
                await DisplayAlert("Virhe", e.Message.ToString(), "OK");

            }
        }
        private async void RekPicker(object sender, EventArgs e)
        {
            var selectedAjoneuvo = (Ajoneuvot)rekPicker.SelectedItem;

            if (selectedAjoneuvo != null)
            {
                // Näytä valitun ajoneuvon tiedot
                Reknro.Text = selectedAjoneuvo.Rekisterinumero;
                _ajoneuvoId = selectedAjoneuvo.AjoneuvoId;
                Tiedot.IsVisible = true;

                // Lataa tankkaustiedot
                await LoadDataFromRestAPI2(selectedAjoneuvo);
            }
        }



        private async void EtusivuButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        private async void TankkausButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TankkkauksetPage());
        }

        private async void LisaaButton_Clicked(object sender, EventArgs e)
        {
            var selectedAjoneuvo = (Ajoneuvot)rekPicker.SelectedItem;

            if (selectedAjoneuvo != null)
            {
                await Navigation.PushAsync(new LisaaTankkausPage(selectedAjoneuvo.AjoneuvoId, selectedAjoneuvo.Rekisterinumero));
            }
            else
            {
                await DisplayAlert("Huomio","Valitse rekisterinumero ensin!","OK");
            }
        }

        private async void LisaaRek_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LisaaRekisteriNroPage());
        }
    }

}
