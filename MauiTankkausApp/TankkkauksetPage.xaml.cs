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
        //LoadDataFromRestAPII();

        //tankklataus.Text = "Ladataan tankkauksia...";
    }

    //async void LoadDataFromRestAPII()
    //{
    //    try
    //    {

    //        HttpClient client = new HttpClient();

    //        client.BaseAddress = new Uri("https://restapibensa24.azurewebsites.net/");
    //        string json = await client.GetStringAsync("api/tankkaus");

    //        IEnumerable<Tankkaus> tank = JsonConvert.DeserializeObject<Tankkaus[]>(json);
    //        // Muuttujan alustaminen
    //        ObservableCollection<Tankkaus> data = new ObservableCollection<Tankkaus>();
    //        data = new ObservableCollection<Tankkaus>(tank);

    //        // Asetetaan datat n�kyviin xaml tiedostossa olevalle listalle
    //        tankkList.ItemsSource = data;

    //        // Tyhjennet��n latausilmoitus label
    //        tankklataus.Text = "";

    //    }

    //    catch (Exception e)
    //    {
    //        await DisplayAlert("Virhe", e.Message.ToString(), "OK");

    //    }
    //}

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
    //            LoadDataFromRestAPII(); // P�ivit� n�kym�
    //        }
    //        else
    //        {
    //            await DisplayAlert("Virhe", "Tankkauksen poisto ep�onnistui.", "OK");
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
    //        // Siirry muokkaussivulle ja siirr� tiedot
    //        await Navigation.PushAsync(new EditTankkauksetPage(
    //            tankkaus.Id,
    //            tankkaus.Rekisterinumero,
    //            tankkaus.Ajokilometrit,
    //            tankkaus.Litraa,
    //            tankkaus.Euroa));
    //    }
    //}

    //private async void EtusivuButton_Clicked(object sender, EventArgs e)
    //{
    //    await Navigation.PushAsync(new MainPage());
    //}

    //private async void PoistaButton_Clicked(object sender, EventArgs e)
    //{
    //    var button = sender as Button;
    //    var tankkaus = button?.BindingContext as Tankkaus;

    //    if (tankkaus != null)
    //    {
    //        bool answer = await DisplayAlert("Poista", "Haluatko varmasti poistaa t�m�n tankkauksen?", "Kyll�", "Ei");

    //        if (answer)
    //        {
    //            await DeleteDataFromRestAPI(tankkaus.Id);
    //        }
    //    }
    //}
}