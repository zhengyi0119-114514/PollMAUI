using PollMgr;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PollMAui.ViewModels
{
    public class AccountViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void _onPropertyChanged([CallerMemberName]String? name = null)
        {
            var _event = Volatile.Read(ref PropertyChanged);
            _event?.Invoke(this, new(name));
        }
        private AccountInfo _accountInfo = new("NULL");
        public AccountInfo AccountInfo
        {
            get => _accountInfo;
            set
            {
                _accountInfo = value;
                _onPropertyChanged();
            }
        }
    }
}
