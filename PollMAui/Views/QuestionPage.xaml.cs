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
}