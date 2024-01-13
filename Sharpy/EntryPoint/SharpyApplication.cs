
using Sharpy.Events;
using Sharpy.Layers;
using Sharpy.Logging;
using Sharpy.Window;

namespace Sharpy.EntryPoint
{
    /// <summary>
    /// Main Sharpy application class. This is all you need to run sharpy engine app.
    /// </summary>
    public class SharpyApplication
    {

        #region Fields

        /// <summary>
        /// Event dispatcher
        /// </summary>
        protected EventDispatcher m_evtDispatcher;

        /// <summary>
        /// Layer stack
        /// </summary>
        public LayerStack m_stackLayers;

        #endregion


        #region Constructors

        /// <summary>
        /// Constructors
        /// </summary>
        public SharpyApplication() 
        {
            m_stackLayers = new LayerStack();

            m_evtDispatcher = new EventDispatcher();
            m_evtDispatcher.Event += OnEventDispatcherEvent;
        }

        #endregion


        #region Public members

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
            var window = new WindowsWindow(options, m_evtDispatcher);
            window.Render += OnWindowRender;
            window.Update += OnWindowUpdate;
            window.Run();
        }

        #endregion


        #region EventDispatcher events

        /// <summary>
        /// Handles event from EventDispatcher
        /// </summary>
        /// <param name="t_oSender">Event owner</param>
        /// <param name="t_evtArgs">Event details</param>
        private void OnEventDispatcherEvent(object t_oSender, EventArgsBase t_evtArgs)
        {
            Log.Debug("{0}", t_evtArgs);

            if (t_evtArgs is WindowInitEventArgs)
            {
                for (int i = 0; i < m_stackLayers.Count(); i++)
                {
                    m_stackLayers[i].OnInit((WindowBase)t_oSender);
                }
                return;
            }

            bool bIsWindowClosingEvent = t_evtArgs is WindowClosingEventArgs;
            for (int i = m_stackLayers.Count() - 1; i >= 0; i--)
            {
                if (bIsWindowClosingEvent)
                {
                    m_stackLayers[i].OnClose();
                }
                else
                {
                    m_stackLayers[i].OnEvent(t_evtArgs);
                    if (t_evtArgs.IsHandled)
                    {
                        break;
                    }
                }
            }
        }

        #endregion


        #region Window events

        private void OnWindowRender(double t_fElapsedTime)
        {
            for (int i = 0; i < m_stackLayers.Count(); i++)
            {
                m_stackLayers[i].OnRender(t_fElapsedTime);
            }
        }

        private void OnWindowUpdate(double t_fElapsedTime)
        {
            for (int i = 0; i < m_stackLayers.Count(); i++)
            {
                m_stackLayers[i].OnUpdate(t_fElapsedTime);
            }
        }      

        #endregion

    }
}
