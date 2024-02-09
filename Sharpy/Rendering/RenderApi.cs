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

        private uint m_unVertexArray;
        private uint m_unVertexBuffer;
        private uint m_unElementArrayBuffer;
        private uint m_unShader;

        //Vertex shaders are run on each vertex.
        private static readonly string VertexShaderSource = @"
#version 330 core

layout (location = 0) in vec4 vPos;
        
void main()
{
    gl_Position = vec4(vPos.x, vPos.y, vPos.z, 1.0);
}
";

        //Fragment shaders are run on each fragment/pixel of the geometry.
        private static readonly string FragmentShaderSource = @"
#version 330 core

out vec4 FragColor;

void main()
{
    FragColor = vec4(1.0f, 0.5f, 0.2f, 1.0f);
}
";

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
        public void Close()
        {
            var gl = (GL?)m_ctxRender.GetContextHandle();
            if (null == gl)
            {
                return;
            }
            gl.DeleteBuffer(m_unVertexBuffer);
            gl.DeleteBuffer(m_unElementArrayBuffer);
            gl.DeleteVertexArray(m_unVertexArray);
            gl.DeleteProgram(m_unShader);
        }

        /// <summary>
        /// Perform object initialization
        /// </summary>
        public unsafe void Init() 
        {
            var gl = (GL?)m_ctxRender.GetContextHandle();
            if (null == gl)
            {
                return;
            }
            m_unVertexArray = gl.GenVertexArray();
            gl.BindVertexArray(m_unVertexArray);

            m_unVertexBuffer = gl.GenBuffer();
            gl.BindBuffer(BufferTargetARB.ArrayBuffer, m_unVertexBuffer);

            float[] rgfVertices =
            {
                 0.5f,  0.5f, 0.0f,
                 0.5f, -0.5f, 0.0f,
                -0.5f, -0.5f, 0.0f,
                -0.5f,  0.5f, 0.5f
            };

            // upload vertices to buffer
            fixed (float* pfBuf = rgfVertices)
            {
                gl.BufferData(BufferTargetARB.ArrayBuffer,
                    (nuint)(rgfVertices.Length * sizeof(uint)),
                    pfBuf,
                    BufferUsageARB.StaticDraw
                );
            }

            m_unElementArrayBuffer = gl.GenBuffer();
            gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, m_unElementArrayBuffer);

            // set up indices
            uint[] rgunIndices =
            {
                0u, 1u, 3u,
                1u, 2u, 3u
            };

            fixed (void* punBuf = rgunIndices)
            {
                gl.BufferData(BufferTargetARB.ElementArrayBuffer,
                    (nuint)(rgunIndices.Length * sizeof(uint)),
                    punBuf,
                    BufferUsageARB.StaticDraw
                );
            }

            // create vertex shader
            uint unVertexShader = gl.CreateShader(ShaderType.VertexShader);
            gl.ShaderSource(unVertexShader, VertexShaderSource);
            gl.CompileShader(unVertexShader);

            // check the shader after compilation
            string sInfoLog = gl.GetShaderInfoLog(unVertexShader);
            if (!string.IsNullOrEmpty(sInfoLog))
            {
                Logging.Log.Error("Error compiling vertex shader {0}", sInfoLog);
            }

            // create fragment shader
            uint unFragmentShader = gl.CreateShader(ShaderType.FragmentShader);
            gl.ShaderSource(unFragmentShader, FragmentShaderSource);
            gl.CompileShader(unFragmentShader);

            // check the shader after compilation
            sInfoLog = gl.GetShaderInfoLog(unVertexShader);
            if (!string.IsNullOrEmpty(sInfoLog))
            {
                Logging.Log.Error("Error compiling fragment shader {0}", sInfoLog);
            }

            // combine both shaders
            m_unShader = gl.CreateProgram();
            gl.AttachShader(m_unShader, unVertexShader);
            gl.AttachShader(m_unShader, unFragmentShader);
            gl.LinkProgram(m_unShader);

            // check linking errors
            gl.GetProgram(m_unShader, GLEnum.LinkStatus, out int nStatus);
            if (nStatus == 0)
            {
                Logging.Log.Error("Error linking shader {0}", gl.GetProgramInfoLog(m_unShader));
            }

            // clean up shaders
            gl.DetachShader(m_unShader, unVertexShader);
            gl.DetachShader(m_unShader, unFragmentShader);
            gl.DeleteShader(unVertexShader);
            gl.DeleteShader(unFragmentShader);

            gl.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), null);
            gl.EnableVertexAttribArray(0);
        }

        /// <summary>
        /// Draw quad object
        /// </summary>
        /// <param name="t_v2dfStart">Start position</param>
        /// <param name="t_v2dfEnd">End position</param>
        public unsafe void DrawQuad(Vector2D<decimal> t_v2dfStart, Vector2D<decimal> t_v2dfEnd)
        {
            var gl = (GL?)m_ctxRender.GetContextHandle();
            if (null == gl)
            {
                return;
            }
            gl.BindVertexArray(m_unVertexArray);
            gl.UseProgram(m_unShader);
            gl.DrawElements(PrimitiveType.Triangles, (uint)6 /* indices.length */, DrawElementsType.UnsignedInt, null);
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
