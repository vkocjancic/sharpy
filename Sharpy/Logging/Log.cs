using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sharpy.Logging
{
    /// <summary>
    /// Sharpy logger class
    /// </summary>
    /// <remarks>
    /// Why? Because nuget is a bitch
    /// </remarks>
    internal static class Log
    {

        #region Declarations

        /// <summary>
        /// Enum containing all availabble log levels
        /// </summary>
        public enum LogLevel
        {
            DEBUG,
            INFO, 
            WARN,
            ERROR,
            FATAL
        }

        private static object oLock = new object();
        private static int m_nBufferPos = 0;
        private static int m_nCurrentBuffer = 0;
        private static LogEntry?[][] m_rglogeEntriesBuffer = new LogEntry?[2][];

        #endregion


        #region Constructors

        static Log()
        {
            m_rglogeEntriesBuffer[0] = new LogEntry?[100];
            m_rglogeEntriesBuffer[1] = new LogEntry?[100];
            Timer tmrLog = new Timer(OnTimerEvent, null, 100, 100);
        }

        #endregion


        #region Public methods

        /// <summary>
        /// Log message with DEBUG level
        /// </summary>
        /// <param name="t_sMessage">Message with formatted arguments</param>
        /// <param name="t_rgobjArgs">Arguments to use in message</param>
        public static void Debug(string t_sMessage, params object[] t_rgobjArgs)
        {
            WriteToLog(LogLevel.DEBUG, t_sMessage, t_rgobjArgs);
        }

        /// <summary>
        /// Log message with INFO level
        /// </summary>
        /// <param name="t_sMessage">Message with formatted arguments</param>
        /// <param name="t_rgobjArgs">Arguments to use in message</param>
        public static void Info(string t_sMessage, params object[] t_rgobjArgs)
        {
            WriteToLog(LogLevel.INFO, t_sMessage, t_rgobjArgs);
        }

        /// <summary>
        /// Log message with WARN level
        /// </summary>
        /// <param name="t_sMessage">Message with formatted arguments</param>
        /// <param name="t_rgobjArgs">Arguments to use in message</param>
        public static void Warn(string t_sMessage, params object[] t_rgobjArgs)
        {
            WriteToLog(LogLevel.WARN, t_sMessage, t_rgobjArgs);
        }

        /// <summary>
        /// Log message with ERROR level
        /// </summary>
        /// <param name="t_sMessage">Message with formatted arguments</param>
        /// <param name="t_rgobjArgs">Arguments to use in message</param>
        public static void Error(string t_sMessage, params object[] t_rgobjArgs)
        {
            WriteToLog(LogLevel.ERROR, t_sMessage, t_rgobjArgs);
        }

        /// <summary>
        /// Log exception with ERROR level
        /// </summary>
        /// <param name="t_sMessage">Error message</param>
        /// <param name="t_ex">Exception to log</param>
        public static void Error(string t_sMessage, Exception t_ex)
        {
            Error("{0}\n{1}\n{2}", new object[] { t_sMessage, t_ex.Message, t_ex.StackTrace ?? "" });
        }

        /// <summary>
        /// Log message with FATAL level
        /// </summary>
        /// <param name="t_sMessage">Message with formatted arguments</param>
        /// <param name="t_rgobjArgs">Arguments to use in message</param>
        public static void Fatal(string t_sMessage, params object[] t_rgobjArgs)
        {
            WriteToLog(LogLevel.FATAL, t_sMessage, t_rgobjArgs);
        }

        /// <summary>
        /// Log exception with FATAL level
        /// </summary>
        /// <param name="t_sMessage">Error message</param>
        /// <param name="t_ex">Exception to log</param>
        public static void Fatal(string t_sMessage, Exception t_ex)
        {
            Fatal("{0}\n{1}\n{2}", new object[] { t_sMessage, t_ex.Message, t_ex.StackTrace ?? "" });
        }

        #endregion


        #region Helper methods

        /// <summary>
        /// Write message to log. It also color codes output
        /// </summary>
        /// <param name="t_logLevel">Log level</param>
        /// <param name="t_sMessage">Message with formatted arguments</param>
        /// <param name="t_rgobjArgs">ARguments to use in message</param>
        private static void WriteToLog(LogLevel t_logLevel, string t_sMessage, params object[] t_rgobjArgs)
        {
            lock (oLock)
            {
                m_rglogeEntriesBuffer[m_nCurrentBuffer][m_nBufferPos] = new LogEntry(t_logLevel, string.Format(t_sMessage, t_rgobjArgs));
                m_nBufferPos++;
            }

        }

        /// <summary>
        /// Timer callback. Does actual logging
        /// </summary>
        /// <param name="t_oState">State of timer. Is just null</param>
        private static void OnTimerEvent(object? t_oState)
        {
            int nBuffer = 0;
            // swap buffers
            lock(oLock)
            {
                nBuffer = m_nCurrentBuffer;
                m_nCurrentBuffer = (m_nCurrentBuffer + 1) % 2;
                m_nBufferPos = 0;
            }
            foreach(LogEntry? logeEntry in m_rglogeEntriesBuffer[nBuffer])
            {
                if (logeEntry == null)
                {
                    break;
                }
                ConsoleColor colrOutput = Console.ForegroundColor;
                switch (logeEntry.Value.m_logLevel)
                {
                    case LogLevel.ERROR: colrOutput = ConsoleColor.Red; break;
                    case LogLevel.FATAL: colrOutput = ConsoleColor.DarkMagenta; break;
                    case LogLevel.INFO: colrOutput = (colrOutput == ConsoleColor.Green) ? ConsoleColor.Gray : ConsoleColor.Green; break;
                    case LogLevel.WARN: colrOutput = ConsoleColor.Yellow; break;
                }
                ConsoleColor colrPrev = Console.ForegroundColor;
                Console.ForegroundColor = colrOutput;
                Console.WriteLine($"{logeEntry.Value.m_logLevel}: {logeEntry.Value.m_sMessage}");
                Console.ForegroundColor = colrPrev;
            }
            Array.Clear(m_rglogeEntriesBuffer[nBuffer]);
        }


        #endregion

    }
}
