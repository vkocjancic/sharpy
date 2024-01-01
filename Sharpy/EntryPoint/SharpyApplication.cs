
using Sharpy.Events;
using Sharpy.Logging;

namespace Sharpy.EntryPoint
{
    /// <summary>
    /// Main Sharpy application class. This is all you need to run sharpy engine app.
    /// </summary>
    public class SharpyApplication
    {

        #region Properties

        /// <summary>
        /// Event dispatcher
        /// </summary>
        public EventDispatcher EventDispatcher { get; protected set; }

        #endregion


        #region Constructors

        /// <summary>
        /// Constructors
        /// </summary>
        public SharpyApplication() 
        {
            EventDispatcher = new EventDispatcher();
        }

        #endregion


        #region Public methods

        /// <summary>
        /// Main function with game loop.
        /// </summary>
        public void Run()
        {
            // TODO: remove at next implementation
            KeyPressedEventArgs eKp = new KeyPressedEventArgs() { KeyCode = 101, NumberOfRepeats = 1 };
            WindowResizeEventArgs eWr = new WindowResizeEventArgs() { WindowHeight = 1024, WindowWidth = 768 };

            EventDispatcher.Dispatch(this, eKp);
            EventDispatcher.Dispatch(this, eWr);

            // TODO: add game loop
            while (true) ;
        }

        #endregion

    }
}
