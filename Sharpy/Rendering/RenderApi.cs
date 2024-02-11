using Silk.NET.Maths;
using Silk.NET.OpenGL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpy.Rendering
{

    /// <summary>
    /// Api class for renderrer
    /// </summary>
    /// <remarks>
    /// This is a singleton class!!!
    /// </remarks>
    public class RenderApi
    {

        #region Declarations

        private static RenderApi? m_sapiInstance;
        private static object m_oLock = new object();

        private RenderContextBase m_ctxRender;

        #endregion


        #region Constructors

        /// <summary>
        /// Instance constructor
        /// </summary>
        /// <param name="t_ctxRender">Render graphics contexts</param>
        private RenderApi(RenderContextBase t_ctxRender) 
        { 
            m_ctxRender = t_ctxRender;
        }

        #endregion


        #region Public members

        /// <summary>
        /// Clean up buffers
        /// </summary>
        public void Close(RenderableObjectBase obj)
        {
            var gl = (GL?)m_ctxRender.GetContextHandle();
            if (null == gl)
            {
                return;
            }
            gl.DeleteBuffer(obj.m_unVertexBufferId);
            gl.DeleteBuffer(obj.m_unElementArrayBufferId);
            gl.DeleteVertexArray(obj.m_unVertexArrayId);
            gl.DeleteProgram(obj.m_unShaderProgramId);
        }

        /// <summary>
        /// Perform object initialization
        /// </summary>
        public unsafe void Init(RenderableObjectBase obj) 
        {
            var gl = (GL?)m_ctxRender.GetContextHandle();
            if (null == gl)
            {
                return;
            }
            obj.m_unVertexArrayId = gl.GenVertexArray();
            gl.BindVertexArray(obj.m_unVertexArrayId);

            obj.m_unVertexBufferId = gl.GenBuffer();
            gl.BindBuffer(BufferTargetARB.ArrayBuffer, obj.m_unVertexBufferId);

            // upload vertices to buffer
            fixed (float* pfBuf = obj.Vertices)
            {
                gl.BufferData(BufferTargetARB.ArrayBuffer,
                    (nuint)((obj.Vertices?.Length ?? 0) * sizeof(uint)),
                    pfBuf,
                    BufferUsageARB.StaticDraw
                );
            }

            obj.m_unElementArrayBufferId = gl.GenBuffer();
            gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, obj.m_unElementArrayBufferId);

            fixed (void* punBuf = obj.Indices)
            {
                gl.BufferData(BufferTargetARB.ElementArrayBuffer,
                    (nuint)((obj.Indices?.Length ?? 0) * sizeof(uint)),
                    punBuf,
                    BufferUsageARB.StaticDraw
                );
            }

            // create vertex shader
            uint unVertexShader = gl.CreateShader(ShaderType.VertexShader);
            gl.ShaderSource(unVertexShader, obj.VertexShader.m_sSource);
            gl.CompileShader(unVertexShader);

            // check the shader after compilation
            string sInfoLog = gl.GetShaderInfoLog(unVertexShader);
            if (!string.IsNullOrEmpty(sInfoLog))
            {
                Logging.Log.Error("Error compiling vertex shader for object {0}:\r\n{1}", obj, sInfoLog);
            }

            // create fragment shader
            uint unFragmentShader = gl.CreateShader(ShaderType.FragmentShader);
            gl.ShaderSource(unFragmentShader, obj.FragmentShader.m_sSource);
            gl.CompileShader(unFragmentShader);

            // check the shader after compilation
            sInfoLog = gl.GetShaderInfoLog(unVertexShader);
            if (!string.IsNullOrEmpty(sInfoLog))
            {
                Logging.Log.Error("Error compiling fragment shader for object {0}:\r\n{1}", obj, sInfoLog);
            }

            // combine both shaders
            obj.m_unShaderProgramId = gl.CreateProgram();
            gl.AttachShader(obj.m_unShaderProgramId, unVertexShader);
            gl.AttachShader(obj.m_unShaderProgramId, unFragmentShader);
            gl.LinkProgram(obj.m_unShaderProgramId);

            // check linking errors
            gl.GetProgram(obj.m_unShaderProgramId, GLEnum.LinkStatus, out int nStatus);
            if (nStatus == 0)
            {
                Logging.Log.Error("Error linking shader for object {0}:\r\n{1}", obj, gl.GetProgramInfoLog(obj.m_unShaderProgramId));
            }

            // clean up shaders
            gl.DetachShader(obj.m_unShaderProgramId, unVertexShader);
            gl.DetachShader(obj.m_unShaderProgramId, unFragmentShader);
            gl.DeleteShader(unVertexShader);
            gl.DeleteShader(unFragmentShader);

            foreach (ShaderAttribute attribute in obj.VertexShader.m_rgAttributes ?? new List<ShaderAttribute>())
            {
                gl.VertexAttribPointer(attribute.m_unPosition, attribute.m_nSize, VertexAttribPointerType.Float, false, (uint)attribute.m_nSize * sizeof(float), null);
            }
            gl.EnableVertexAttribArray(0);
        }


        public unsafe void Draw(RenderableObjectBase obj)
        {
            var gl = (GL?)m_ctxRender.GetContextHandle();
            if (null == gl)
            {
                return;
            }
            gl.BindVertexArray(obj.m_unVertexArrayId);
            gl.UseProgram(obj.m_unShaderProgramId);
            
            gl.DrawElements(PrimitiveType.Triangles, (uint)(obj.Indices?.Length ?? 0), DrawElementsType.UnsignedInt, null);
        }

        #endregion


        #region Factory methods

        /// <summary>
        /// Get instance. If it does not exist, create one.
        /// </summary>
        /// <param name="t_ctxRender">Render graphics context</param>
        /// <returns>Redner API instance</returns>
        public static RenderApi GetInstance(RenderContextBase t_ctxRender)
        {
            lock(m_oLock)
            {
                if (null == m_sapiInstance) 
                {
                    m_sapiInstance = new RenderApi(t_ctxRender);
                }
            }
            return m_sapiInstance;
        }

        /// <summary>
        /// Get instance, if it is already created. If not, assertion is triggered!
        /// </summary>
        /// <returns>Render API instance</returns>
        public static RenderApi GetInstance()
        {
            Debug.Assert(null != m_sapiInstance, "Instance is not set!");
            return m_sapiInstance;
        }

        #endregion

    }

}
