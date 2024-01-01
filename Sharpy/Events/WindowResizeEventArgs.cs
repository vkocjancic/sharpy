using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpy.Events
{

    /// <summary>
    /// Class for storing data of window size when window is resized
    /// </summary>
    public class WindowResizeEventArgs : EventArgsBase
    {

        #region Properties

        /// <summary>
        /// New window height
        /// </summary>
        public int WindowHeight { get; set; } = 0;

        /// <summary>
        /// New window width
        /// </summary>
        public int WindowWidth { get; set; } = 0;

        #endregion


        #region Overrides

        /// <summary>
        /// Display information about stored data
        /// </summary>
        /// <returns>String representation of stored data</returns>
        /// <remarks>For debugging purposes only!</remarks>
        public override string ToString()
        {
            return $"width = {WindowWidth}; height = {WindowHeight}";
        }

        #endregion

    }

}
