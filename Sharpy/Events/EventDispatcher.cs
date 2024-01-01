using Sharpy.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpy.Events
{
    /// <summary>
    /// Class used to dispatch all in-game IO events
    /// </summary>
    public class EventDispatcher
    {

        #region Events

        /// <summary>
        /// Handle key pressed event delegate
        /// </summary>
        /// <param name="sender">Object that triggered this event</param>
        /// <param name="e">Event arguments</param>
        public delegate void KeyPressedEventHandler(object sender, KeyPressedEventArgs e);
        /// <summary>
        /// Key pressed event
        /// </summary>
        public event KeyPressedEventHandler? KeyPressed;

        /// <summary>
        /// Handle key released event delegate
        /// </summary>
        /// <param name="sender">Object that triggered this event</param>
        /// <param name="e">Event arguments</param>
        public delegate void KeyReleasedEventHandler(object sender, KeyReleasedEventArgs e);
        /// <summary>
        /// Key released event
        /// </summary>
        public event KeyReleasedEventHandler? KeyReleased;

        /// <summary>
        /// Handle mouse button pressed event delegate
        /// </summary>
        /// <param name="sender">Object that triggered this event</param>
        /// <param name="e">Event arguments</param>
        public delegate void MouseButtonPressedEventHandler(object sender, MouseButtonPressedEventArgs e);
        /// <summary>
        /// Mouse button pressed event
        /// </summary>
        public event MouseButtonPressedEventHandler? MouseButtonPressed;

        /// <summary>
        /// Handle mouse button released event delegate
        /// </summary>
        /// <param name="sender">Object that triggered this event</param>
        /// <param name="e">Event arguments</param>
        public delegate void MouseButtonReleasedEventHandler(object sender, MouseButtonReleasedEventArgs e);
        /// <summary>
        /// Mouse button released event
        /// </summary>
        public event MouseButtonReleasedEventHandler? MouseButtonReleased;

        /// <summary>
        /// Handle mouse moved event delegate
        /// </summary>
        /// <param name="sender">Object that triggered this event</param>
        /// <param name="e">Event arguments</param>
        public delegate void MouseMovedEventHandler(object sender, MouseMovedEventArgs e);
        /// <summary>
        /// Mouse moved event
        /// </summary>
        public event MouseMovedEventHandler? MouseMoved;

        /// <summary>
        /// Handle mouse scrolled event delegate
        /// </summary>
        /// <param name="sender">Object that triggered this event</param>
        /// <param name="e">Event arguments</param>
        public delegate void MouseScrolledEventHandler(object sender, MouseScrolledEventArgs e);
        /// <summary>
        /// Mouse scrolled event
        /// </summary>
        public event MouseScrolledEventHandler? MouseScrolled;

        /// <summary>
        /// Handle window resize event delegate
        /// </summary>
        /// <param name="sender">Object that triggered this event</param>
        /// <param name="e">Event arguments</param>
        public delegate void WindowResizeEventHandler(object sender, WindowResizeEventArgs e);
        /// <summary>
        /// Window resize event
        /// </summary>
        public event WindowResizeEventHandler? WindowResize;

        #endregion


        #region Abstract methods

        /// <summary>
        /// Dispatch event
        /// </summary>
        /// <param name="sender">Object that triggered event</param>
        /// <param name="e">Event arguments</param>
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
