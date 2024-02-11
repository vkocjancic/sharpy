using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpy.Rendering
{

    /// <summary>
    /// Contains shader attribute properties
    /// </summary>
    public struct ShaderAttribute
    {

        #region Fields

        public string m_sName;
        public uint m_unPosition;
        public int m_nSize;

        #endregion


        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="t_sName">Attribute name</param>
        /// <param name="t_unPosition">Attribute position</param>
        /// <param name="t_nSize">Attribute size</param>
        public ShaderAttribute(string t_sName, uint t_unPosition, int t_nSize)
        {
            m_sName = t_sName;
            m_unPosition = t_unPosition;    
            m_nSize = t_nSize;
        }

        #endregion

    }
}
