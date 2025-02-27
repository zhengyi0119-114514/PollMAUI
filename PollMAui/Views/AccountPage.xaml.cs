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
}