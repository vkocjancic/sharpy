using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpy.Rendering
{

    /// <summary>
    /// Abstract class for index buffers
    /// </summary>
    internal abstract class IndexBufferBase
    {

        #region Declarations

        protected readonly uint[]? m_rgnIndices;

        #endregion


        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="t_rgnIndices">Indices to load to index buffer</param>
        public IndexBufferBase(uint[]? t_rgnIndices)
        {
            m_rgnIndices = t_rgnIndices;
        }

        #endregion


        #region Abstract methods

        /// <summary>
        /// Bind index buffer
        /// </summary>
        public abstract void Bind();

        /// <summary>
        /// Unbind index buffer
        /// </summary>
        public abstract void Unbind();

        #endregion

    }
}
