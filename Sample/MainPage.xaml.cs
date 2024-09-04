using KeyboardVisibilityListener;
using Microsoft.Maui.Platform;
using CommunityToolkit.Maui.Core.Platform;
using System.Diagnostics;


namespace Sample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new MainPageViewModel();
        }
        private async void HideKeyboardAsyncCommunityToolkitClicked(object sender, EventArgs e)
        {
            await CommunityToolkit.Maui.Core.Platform.KeyboardExtensions.HideKeyboardAsync(this.Entry1);
        }
        private async void ShowKeyboardAsyncCommunityToolkitClicked(object sender, EventArgs e)
        {
            await CommunityToolkit.Maui.Core.Platform.KeyboardExtensions.ShowKeyboardAsync(this.Entry1);
        }

        private void ToggleKeyboardVisibilityClicked(object sender, EventArgs e)
        {
            KeyBoardUtils.ToggleKeyboardVisibility();
        }

        private void ForceCloseKeyboardClicked(object sender, EventArgs e)
        {
            KeyBoardUtils.ForceCloseKeyboard(this.Entry1);
        }
    }

    public class MainPageViewModel 
    {
        public KeyboardVisibilityState VisibilityState { get; set; }= KeyboardVisibilityState.Instance;
        public MainPageViewModel()
        {
            KeyboardVisibilityState.VisibilityChanged += KeyboardVisibilityState_VisibilityChanged;
        }

        private void KeyboardVisibilityState_VisibilityChanged(object? sender, KeyboardVisibilityStateChangedEventArgs e)
        {
            Debug.WriteLine("Keyboard visibility has changed");
        }
    }

}
