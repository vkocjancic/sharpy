using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpy.Core
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
        private enum LogLevel
        {
            DEBUG,
            INFO, 
            WARN,
            ERROR,
            FATAL
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
            ConsoleColor colrOutput = Console.ForegroundColor;
            switch(t_logLevel)
            {
                case LogLevel.ERROR: colrOutput = ConsoleColor.Red; break;
                case LogLevel.FATAL: colrOutput = ConsoleColor.DarkMagenta; break;
                case LogLevel.INFO: colrOutput = (colrOutput == ConsoleColor.Green) ? ConsoleColor.Gray : ConsoleColor.Green; break;
                case LogLevel.WARN: colrOutput = ConsoleColor.Yellow; break;
            }
            ConsoleColor colrPrev = Console.ForegroundColor;
            Console.ForegroundColor = colrOutput;
            Console.WriteLine($"{t_logLevel}: {t_sMessage}", t_rgobjArgs);
            Console.ForegroundColor = colrPrev;
        }

        #endregion

    }
}
