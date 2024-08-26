
using Android.App;
using Android.Content;
using Android.Views;
using Android.Views.InputMethods;
using View = Android.Views.View;
/**
 * Based on:
 * Stackoverflow answer: http://stackoverflow.com/questions/2150078/how-to-check-visibility-of-software-keyboard-in-android
 * Github: https://github.com/ravindu1024/android-keyboardlistener
 */
namespace KeyboardVisibilityListener.Droid
{
    internal class KeyboardToogleListener : ISoftKeyboardToggleListener
    {
        public void OnToggleSoftKeyboard(bool isVisible)
        {
            KeyboardVisibilityState.Instance.IsKeyboardOpen = isVisible;
        }
        internal KeyboardToogleListener()
        {

        }
    }

    internal class KeyboardUtils : Java.Lang.Object, ViewTreeObserver.IOnGlobalLayoutListener
    {
        private const int MAGIC_NUMBER = 200;

        private ISoftKeyboardToggleListener? mCallback;
        private readonly View? mRootView;

        private readonly float? mScreenDensity;
        private static Dictionary<ISoftKeyboardToggleListener, KeyboardUtils> sListenerMap = new();

        public void OnGlobalLayout()
        {
            if (mRootView is null)
            {
                return;
            }
            Android.Graphics.Rect r = new ();
            mRootView.GetWindowVisibleDisplayFrame(r);

            var heightDiff = mRootView.RootView?.Height - (r.Bottom - r.Top);

            var dp = heightDiff / mScreenDensity;
            bool isVisible = dp > MAGIC_NUMBER;

            KeyboardVisibilityState.Instance.IsKeyboardOpen = isVisible;
            if (mCallback != null)
            {
                mCallback.OnToggleSoftKeyboard(isVisible);
            }
        }

        //    /**
        //     * Add a new keyboard listener
        //     * @param act calling activity
        //     * @param listener callback
        //     */
        public static void AddKeyboardToggleListener(
            Activity act,
            ISoftKeyboardToggleListener listener
        )
        {
            RemoveKeyboardToggleListener(listener);

            sListenerMap.Add(listener, new KeyboardUtils(act, listener));
        }

        /**
         * Remove a registered listener
         * @param listener {@link SoftKeyboardToggleListener}
         */
        public static void RemoveKeyboardToggleListener(ISoftKeyboardToggleListener listener)
        {
            if (sListenerMap.TryGetValue(listener, out KeyboardUtils? value))
            {
                KeyboardUtils k = value;
                k.RemoveListener();

                sListenerMap.Remove(listener);
            }
        }

        /**
         * Remove all registered keyboard listeners
         */
        public static void RemoveAllKeyboardToggleListeners()
        {
            foreach (ISoftKeyboardToggleListener l in sListenerMap.Keys)
            {
                RemoveKeyboardToggleListener(l);
            }

            sListenerMap.Clear();
        }

        //    /**
        //     * Manually toggle soft keyboard visibility
        //     * @param context calling context
        //     */
        public static void ToggleKeyboardVisibility(Context? context)
        {
            if (context is null)
            {
                return;
            }
            if (context.GetSystemService(Context.InputMethodService) is InputMethodManager inputMethodManager)
            {
                inputMethodManager.ToggleSoftInput(ShowFlags.Forced, 0);
            }
        }

        //    /**
        //     * Force closes the soft keyboard
        //     * @param activeView the view with the keyboard focus
        //     */
        public static void ForceCloseKeyboard(View? activeView)
        {
            if (activeView is null)
            {
                return;
            }
            if (activeView?.Context?.GetSystemService(Context.InputMethodService) is InputMethodManager inputMethodManager)
            {
                inputMethodManager.HideSoftInputFromWindow(activeView.WindowToken, 0);
            }
        }

        private void RemoveListener()
        {
            mCallback = null;
            if (mRootView?.ViewTreeObserver is not null)
            {
                mRootView.ViewTreeObserver.RemoveOnGlobalLayoutListener(this);
            }
        }

        private KeyboardUtils(Activity? activity, ISoftKeyboardToggleListener listener)
        {
            if (activity is null)
            {
                throw new ArgumentNullException(nameof(activity), "Activity cannot be null");
            }
            mCallback = listener;

            var contentView = activity.FindViewById(Android.Resource.Id.Content);
            if (contentView is ViewGroup group)
            {
                mRootView = group.GetChildAt(0);
            }
            if (mRootView?.ViewTreeObserver is not null)
            {
                mRootView.ViewTreeObserver.AddOnGlobalLayoutListener(this);
            }

            mScreenDensity = activity.Resources?.DisplayMetrics?.Density;
        }
    }
}
