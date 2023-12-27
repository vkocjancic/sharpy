
using Sharpy.Logging;

namespace Sharpy.EntryPoint
{
    /// <summary>
    /// Main Sharpy application class. This is all you need to run sharpy engine app.
    /// </summary>
    public class SharpyApplication
    {

        #region Public methods

        /// <summary>
        /// Main function with game loop.
        /// </summary>
        public void Run()
        {
            // TODO: remove at next implementation
            Log.Debug("Sharpy application started at {0:dd.MM.yyyy HH:mm:ss}", DateTime.Now);
            Log.Info("Sharpy application started at {0:dd.MM.yyyy HH:mm:ss}", DateTime.Now);
            Log.Warn("Sharpy application started at {0:dd.MM.yyyy HH:mm:ss}", DateTime.Now);
            Log.Error("Sharpy application started at {0:dd.MM.yyyy HH:mm:ss}", DateTime.Now);
            Log.Error("Sharpy application error", new ApplicationException("Foo bar"));
            Log.Fatal("Sharpy application started at {0:dd.MM.yyyy HH:mm:ss}", DateTime.Now);
            Log.Fatal("Sharpy application critical error", new ApplicationException("Foo bar"));
            Thread.Sleep(200);

            // TODO: add game loop
        }

        #endregion

    }
}
