using System.ComponentModel;
using System.Runtime.CompilerServices;
using PollMgr;

namespace PollMAui.ViewModels;

public class QuestionListViewModel : INotifyPropertyChanged
{
    public QuestionListViewModel()
    {
    }
    public event PropertyChangedEventHandler? PropertyChanged; 
    private void onPropertyChanged([CallerMemberName] String? name = null)
    {
        var _evant = Volatile.Read(in PropertyChanged);
        _evant?.Invoke(this, new(name));
    }
    private Question[] m_questions = [];
    public Question[] Questions
    {
        get=>m_questions;
        set
        {
            m_questions =value;
            onPropertyChanged();
        }
    }
}
