using Silk.NET.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpy.Rendering.OpenGL
{
    /// <summary>
    /// OpenGL vertex buffer implementation
    /// </summary>
    internal class OpenGlVertexBuffer : VertexBufferBase
    {

        #region Declarations

        private readonly GL m_gl;
        private readonly uint m_unArrayId;
        private readonly uint m_unBufferId;

        #endregion


        #region Constructors

        /// <summary>
        /// Constructors
        /// </summary>
        /// <param name="t_gl">GL render context</param>
        /// <param name="t_rgfVertices">Vertices to load</param>
        public unsafe OpenGlVertexBuffer(GL t_gl, float[]? t_rgfVertices) : base(t_rgfVertices)
        {
            m_gl = t_gl;
            m_unArrayId = m_gl.GenVertexArray();
            m_gl.BindVertexArray(m_unArrayId);

            m_unBufferId = m_gl.GenBuffer();
            m_gl.BindBuffer(BufferTargetARB.ArrayBuffer, m_unBufferId);

            // upload vertices to buffer
            fixed (float* pfBuf = m_rgfVertices)
            {
                m_gl.BufferData(BufferTargetARB.ArrayBuffer,
                    (nuint)((m_rgfVertices?.Length ?? 0) * sizeof(uint)),
                    pfBuf,
                    BufferUsageARB.StaticDraw
                );
            }
        }

        #endregion


        #region VertexBufferBase implementation

        public override void Bind()
        {
            m_gl.BindVertexArray(m_unArrayId);
        }

        public override void Unbind()
        {
            m_gl.DeleteBuffer(m_unBufferId);
            m_gl.DeleteVertexArray(m_unArrayId);
        } 

        #endregion

    }

}
