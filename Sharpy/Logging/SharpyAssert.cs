using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpy.Logging
{
    
    /// <summary>
    /// Sharpy custom assertion class
    /// </summary>
    internal static class SharpyAssert
    {

        /// <summary>
        /// Checks for condition. If condition is false, it asserts and logs the assertion as FATAL error
        /// </summary>
        /// <param name="t_bCondition">Condition to check</param>
        /// <param name="t_sMessage">Message to display, if condition fails</param>
        public static void Assert([DoesNotReturnIf(false)] bool t_bCondition, string t_sMessage)
        {
            if (!t_bCondition)
            {
                Debug.Fail(t_sMessage);
                Logging.Log.Fatal(t_sMessage);
            }
        }

        /// <summary>
        /// Emits the specified error message
        /// </summary>
        /// <param name="t_sMessage">Message to emit</param>
        [DoesNotReturn]
        public static void Fail(string t_sMessage)
        {
            Assert(false, t_sMessage);
        }

    }
}
