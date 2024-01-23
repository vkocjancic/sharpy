using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpy.Rendering
{

    /// <summary>
    /// Open GL render context implementation
    /// </summary>
    internal class OpenGlRenderContext : RenderContextBase
    {

        #region Declarations

        /// <summary>
        /// Window handle
        /// </summary>
        private IWindow m_window;

        /// <summary>
        /// GL context variable
        /// </summary>
        private GL? m_gl;

        #endregion


        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="t_window">Window handle</param>
        public OpenGlRenderContext(IWindow t_window)
        {
            m_window = t_window;
        }

        #endregion


        #region RenderContextBase implementation

        public override object? GetContextHandle()
        {
            return m_gl;
        }

        public override void Init()
        {
            m_gl = m_window.CreateOpenGL();
            m_gl.ClearColor(System.Drawing.Color.CornflowerBlue);
        }

        public override void SetViewport(Vector2D<int> t_vec2dSize)
        {
            m_gl?.Viewport(t_vec2dSize);
        }

        public override void SwapBuffers()
        {
            m_gl?.Clear(ClearBufferMask.ColorBufferBit);
        }

        protected override void Dispose(bool t_bDisposing)
        {
            if (m_bDisposed)
            {
                return;
            }
            if (t_bDisposing)
            {
                // dispose managed state (managed objects)
                m_gl?.Dispose();
            }
            // free unmanaged resources (unmanaged objects) and override finalizer
            // set large fields to null
            m_gl = null;
            m_bDisposed = true;
        }

        #endregion

    }
}
