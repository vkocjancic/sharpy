using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpy.Events
{

    /// <summary>
    /// Class for storing data when key is released
    /// </summary>
    public class KeyReleasedEventArgs : EventArgsBase
    {

        #region Properties

        /// <summary>
        /// Released key key code
        /// </summary>
        public int KeyCode { get; set; }

        #endregion


        #region Overrides

        /// <summary>
        /// Display information about stored data
        /// </summary>
        /// <returns>String representation of stored data</returns>
        /// <remarks>For debugging purposes only!</remarks>
        public override string ToString()
        {
            return $"Key released: keyCode = {KeyCode}";
        }

        #endregion

    }

}
