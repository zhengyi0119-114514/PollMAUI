using PollMAui.ViewModels;
using PollMgr;
using System.Threading.Tasks;
namespace PollMAui.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
    }

    private async void Login_(object sender, EventArgs e)
    {
        try
        {
            String userName = NameEntry.Text;
            String password = PasswordEntry.Text;
            var token = await Account.LoginAsync(userName, password);
            Static.Token = token;
            await DisplayAlert("Message", "登录成功", "OK");
            await Navigation.PopAsync();

        }
        catch (AccountException ex)
        {
            await DisplayAlert("ERROR", ex.Message, "OK");
        }
    }

    private async void Register_(object sender, EventArgs e)
    {
        try
        {
            String userName = NameEntry.Text;
            String password = PasswordEntry.Text;
            var token = await Account.RegisterAsync(userName, password);
            Static.Token = token;
            await Navigation.PopAsync();
        }
        catch (AccountException ex)
        {
            await DisplayAlert("ERROR", ex.Message, "OK");
        }
    }
}