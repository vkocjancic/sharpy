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
    /// Base class for all renderable objects (e.g. triangles, quads etc.)
    /// </summary>
    public abstract class RenderableObjectBase
    {

        #region Fields

        /// <summary>
        /// Element array buffer for object
        /// </summary>
        internal IndexBufferBase? m_bufIndex;

        /// <summary>
        /// Shader program id
        /// </summary>
        public uint m_unShaderProgramId;

        /// <summary>
        /// Stores vertex buffer
        /// </summary>
        internal VertexBufferBase? m_bufVertex;

        protected Dictionary<ShaderType, Shader> m_dictShaders = new Dictionary<ShaderType, Shader>();

        #endregion


        #region Properties

        /// <summary>
        /// Get fragment shader
        /// </summary>
        public Sharpy.Rendering.Shader? FragmentShader 
        { 
            get
            {
                return m_dictShaders[ShaderType.FragmentShader];
            }
        }

        /// <summary>
        /// Get indices array
        /// </summary>
        public uint[]? Indices { get; private set; }

        /// <summary>
        /// Get vertices array
        /// </summary>
        public float[]? Vertices { get; private set; }    

        /// <summary>
        /// Get vertex shader
        /// </summary>
        public Sharpy.Rendering.Shader? VertexShader
        {
            get
            {
                return m_dictShaders[ShaderType.VertexShader];
            }
        }

        #endregion


        #region Abstract methods

        /// <summary>
        /// Initializes concrete object
        /// </summary>
        protected abstract void InitInternal();

        #endregion


        #region Public methods

        /// <summary>
        /// Clean up resources
        /// </summary>
        public void Close()
        {
            var api = RenderApiBase.GetInstance();
            api.Close(this);
        }

        /// <summary>
        /// Initializes the object
        /// </summary>
        public void Init()
        {
            InitInternal();

            Debug.Assert(Indices != null && Indices.Length > 0, "Indices not set");
            Debug.Assert(Vertices != null && Vertices.Length > 0, "Vertices not set");
            Debug.Assert(FragmentShader != null, "Fragment shader not set");
            Debug.Assert(VertexShader != null, "Vertex shader not set");

            var api = RenderApiBase.GetInstance();
            api.Init(this);
        }

        public void Render()
        {
            var api = RenderApiBase.GetInstance();
            api.Draw(this);
        }

        #endregion


        #region Helper methods

        /// <summary>
        /// Appends shader
        /// </summary>
        /// <param name="t_typeShader">Type of shader</param>
        /// <param name="shader">Shader to append</param>
        protected void AppendShader(ShaderType t_typeShader, Shader shader)
        {
            if (m_dictShaders.ContainsKey(t_typeShader))
            {
                Debug.Fail($"Shader of type {t_typeShader} already exists");
                return;
            }
            m_dictShaders.Add(t_typeShader, shader);
        }

        /// <summary>
        /// Set indices array for render initialization
        /// </summary>
        /// <param name="t_rgvec3dIndices">Indices to set</param>
        protected void SetIndices(uint[] t_rgvec3dIndices)
        {
            Debug.Assert(t_rgvec3dIndices != null);
            Indices = new uint[t_rgvec3dIndices.Length];
            Array.Copy(t_rgvec3dIndices, Indices, t_rgvec3dIndices.Length);
        }

        /// <summary>
        /// Set vertices array for render initialization
        /// </summary>
        /// <param name="t_rgvec3dVertices"></param>
        protected void SetVertices(float[] t_rgvec3dVertices)
        {
            Debug.Assert(t_rgvec3dVertices != null);
            Vertices = new float[t_rgvec3dVertices.Length];
            Array.Copy(t_rgvec3dVertices, Vertices, t_rgvec3dVertices.Length);
        }

        #endregion

    }
}
