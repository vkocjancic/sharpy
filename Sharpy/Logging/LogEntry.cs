using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpy.Logging
{
    /// <summary>
    /// Struct for storing log entries
    /// </summary>
    internal struct LogEntry
    {

        #region Fields

        /// <summary>
        /// Log level
        /// </summary>
        public Log.LogLevel m_logLevel;

        /// <summary>
        /// Log message
        /// </summary>
        public string m_sMessage;

        #endregion


        #region Constructors

        /// <summary>
        /// Constructor for LogEntry
        /// </summary>
        /// <param name="t_level">Log level</param>
        /// <param name="t_sMessage">Log message</param>
        public LogEntry(Log.LogLevel t_level, string t_sMessage)
        {
            m_logLevel = t_level;
            m_sMessage = t_sMessage;
        }

        #endregion

    }
}
