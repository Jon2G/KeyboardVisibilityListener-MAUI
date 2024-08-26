
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardVisibilityListener
{
    public class KeyboardVisibilityStateChangedEventArgs(bool oldValue, bool currentValue) : EventArgs
    {
        public readonly bool OldValue = oldValue;
        public readonly bool CurrentValue = currentValue;
    }
    public class KeyboardVisibilityState : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public static event EventHandler<KeyboardVisibilityStateChangedEventArgs>? VisibilityChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private bool _IsKeyboardOpen;
        public bool IsKeyboardOpen
        {
            get => _IsKeyboardOpen; internal set
            {
                if (value != _IsKeyboardOpen)
                {
                    VisibilityChanged?.Invoke(this, new KeyboardVisibilityStateChangedEventArgs(_IsKeyboardOpen, value));
                }
                _IsKeyboardOpen = value;
                OnPropertyChanged();
            }
        }
#if ANDROID
        public void SetActivity(Android.App.Activity activity)
        {
            Droid.KeyboardUtils.AddKeyboardToggleListener(activity, new Droid.KeyboardToogleListener());
        }
#endif
        private KeyboardVisibilityState()
        {
        }
        ~KeyboardVisibilityState()
        {
#if ANDROID
            Droid.KeyboardUtils.RemoveAllKeyboardToggleListeners();
#endif
        }
        private static KeyboardVisibilityState? _Instance = null;
        public static KeyboardVisibilityState Instance
        {
            get => _Instance ??= new KeyboardVisibilityState();
        }

    }
}
