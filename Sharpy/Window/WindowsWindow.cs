using Sharpy.Events;
using Sharpy.Logging;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.SDL;
using Silk.NET.Windowing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Sharpy.Window
{

    /// <summary>
    /// Currently main window to use on Windows platform
    /// </summary>
    public class WindowsWindow : WindowBase
    {

        #region Events

        public event Action<double>? Render;

        public event Action<double>? Update;

        #endregion


        #region Fields

        /// <summary>
        /// Silk.NET window instance
        /// </summary>
        private IWindow m_windowSilk;

        #endregion


        #region Constructors

        /// <summary>
        /// Constructs window object
        /// </summary>
        /// <param name="t_optWindow">Window options to apply to window</param>
        /// <param name="t_evtDispatcher">Event dispatcher to use for handling events</param>
        public WindowsWindow(WindowOptions t_optWindow, EventDispatcher t_evtDispatcher) : base(t_optWindow, t_evtDispatcher)
        {
            var options = Silk.NET.Windowing.WindowOptions.Default;
            options.Size = new Vector2D<int>((int)m_optWindow.m_unWidth, (int)m_optWindow.m_unHeight);
            options.Title = m_optWindow.m_sTitle;
            options.VSync = m_optWindow.m_bVsyncEnabled;
            m_windowSilk = Silk.NET.Windowing.Window.Create(options);
            m_windowSilk.Closing += OnSilkWindowClosing;
            m_windowSilk.Load += OnSilkWindowLoad;
            m_windowSilk.Resize += OnSilkWindowResize;
            m_windowSilk.Render += OnSilkWindowRender;
            m_windowSilk.Update += OnSilkWindowUpdate;
        }

        #endregion

        #region WindowBase implementation

        /// <summary>
        /// Runs window with game loop
        /// </summary>
        public override void Run()
        {
            m_windowSilk.Run();
        }

        #endregion


        #region Silk window event handlers

        /// <summary>
        /// OnClosing event handler for Silk.NET window
        /// </summary>
        private void OnSilkWindowClosing()
        {
            m_evtDispatcher.Dispatch(this, new WindowClosingEventArgs());
        }

        /// <summary>
        /// OnLoad event handler for Silk.NET window
        /// </summary>
        /// <remarks>
        /// Initializes all event handlers for input devices
        /// </remarks>
        private void OnSilkWindowLoad()
        {
            IInputContext input = m_windowSilk.CreateInput();
            foreach(IKeyboard keyboard in input.Keyboards)
            {
                keyboard.KeyDown += (IKeyboard t_keyboard, Key t_key, int t_nkeyCode) => 
                {
                    m_evtDispatcher.Dispatch(this, new KeyPressedEventArgs() { KeyCode = t_nkeyCode, NumberOfRepeats = 1 });
                };
                keyboard.KeyUp += (IKeyboard t_keyboard, Key t_key, int t_nkeyCode) =>
                {
                    m_evtDispatcher.Dispatch(this, new KeyReleasedEventArgs() { KeyCode = t_nkeyCode });
                };
            }

            foreach (IMouse mouse in input.Mice)
            {
                mouse.MouseDown += (IMouse t_mouse, MouseButton t_btnMouse) =>
                {
                    m_evtDispatcher.Dispatch(this, new MouseButtonPressedEventArgs() { ButtonCode = (int)t_btnMouse });
                };
                mouse.MouseUp += (IMouse t_mouse, MouseButton t_btnMouse) =>
                {
                    m_evtDispatcher.Dispatch(this, new MouseButtonReleasedEventArgs() { ButtonCode = (int)t_btnMouse });
                };
                mouse.MouseMove += (IMouse t_mouse, Vector2 t_vecPos) =>
                {
                    m_evtDispatcher.Dispatch(this, new MouseMovedEventArgs() { PositionX = Convert.ToDecimal(t_vecPos.X), PositionY = Convert.ToDecimal(t_vecPos.Y) });
                };
                mouse.Scroll += (IMouse t_mouse, ScrollWheel t_wheelScroll) =>
                {
                    m_evtDispatcher.Dispatch(this, new MouseScrolledEventArgs() { OffsetX = Convert.ToDecimal(t_wheelScroll.X), OffsetY = Convert.ToDecimal(t_wheelScroll.Y) });
                };
            }
        }

        /// <summary>
        /// OnRender event handler for Silk.NET window
        /// </summary>
        /// <param name="t_fElapsedTime">Elapsed time in ms</param>
        private void OnSilkWindowRender(double t_fElapsedTime)
        {
            Render?.Invoke(t_fElapsedTime);
        }

        /// <summary>
        /// OnResize event handler for Silk.NET window
        /// </summary>
        /// <param name="t_vec2dSize">Window size</param>
        private void OnSilkWindowResize(Vector2D<int> t_vec2dSize)
        {
            m_evtDispatcher.Dispatch(this, new WindowResizeEventArgs() { WindowHeight = t_vec2dSize.Y, WindowWidth = t_vec2dSize.X });
        }

        /// <summary>
        /// OnRender event handler for Silk.NET window
        /// </summary>
        /// <param name="t_fElapsedTime">Elapsed time in ms</param>
        private void OnSilkWindowUpdate(double t_fElapsedTime)
        {
            Update?.Invoke(t_fElapsedTime);
        }

        #endregion

    }
}
