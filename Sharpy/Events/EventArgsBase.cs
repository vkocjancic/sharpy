using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpy.Events
{

    /// <summary>
    /// Abstract base class for Sharpy events
    /// </summary>
    public abstract class EventArgsBase : System.EventArgs
    {

        #region Properties

        public bool IsHandled { get; set; } = false;

        #endregion

    }

}
