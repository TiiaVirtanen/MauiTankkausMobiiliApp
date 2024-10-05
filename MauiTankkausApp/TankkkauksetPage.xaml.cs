using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using MauiTankkausApp.Models;
using Newtonsoft.Json;

namespace MauiTankkausApp;

public partial class TankkkauksetPage : ContentPage
{
    public TankkkauksetPage()
    {
        InitializeComponent();
        LoadDataFromRestAPI();

        rekPicker.SelectedIndexChanged += RekPicker_SelectedIndexChanged;
        tanklataus.Text = "Ladataan tietoja...";
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

            // Asetetaan Picker komponentin tietol‰hde ja DisplayMemberPath
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

            // Asetetaan datat n‰kyviin XAML-tiedostossa olevalle listalle
            tankList.ItemsSource = datat;

            // Tyhjennet‰‰n latausilmoitus label
            tanklataus.Text = "";

        }

        catch (Exception e)
        {
            await DisplayAlert("Virhe", e.Message.ToString(), "OK");

        }
    }
    private async void RekPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selectedAjoneuvo = (Ajoneuvot)rekPicker.SelectedItem;

        if (selectedAjoneuvo != null)
        {
            // N‰yt‰ valitun ajoneuvon tiedot
            Reknro.Text = selectedAjoneuvo.Rekisterinumero;
            Tiedot.IsVisible = true;

            // Lataa tankkaustiedot
            await LoadDataFromRestAPI2(selectedAjoneuvo);
        }
    }

    //private async Task DeleteDataFromRestAPI(int id)
    //{
    //    try
    //    {
    //        HttpClient client = new HttpClient();
    //        client.BaseAddress = new Uri("https://restapibensa24.azurewebsites.net/");

    //        HttpResponseMessage response = await client.DeleteAsync($"api/tankkaus/{id}");

    //        if (response.IsSuccessStatusCode)
    //        {
    //            await DisplayAlert("Poistettu", "Tankkaus poistettiin onnistuneesti!", "OK");
    //            LoadDataFromRestAPI(); // P‰ivit‰ n‰kym‰
    //        }
    //        else
    //        {
    //            await DisplayAlert("Virhe", "Tankkauksen poisto ep‰onnistui.", "OK");
    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        await DisplayAlert("Virhe", e.Message.ToString(), "OK");
    //    }
    //}

    //private async void MuokkaaButton_Clicked(object sender, EventArgs e)
    //{
    //    // Hae ListView:n valittu item
    //    var button = sender as Button;
    //    var tankkaus = button?.BindingContext as Tankkaus;

    //    if (tankkaus != null)
    //    {
    //        // Siirry muokkaussivulle ja siirr‰ tiedot
    //        await Navigation.PushAsync(new EditTankkauksetPage(
    //            tankkaus.Id,
    //            tankkaus.Rekisterinumero,
    //            tankkaus.Ajokilometrit,
    //            tankkaus.Litraa,
    //            tankkaus.Euroa));
    //    }
    //}

    private async void EtusivuButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }

    //private async void PoistaButton_Clicked(object sender, EventArgs e)
    //{
    //    var button = sender as Button;
    //    var tankkaus = button?.BindingContext as Tankkaus;

    //    if (tankkaus != null)
    //    {
    //        bool answer = await DisplayAlert("Poista", "Haluatko varmasti poistaa t‰m‰n tankkauksen?", "Kyll‰", "Ei");

    //        if (answer)
    //        {
    //            await DeleteDataFromRestAPI(tankkaus.Id);
    //        }
    //    }
    //}
}