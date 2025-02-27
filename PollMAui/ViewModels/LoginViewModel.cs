using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PollMAui.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {
        String m_Name =String.Empty, m_Password = String.Empty;
        public String Name
        {
            get => m_Name;
            set
            {
                m_Name = value;
                _OnPropertyChanged();
            }
        }
        public String Password
        {
            get => m_Password;
            set
            {
                m_Password = value;
            }
        }
        private void _OnPropertyChanged([CallerMemberName] String? name = null)
        {
            var _event = Volatile.Read(in PropertyChanged);
            _event?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
