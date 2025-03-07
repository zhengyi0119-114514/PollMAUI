using System.Threading.Tasks;
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
            if (Static.Token != null)
            {
                AccountViewModel.AccountInfo = await Account.GetAccountInfoAsync(Static.Token);
            }
        };
    }

    private async void LoginOrRegisterButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginPage() { });
        if (Static.Token != null)
        {
            LoginOrRegisterButton.IsEnabled = false;
            ExitLoginButton.IsEnabled = true;
        }
    }

    private void ExitLoginButton_Clicked(object sender, EventArgs e)
    {
        Static.Token = null;
        {
            LoginOrRegisterButton.IsEnabled = true;
            ExitLoginButton.IsEnabled = false;
        }
    }

    private async void StudyPlanButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new StudyPlanPage());
    }
}