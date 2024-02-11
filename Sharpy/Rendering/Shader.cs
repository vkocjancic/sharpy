using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpy.Rendering
{
    public struct Shader
    {

        #region Fields

        public List<ShaderAttribute> m_rgAttributes;

        public string m_sSource;

        #endregion


        #region Public methods

        /// <summary>
        /// Adds attribute to shader
        /// </summary>
        /// <param name="t_attr">Attribute to add</param>
        public void AddAttribute(ShaderAttribute t_attr)
        {
            if (null == m_rgAttributes)
            {
                m_rgAttributes = new List<ShaderAttribute>();
            }
            m_rgAttributes.Add(t_attr);
        }

        #endregion

    }
}
