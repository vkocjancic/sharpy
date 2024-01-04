
using Sharpy.Events;
using Sharpy.Logging;
using Sharpy.Window;

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
            var options = new WindowOptions()
            {
                m_bVsyncEnabled = true,
                m_sTitle = "Sharpy application",
                m_unHeight = 600,
                m_unWidth = 800
            };
            var window = new WindowsWindow(options, EventDispatcher);
            window.Run();
        }

        #endregion

    }
}
