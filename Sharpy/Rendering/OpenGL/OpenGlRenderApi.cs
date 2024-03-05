using Sharpy.Logging;
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
            SharpyAssert.Assert(m_gl != null, "Render context is not set");
            obj.m_bufTexture?.Unbind();
            obj.m_bufIndex?.Unbind();
            obj.m_bufVertex?.Unbind();
            m_gl.DeleteProgram(obj.m_unShaderProgramId);
        }

        public override unsafe void Draw(RenderableObjectBase obj)
        {
            SharpyAssert.Assert(m_gl != null, "Render context is not set");
            obj.m_bufVertex?.Bind();
            m_gl.UseProgram(obj.m_unShaderProgramId);
            
            obj.m_bufTexture?.Bind();
            obj.m_bufIndex?.Bind();
        }

        public override unsafe void Init(RenderableObjectBase obj)
        {
            m_gl = (GL?)RenderContext?.GetContextHandle();
            if (null == m_gl)
            {
                return;
            }

            obj.m_bufVertex = new OpenGlVertexBuffer(m_gl, obj.Vertices);
            if (obj.VertexShader != null)
            {
                obj.m_bufVertex.SetAttributes(ref obj.VertexShader.m_lstAttributes);
            }

            obj.m_bufIndex = new OpenGlIndexBuffer(m_gl, obj.Indices);

            // create vertex shader
            uint unVertexShader = m_gl.CreateShader(Silk.NET.OpenGL.ShaderType.VertexShader);
            m_gl.ShaderSource(unVertexShader, obj.VertexShader?.m_sSource);
            m_gl.CompileShader(unVertexShader);

            // check the shader after compilation
            string sInfoLog = m_gl.GetShaderInfoLog(unVertexShader);
            if (!string.IsNullOrEmpty(sInfoLog))
            {
                Logging.Log.Error("Error compiling vertex shader for object {0}:\r\n{1}", obj, sInfoLog);
            }

            // create fragment shader
            uint unFragmentShader = m_gl.CreateShader(Silk.NET.OpenGL.ShaderType.FragmentShader);
            m_gl.ShaderSource(unFragmentShader, obj.FragmentShader?.m_sSource);
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

            // set up textures

            if (null != obj.m_texture)
            {
                obj.m_bufTexture = new OpenGlTextureBuffer(m_gl, obj);
            }
        } 

        #endregion

    }
}
