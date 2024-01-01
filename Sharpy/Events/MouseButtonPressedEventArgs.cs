using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Sharpy.Events
{

    /// <summary>
    /// Class for storing data when mouse button is pressed
    /// </summary>
    public class MouseButtonPressedEventArgs : EventArgsBase
    {

        #region Properties

        /// <summary>
        /// Button pressed
        /// </summary>
        public int ButtonCode { get; set; }

        #endregion


        #region Overrides

        /// <summary>
        /// Display information about stored data
        /// </summary>
        /// <returns>String representation of stored data</returns>
        /// <remarks>For debugging purposes only!</remarks>
        public override string ToString()
        {
            return $"buttonCode = {ButtonCode}";
        }

        #endregion

    }

}
