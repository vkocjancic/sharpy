using Sharpy.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpy.Rendering
{

    /// <summary>
    /// Abstract class for texture buffers
    /// </summary>
    internal abstract class TextureBufferBase
    {

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
