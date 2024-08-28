
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KeyboardVisibilityListener
{
    // All the code in this file is included in all platforms.
    public static class KeyBoardUtils
    {



        /**
* Manually toggle soft keyboard visibility
* @param context calling context
*/
        public static void ToggleKeyboardVisibility()
        {
#if ANDROID
            Droid.KeyboardUtils.ToggleKeyboardVisibility(Platform.CurrentActivity);
#endif
        }
        public static void ForceCloseKeyboard()
        {
#if ANDROID
            Droid.KeyboardUtils.ForceCloseKeyboard(Platform.CurrentActivity);
#endif
        }
        /**
         * Force closes the soft keyboard
         * @param activeView the view with the keyboard focus
         */
        public static void ForceCloseKeyboard(View activeView)
        {
#if ANDROID
            if (activeView?.Handler?.PlatformView is Android.Views.View av)
            {
                Droid.KeyboardUtils.ForceCloseKeyboard(av);
            }
#endif
        }
    }
}
