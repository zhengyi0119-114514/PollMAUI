using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PollMgr;

namespace PollMAui.ViewModels;

public class QuestionViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private void onPropertyChanged([CallerMemberName]String? name =null)
    {
        var _evant = Volatile.Read(in PropertyChanged);
        _evant?.Invoke(this,new(name));
    }
    private Question? m_question;
    public Question? Question
    {
        get => m_question;
        set
        {
            m_question=value;
            onPropertyChanged();
        }
    }
    private String? m_SelectedItem;
    public String? SelectedItem
    {
        get => SelectedItem;
        set
        {
            m_SelectedItem =value;
            onPropertyChanged();
        }
    }
}
