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
        /// General event
        /// </summary>
        public event Action<object, EventArgsBase>? Event;

        #endregion


        #region Abstract methods

        /// <summary>
        /// Dispatch event
        /// </summary>
        /// <param name="t_oSender">Object that triggered event</param>
        /// <param name="t_evtArgs">Event arguments</param>
        public void Dispatch(object t_oSender, EventArgsBase t_evtArgs)
        {
            Event?.Invoke(t_oSender, t_evtArgs);
        }

        #endregion

    }
}
