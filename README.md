# KeyboardVisibilityListener-MAUI
 
⚠️ Currently only supports Android 


https://github.com/user-attachments/assets/ba49f287-1f7c-4fdc-910a-c7f538e22cdf


## Getting started

MauiProgram.cs
```
using KeyboardVisibilityListener;

var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>()
        .UseKeyboardVisibilityListener() //Add this line
    //Etc ...

```


## Toogle keyboard
```
 private void ToggleKeyboardVisibilityClicked(object sender, EventArgs e)
 {
     KeyBoardUtils.ToggleKeyboardVisibility();
 }

 private void ForceCloseKeyboardClicked(object sender, EventArgs e)
 {
     KeyBoardUtils.ForceCloseKeyboard(this.Entry1);
 }
```

## Watch keyboard visibility
```
KeyboardVisibilityState VisibilityState = KeyboardVisibilityState.Instance;

//IsKeyboardOpen property also notifies via PropertyChanged 
//KeyboardVisibilityState.Instance.IsKeyboardOpen 



KeyboardVisibilityState.VisibilityChanged += KeyboardVisibilityState_VisibilityChanged;


private void KeyboardVisibilityState_VisibilityChanged(object? sender, KeyboardVisibilityStateChangedEventArgs e)
{
    Debug.WriteLine("Keyboard visibility has changed");
}

 ```
