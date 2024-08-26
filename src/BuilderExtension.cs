using Microsoft.Maui.LifecycleEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardVisibilityListener
{
    public static class BuilderExtension
    {
        public static MauiAppBuilder UseKeyboardVisibilityListener(this MauiAppBuilder builder)
        {
            builder.ConfigureLifecycleEvents(events =>
            {
#if ANDROID
                events.AddAndroid(android => android
                    .OnStart((activity) =>
                    {
                        KeyboardVisibilityState.Instance.SetActivity(activity);
                    })
                    .OnStop((activity) =>
                    {
                        Droid.KeyboardUtils.RemoveAllKeyboardToggleListeners();
                    }));
#endif
            });
            return builder;
        }
    }
}
