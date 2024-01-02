using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpy.Events
{
    /// <summary>
    /// Class for storing information when key is pressed
    /// </summary>
    public class KeyPressedEventArgs : EventArgsBase
    {

        #region Properties

        /// <summary>
        /// Pressed key key code
        /// </summary>
        public int KeyCode { get; set; }

        /// <summary>
        /// Number of repeates of same key
        /// </summary>
        public int NumberOfRepeats { get; set; }

        #endregion


        #region Overrides

        /// <summary>
        /// Display information about stored data
        /// </summary>
        /// <returns>String representation of stored data</returns>
        /// <remarks>For debugging purposes only!</remarks>
        public override string ToString()
        {
            return $"Key pressed: keyCode = {KeyCode}; numberOfRepeats = {NumberOfRepeats}";
        }

        #endregion

    }
}
