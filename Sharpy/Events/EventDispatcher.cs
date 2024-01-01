using Sharpy.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpy.Events
{
    public class EventDispatcher
    {

        #region Events

        public delegate void KeyPressedEventHandler(object sender, KeyPressedEventArgs e);
        public event KeyPressedEventHandler? KeyPressed;

        public delegate void KeyReleasedEventHandler(object sender, KeyReleasedEventArgs e);
        public event KeyReleasedEventHandler? KeyReleased;

        public delegate void MouseButtonPressedEventHandler(object sender, MouseButtonPressedEventArgs e);
        public event MouseButtonPressedEventHandler? MouseButtonPressed;

        public delegate void MouseButtonReleasedEventHandler(object sender, MouseButtonReleasedEventArgs e);
        public event MouseButtonReleasedEventHandler? MouseButtonReleased;

        public delegate void MouseMovedEventHandler(object sender, MouseMovedEventArgs e);
        public event MouseMovedEventHandler? MouseMoved;

        public delegate void MouseScrolledEventHandler(object sender, MouseScrolledEventArgs e);
        public event MouseScrolledEventHandler? MouseScrolled;

        public delegate void WindowResizeEventHandler(object sender, WindowResizeEventArgs e);
        public event WindowResizeEventHandler? WindowResize;

        #endregion


        #region Abstract methods

        public void Dispatch(object sender, EventArgsBase e)
        {
            switch (e)
            {
                case KeyPressedEventArgs args:
                    Log.Debug("Key pressed: {0}", args);
                    KeyPressed?.Invoke(sender, args);
                    break;
                case KeyReleasedEventArgs args:
                    Log.Debug("Key released: {0}", args);
                    KeyReleased?.Invoke(sender, args);
                    break;
                case MouseButtonPressedEventArgs args:
                    Log.Debug("Mouse button pressed: {0}", args);
                    MouseButtonPressed?.Invoke(sender, args);
                    break;
                case MouseButtonReleasedEventArgs args:
                    Log.Debug("Mouse button released: {0}", args);
                    MouseButtonReleased?.Invoke(sender, args);
                    break;
                case MouseMovedEventArgs args:
                    Log.Debug("Mouse moved: {0}", args);
                    MouseMoved?.Invoke(sender, args);
                    break;
                case MouseScrolledEventArgs args:
                    Log.Debug("Mouse scrolled: {0}", args);
                    MouseScrolled?.Invoke(sender, args);
                    break;
                case WindowResizeEventArgs args:
                    Log.Debug("Window resized: {0}", args);
                    WindowResize?.Invoke(sender, args);
                    break;
            }

        }

        #endregion

    }
}
