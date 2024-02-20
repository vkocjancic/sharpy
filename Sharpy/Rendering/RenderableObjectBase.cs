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

        #endregion


        #region Properties

        /// <summary>
        /// Get fragment shader
        /// </summary>
        public Sharpy.Rendering.Shader? FragmentShader { get; private set; }

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
        public Sharpy.Rendering.Shader? VertexShader { get; private set; }

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
        public abstract void Init();

        public void Render()
        {
            var api = RenderApiBase.GetInstance();
            api.Draw(this);
        }

        #endregion


        #region Helper methods

        /// <summary>
        /// Initialize renderable object
        /// </summary>
        /// <param name="t_rgvec3dVertices">Object vertices</param>
        /// <param name="t_rgvec3dIndices">Object indices</param>
        /// <param name="t_shdrVertex">Vertex shader</param>
        /// <param name="t_shdrFragment">Fragment shader</param>
        protected void Init(float[] t_rgvec3dVertices, uint[] t_rgvec3dIndices, Sharpy.Rendering.Shader t_shdrVertex, Sharpy.Rendering.Shader t_shdrFragment)
        {
            SetIndices(t_rgvec3dIndices);
            SetVertices(t_rgvec3dVertices);
            FragmentShader = t_shdrFragment;
            VertexShader = t_shdrVertex;

            var api = RenderApiBase.GetInstance();
            api.Init(this);
        }

        /// <summary>
        /// Set indices array for render initialization
        /// </summary>
        /// <param name="t_rgvec3dIndices">Indices to set</param>
        private void SetIndices(uint[] t_rgvec3dIndices)
        {
            Debug.Assert(t_rgvec3dIndices != null);
            Indices = new uint[t_rgvec3dIndices.Length];
            Array.Copy(t_rgvec3dIndices, Indices, t_rgvec3dIndices.Length);
        }

        /// <summary>
        /// Set vertices array for render initialization
        /// </summary>
        /// <param name="t_rgvec3dVertices"></param>
        private void SetVertices(float[] t_rgvec3dVertices)
        {
            Debug.Assert(t_rgvec3dVertices != null);
            Vertices = new float[t_rgvec3dVertices.Length];
            Array.Copy(t_rgvec3dVertices, Vertices, t_rgvec3dVertices.Length);
        }

        #endregion

    }
}
