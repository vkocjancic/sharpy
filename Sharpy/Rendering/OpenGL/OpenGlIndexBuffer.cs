using Silk.NET.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpy.Rendering.OpenGL
{

    /// <summary>
    /// OpenGL implementation of index buffer
    /// </summary>
    internal class OpenGlIndexBuffer : IndexBufferBase
    {

        #region Declarations

        private readonly GL m_gl;
        private readonly uint m_unBufferId;

        #endregion


        #region Constructors

        /// <summary>
        /// Constructors
        /// </summary>
        /// <param name="t_gl">Render context</param>
        /// <param name="t_rgnIndices">Indices to load to buffer</param>
        public unsafe OpenGlIndexBuffer(GL t_gl, uint[]? t_rgnIndices) : base(t_rgnIndices)
        {
            m_gl = t_gl;
            m_unBufferId = m_gl.GenBuffer();
            m_gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, m_unBufferId);

            fixed (void* punBuf = t_rgnIndices)
            {
                m_gl.BufferData(BufferTargetARB.ElementArrayBuffer,
                    (nuint)((m_rgnIndices?.Length ?? 0) * sizeof(uint)),
                    punBuf,
                    BufferUsageARB.StaticDraw
                );
            }
        }

        #endregion


        #region IndexBufferBase implementation

        public override unsafe void Bind()
        {
            m_gl.DrawElements(PrimitiveType.Triangles, (uint)(m_rgnIndices?.Length ?? 0), DrawElementsType.UnsignedInt, null);
        }

        public override void Unbind()
        {
            m_gl.DeleteBuffer(m_unBufferId);
        } 

        #endregion

    }
}
