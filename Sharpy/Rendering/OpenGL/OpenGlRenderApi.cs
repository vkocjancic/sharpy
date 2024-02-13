using Silk.NET.OpenGL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpy.Rendering.OpenGL
{
    internal class OpenGlRenderApi : RenderApiBase
    {

        #region Declarations

        GL? m_gl;

        #endregion


        #region RenderApiBase implementation

        public override void Close(RenderableObjectBase obj)
        {
            Debug.Assert(m_gl != null, "Render context is not set");
            m_gl.DeleteBuffer(obj.m_unVertexBufferId);
            m_gl.DeleteBuffer(obj.m_unElementArrayBufferId);
            m_gl.DeleteVertexArray(obj.m_unVertexArrayId);
            m_gl.DeleteProgram(obj.m_unShaderProgramId);
        }

        public override unsafe void Draw(RenderableObjectBase obj)
        {
            Debug.Assert(m_gl != null, "Render context is not set");
            m_gl.BindVertexArray(obj.m_unVertexArrayId);
            m_gl.UseProgram(obj.m_unShaderProgramId);

            m_gl.DrawElements(PrimitiveType.Triangles, (uint)(obj.Indices?.Length ?? 0), DrawElementsType.UnsignedInt, null);
        }

        public override unsafe void Init(RenderableObjectBase obj)
        {
            m_gl = (GL?)RenderContext?.GetContextHandle();
            if (null == m_gl)
            {
                return;
            }

            obj.m_unVertexArrayId = m_gl.GenVertexArray();
            m_gl.BindVertexArray(obj.m_unVertexArrayId);

            obj.m_unVertexBufferId = m_gl.GenBuffer();
            m_gl.BindBuffer(BufferTargetARB.ArrayBuffer, obj.m_unVertexBufferId);

            // upload vertices to buffer
            fixed (float* pfBuf = obj.Vertices)
            {
                m_gl.BufferData(BufferTargetARB.ArrayBuffer,
                    (nuint)((obj.Vertices?.Length ?? 0) * sizeof(uint)),
                    pfBuf,
                    BufferUsageARB.StaticDraw
                );
            }

            obj.m_unElementArrayBufferId = m_gl.GenBuffer();
            m_gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, obj.m_unElementArrayBufferId);

            fixed (void* punBuf = obj.Indices)
            {
                m_gl.BufferData(BufferTargetARB.ElementArrayBuffer,
                    (nuint)((obj.Indices?.Length ?? 0) * sizeof(uint)),
                    punBuf,
                    BufferUsageARB.StaticDraw
                );
            }

            // create vertex shader
            uint unVertexShader = m_gl.CreateShader(ShaderType.VertexShader);
            m_gl.ShaderSource(unVertexShader, obj.VertexShader.m_sSource);
            m_gl.CompileShader(unVertexShader);

            // check the shader after compilation
            string sInfoLog = m_gl.GetShaderInfoLog(unVertexShader);
            if (!string.IsNullOrEmpty(sInfoLog))
            {
                Logging.Log.Error("Error compiling vertex shader for object {0}:\r\n{1}", obj, sInfoLog);
            }

            // create fragment shader
            uint unFragmentShader = m_gl.CreateShader(ShaderType.FragmentShader);
            m_gl.ShaderSource(unFragmentShader, obj.FragmentShader.m_sSource);
            m_gl.CompileShader(unFragmentShader);

            // check the shader after compilation
            sInfoLog = m_gl.GetShaderInfoLog(unVertexShader);
            if (!string.IsNullOrEmpty(sInfoLog))
            {
                Logging.Log.Error("Error compiling fragment shader for object {0}:\r\n{1}", obj, sInfoLog);
            }

            // combine both shaders
            obj.m_unShaderProgramId = m_gl.CreateProgram();
            m_gl.AttachShader(obj.m_unShaderProgramId, unVertexShader);
            m_gl.AttachShader(obj.m_unShaderProgramId, unFragmentShader);
            m_gl.LinkProgram(obj.m_unShaderProgramId);

            // check linking errors
            m_gl.GetProgram(obj.m_unShaderProgramId, GLEnum.LinkStatus, out int nStatus);
            if (nStatus == 0)
            {
                Logging.Log.Error("Error linking shader for object {0}:\r\n{1}", obj, m_gl.GetProgramInfoLog(obj.m_unShaderProgramId));
            }

            // clean up shaders
            m_gl.DetachShader(obj.m_unShaderProgramId, unVertexShader);
            m_gl.DetachShader(obj.m_unShaderProgramId, unFragmentShader);
            m_gl.DeleteShader(unVertexShader);
            m_gl.DeleteShader(unFragmentShader);

            foreach (ShaderAttribute attribute in obj.VertexShader.m_rgAttributes ?? new List<ShaderAttribute>())
            {
                m_gl.VertexAttribPointer(attribute.m_unPosition, attribute.m_nSize, VertexAttribPointerType.Float, false, (uint)attribute.m_nSize * sizeof(float), null);
            }
            m_gl.EnableVertexAttribArray(0);
        } 

        #endregion

    }
}
