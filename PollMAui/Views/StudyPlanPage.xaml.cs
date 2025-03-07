using System.Threading.Tasks;
using PollMAui.ViewModels;
using PollMgr;

namespace PollMAui.Views;

public partial class StudyPlanPage : ContentPage
{
    public StudyPlanPage()
    {
        InitializeComponent();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        if(Static.Token == null)
        {
            await DisplayAlert("Message", "请先登录", "OK");
            return;
        }
        this.StudyPlanEditor.Text = (await StudyPlan.GenerateStudyPlan(Static.Token)).Text;
    }
}