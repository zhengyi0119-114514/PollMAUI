using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PollMgr;

namespace PollMAui.ViewModels;

public class WrongAnswerListViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private void onPropertyChanged([CallerMemberName] String? name = null)
    {
        var _evant = Volatile.Read(in PropertyChanged);
        _evant?.Invoke(this, new(name));
    }
    private List<WrongAnswer>? wrongAnswers;
    public List<WrongAnswer>? WrongAnswers
    {
        get => wrongAnswers;
        set
        {
            wrongAnswers = value;
            onPropertyChanged();
        }
    }
}
