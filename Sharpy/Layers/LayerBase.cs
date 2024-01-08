using Sharpy.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpy.Layers
{
    /// <summary>
    /// Base layer class
    /// </summary>
    public abstract class LayerBase
    {

        #region Fields

        /// <summary>
        /// Layer name
        /// </summary>
        /// <remarks>Used for debugging purposes only</remarks>
        private string m_sLayerName = "";

        /// <summary>
        /// Is layer enabled
        /// </summary>
        public bool m_bIsEnabled = true;

        #endregion


        #region Abstract methods

        /// <summary>
        /// Handles any event
        /// </summary>
        /// <param name="t_evtArgs">Event arguments</param>
        public abstract void OnEvent(EventArgsBase t_evtArgs);

        /// <summary>
        /// Updates layer
        /// </summary>
        /// <param name="t_fElapsedTime">Elapsed time in miliseconds</param>
        public abstract void OnUpdate(double t_fElapsedTime);

        /// <summary>
        /// Renders layer
        /// </summary>
        /// <param name="t_fElapsedTime">Elapsed time in miliseconds</param>
        public abstract void OnRender(double t_fElapsedTIme);

        #endregion


        #region Overrides

        /// <summary>
        /// String representation of object
        /// </summary>
        /// <returns>Layer name</returns>
        /// <remarks>Used for debugging purposes only</remarks>
        public override string ToString()
        {
            return m_sLayerName;
        }

        #endregion

    }
}
