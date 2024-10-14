using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using MauiTankkausApp.Models;
using Newtonsoft.Json;

namespace MauiTankkausApp;

public partial class TankkkauksetPage : ContentPage
{
    private int ajoneuvoId;
    public TankkkauksetPage(int ajoneuvoId, string rekisterinumero)
    {
        InitializeComponent();
        LoadDataFromRestAPI(ajoneuvoId);

        ReknroLabel.Text = rekisterinumero;
        tanklataus.Text = "Ladataan tietoja...";
    }

    private async Task LoadDataFromRestAPI(int ajoneuvoId)
    {
        try
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://restapibensa24.azurewebsites.net/");

            string requestUrl = $"api/tankkaus/ajoneuvo/{ajoneuvoId}";
            string json = await client.GetStringAsync(requestUrl);

            IEnumerable<Tankkaus> tank = JsonConvert.DeserializeObject<Tankkaus[]>(json);

            // Muuttujan alustaminen
            ObservableCollection<Tankkaus> datat = new ObservableCollection<Tankkaus>(tank);

            // Asetetaan datat n�kyviin XAML-tiedostossa olevalle listalle
            tankList.ItemsSource = datat;

            // Tyhjennet��n latausilmoitus label
            tanklataus.Text = "";

        }

        catch (Exception e)
        {
            await DisplayAlert("Virhe", e.Message.ToString(), "OK");

        }
    }

    private async Task DeleteDataFromRestAPI(int id)
    {
        try
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://restapibensa24.azurewebsites.net/");

            HttpResponseMessage response = await client.DeleteAsync($"api/tankkaus/{id}");

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Poistettu", "Tankkaus poistettiin onnistuneesti!", "OK");
                //await LoadDataFromRestAPI(ajoneuvoId); // P�ivit� n�kym�
            }
            else
            {
                await DisplayAlert("Virhe", "Tankkauksen poisto ep�onnistui.", "OK");
            }
        }
        catch (Exception e)
        {
            await DisplayAlert("Virhe", e.Message.ToString(), "OK");
        }
    }

    //private async void MuokkaaButton_Clicked(object sender, EventArgs e)
    //{
    //    // Hae ListView:n valittu item
    //    var button = sender as Button;
    //    var tankkaus = button?.BindingContext as Tankkaus;

    //    if (tankkaus != null)
    //    {
    //        // Siirry muokkaussivulle ja siirr� tiedot
    //        await Navigation.PushAsync(new EditTankkauksetPage(
    //            tankkaus.Id,
    //            tankkaus.Rekisterinumero,
    //            tankkaus.Ajokilometrit,
    //            tankkaus.Litraa,
    //            tankkaus.Euroa));
    //    }
    //}

    private async void PoistaButton_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var tankkaus = button?.BindingContext as Tankkaus;

        if (tankkaus != null)
        {
            bool answer = await DisplayAlert("Poista", "Haluatko varmasti poistaa t�m�n tankkauksen?", "Kyll�", "Ei");

            if (answer)
            {
                await DeleteDataFromRestAPI(tankkaus.TankkausId);
            }
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