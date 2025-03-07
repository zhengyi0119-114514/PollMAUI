using PollMAui.ViewModels;
using PollMgr;

namespace PollMAui.Views;

public partial class WrongListPage : ContentPage
{
    public WrongAnswerListViewModel ViewModel { get; set; }
    public WrongListPage()
    {
        InitializeComponent();
        this.BindingContext = ViewModel = new();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        if (Static.Token == null)
        {
            await DisplayAlert("Message", "请先登录", "OK");
            return;
        }
        this.ViewModel.WrongAnswers = await WrongAnswer.GetWrongAnswer(Static.Token);
    }
}