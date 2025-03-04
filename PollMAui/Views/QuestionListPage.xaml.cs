using System.Threading.Tasks;
using PollMAui.ViewModels;
using PollMgr;

namespace PollMAui.Views;

public partial class QuestionListPage : ContentPage
{
    public QuestionListViewModel ViewModel { get; set; }
    public QuestionListPage()
    {
        InitializeComponent();
        this.BindingContext = ViewModel = new();
    }

    private async void GetButton_Clicked(object sender, EventArgs e)
    {
        ViewModel.Questions = await Question.GetQuestionsAsync();
    }

    private async void QuestionListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        QuestionPage page = new();
        page.Question.Question = (PollMgr.Question)QuestionListView.SelectedItem;
        await Navigation.PushAsync(page);
    }
}