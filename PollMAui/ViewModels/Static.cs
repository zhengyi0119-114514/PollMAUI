using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollMAui.ViewModels
{
    public static class Static
    {
        public static String UserName { get; set; } = String.Empty;
        public static String Password { get; set; } = String.Empty;
        public static Timer? Timer { get; set; }
        private static String _token = String.Empty;
        public static String Token
        {
            get => _token;
            set
            {
                _token = value;
                var _event = Volatile.Read(in OnTokenChanged);
                _event?.Invoke(null, new());
            }
        }
        public static event EventHandler? OnTokenChanged;
    }
}
