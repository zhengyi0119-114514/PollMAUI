using PollMAui.ViewModels;
using PollMgr;

namespace PollMAui.Views;

public partial class AccountPage : ContentPage
{
	public AccountViewModel AccountViewModel { get; set; }
	public AccountPage()
	{
		InitializeComponent();
		this.BindingContext = this.AccountViewModel = new();
		Static.OnTokenChanged += async (s, e) =>
		{
			AccountViewModel.AccountInfo = await Account.GetAccountInfoAsync(Static.Token);
		};
	}

    private async void LoginOrRegisterButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginPage() { });
		LoginOrRegisterButton.IsEnabled = false;
		ExitLoginButton.IsEnabled = true;
    }

    private void ExitLoginButton_Clicked(object sender, EventArgs e)
    {
        LoginOrRegisterButton.IsEnabled = true;
        ExitLoginButton.IsEnabled = false;
    }
}