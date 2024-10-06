namespace MauiTankkausApp;

public partial class LisaaRekisteriNroPage : ContentPage
{
	public LisaaRekisteriNroPage()
	{
		InitializeComponent();
	}

    private async void EtusivuButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
}