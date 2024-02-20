using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpy.Rendering
{

    /// <summary>
    /// Vertex buffer base class
    /// </summary>
    internal abstract class VertexBufferBase
    {

        #region Declarations

        protected readonly float[]? m_rgfVertices;

        #endregion


        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="t_rgfVertices">Vertices to load to vertex buffer</param>
        public VertexBufferBase(float[]? t_rgfVertices)
        {
            m_rgfVertices = t_rgfVertices;
        }

        #endregion


        #region Abstract methods

        /// <summary>
        /// Bind vertex buffer
        /// </summary>
        public abstract void Bind();

        /// <summary>
        /// Set shader attributes to vertex buffer
        /// </summary>
        /// <param name="t_lstAttributes">Attributes to set. Value is available in read-only mode</param>
        public abstract void SetAttributes(ref readonly ShaderAttributeList t_lstAttributes);

        /// <summary>
        /// Unbind vertex buffer
        /// </summary>
        public abstract void Unbind();

        #endregion

    }

}
