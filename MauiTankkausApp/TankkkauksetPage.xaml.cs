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
        LoadDataFromRestAPII();

        tankklataus.Text = "Ladataan tankkauksia...";
    }

    async void LoadDataFromRestAPII()
    {
        try
        {

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://restapibensa.azurewebsites.net/");
            string json = await client.GetStringAsync("api/tankkaus");

            IEnumerable<Tankkaus> tank = JsonConvert.DeserializeObject<Tankkaus[]>(json);
            // Muuttujan alustaminen
            ObservableCollection<Tankkaus> data = new ObservableCollection<Tankkaus>();
            data = new ObservableCollection<Tankkaus>(tank);

            // Asetetaan datat n‰kyviin xaml tiedostossa olevalle listalle
            tankkList.ItemsSource = data;

            // Tyhjennet‰‰n latausilmoitus label
            tankklataus.Text = "";

        }

        catch (Exception e)
        {
            await DisplayAlert("Virhe", e.Message.ToString(), "OK");

        }
    }

    private async void EtusivuButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
}