using Sharpy.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpy.Window
{
    
    /// <summary>
    /// Base class for all sharpy windows
    /// </summary>
    public abstract class WindowBase
    {

        #region Fields

        /// <summary>
        /// Window options
        /// </summary>
        protected WindowOptions m_optWindow;

        /// <summary>
        /// Event dispatcher
        /// </summary>
        protected EventDispatcher m_evtDispatcher;

        #endregion


        #region Constructors

        /// <summary>
        /// Constructs window object
        /// </summary>
        /// <param name="t_optWindow">Window options to apply to window</param>
        /// <param name="t_evtDispatcher">Event dispatcher to use for handling events</param>
        public WindowBase(WindowOptions t_optWindow, EventDispatcher t_evtDispatcher)
        {
            m_optWindow = t_optWindow;
            m_evtDispatcher = t_evtDispatcher;
        }

        #endregion


        #region Public methods

        /// <summary>
        /// Runs window with game loop
        /// </summary>
        public abstract void Run();
        
        #endregion

    }

}
