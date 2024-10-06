namespace MauiTankkausApp;

public partial class LisaaTankkausPage : ContentPage
{
	public LisaaTankkausPage()
	{
		InitializeComponent();
	}

    private async void EtusivuButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
}