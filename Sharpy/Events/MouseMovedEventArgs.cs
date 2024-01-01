using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpy.Events
{

    /// <summary>
    /// Class for storing data of mouse position whenever mouse is moved
    /// </summary>
    public class MouseMovedEventArgs : EventArgsBase
    {

        #region Properties

        /// <summary>
        /// New mouse position on horizontal axis
        /// </summary>
        public decimal PositionX { get; set; } = 0M;    

        /// <summary>
        /// New mouse position on vertical axis
        /// </summary>
        public decimal PositionY { get; set; } = 0M;

        #endregion


        #region Overrides

        /// <summary>
        /// Display information about stored data
        /// </summary>
        /// <returns>String representation of stored data</returns>
        /// <remarks>For debugging purposes only!</remarks>
        public override string ToString()
        {
            return $"x = {PositionX}; y = {PositionY}";
        }

        #endregion

    }

}
