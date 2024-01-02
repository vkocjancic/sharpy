using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpy.Events
{

    /// <summary>
    /// Class for storing data when mouse scroll wheel is used
    /// </summary>
    public class MouseScrolledEventArgs : EventArgsBase
    {

        #region Properties

        /// <summary>
        /// Offset of scroll in horizontal axis
        /// </summary>
        public decimal OffsetX { get; set; } = 0M;

        /// <summary>
        /// Offset of scroll in vertical axis
        /// </summary>
        public decimal OffsetY { get; set; } = 0M;

        #endregion


        #region Overrides

        /// <summary>
        /// Display information about stored data
        /// </summary>
        /// <returns>String representation of stored data</returns>
        /// <remarks>For debugging purposes only!</remarks>
        public override string ToString()
        {
            return $"Mouse scrolled: offX = {OffsetX}; offY = {OffsetY}";
        }

        #endregion

    }

}
