using System.Threading.Tasks;
using PollMAui.ViewModels;

namespace PollMAui.Views;

public partial class QuestionPage : ContentPage
{
    public QuestionViewModel Question { get; set; }
    public QuestionPage()
    {
        InitializeComponent();
        this.BindingContext = Question = new();

    }

    private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (Static.Token == null)
        {
            await DisplayAlert("Message", "请先登录", "OK");
            return;
        }

        await Question.Question!.SubmitAnswerAsync(Static.Token, ListView.SelectedItem.ToString()!);

    }
}