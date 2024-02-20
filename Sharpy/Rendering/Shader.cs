using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpy.Rendering
{
    public class Shader
    {

        #region Fields

        public ShaderAttributeList m_lstAttributes = new ShaderAttributeList();

        public string m_sSource = "";

        #endregion


        #region Public methods

        ///// <summary>
        ///// Adds attribute to shader
        ///// </summary>
        ///// <param name="t_attr">Attribute to add</param>
        //public void AddAttribute(ShaderAttribute t_attr)
        //{
        //    m_rgAttributes.Add(t_attr);
        //}

        /// <summary>
        /// Adds attributes to shader
        /// </summary>
        /// <param name="t_rgAttributes">Attributes to add</param>
        public void AddAttributes(params ShaderAttribute[] t_rgAttributes)
        {
            m_lstAttributes.AddRange(t_rgAttributes);
        }

        #endregion

    }
}
