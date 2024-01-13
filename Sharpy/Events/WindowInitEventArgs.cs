using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpy.Events
{

    /// <summary>
    /// Class for storing data window is loading
    /// </summary>
    public class WindowInitEventArgs : EventArgsBase
    {

        #region Overrides

        /// <summary>
        /// Display information about stored data
        /// </summary>
        /// <returns>String representation of stored data</returns>
        /// <remarks>For debugging purposes only!</remarks>
        public override string ToString()
    {
        return $"Window loading";
    }

    #endregion

}
}
