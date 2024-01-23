using Silk.NET.Maths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpy.Rendering
{

    /// <summary>
    /// Render context base class
    /// </summary>
    internal abstract class RenderContextBase : IDisposable
    {

        #region Declarations

        /// <summary>
        /// Flag defines if object has already been disposed
        /// </summary>
        protected bool m_bDisposed = false;

        #endregion


        #region Abstract methods

        /// <summary>
        /// Abstract method for implementation of dispose method
        /// </summary>
        /// <param name="t_bDisposing">Flag for disposing managed state</param>
        protected abstract void Dispose(bool t_bDisposing);

        /// <summary>
        /// Get implementation context handle
        /// </summary>
        /// <returns>Implementation context handle</returns>
        public abstract object? GetContextHandle();

        /// <summary>
        /// Initializes render context
        /// </summary>
        public abstract void Init();

        /// <summary>
        /// Set render context viewport
        /// </summary>
        /// <param name="t_vec2dSiz">2D vector containing new size of window</param>
        public abstract void SetViewport(Vector2D<int> t_vec2dSiz);

        /// <summary>
        /// Swap buffers on render
        /// </summary>
        public abstract void SwapBuffers();

        #endregion


        #region IDisposable implementation

        /// <summary>
        /// Disposes object in a safe and sound way
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }      

        #endregion

    }
}
