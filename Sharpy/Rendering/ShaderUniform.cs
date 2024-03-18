using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpy.Rendering
{
    /// <summary>
    /// Contains shader uniforms
    /// </summary>
    public struct ShaderUniform
    {

        #region Fields

        public string m_sName;
        public object m_objValue;

        #endregion


        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="t_sName">Name of uniform</param>
        /// <param name="t_objValue">Value to assign to uniform</param>
        public ShaderUniform(string t_sName, object t_objValue)
        {
            m_sName = t_sName;
            m_objValue = t_objValue;
        }


        #endregion

    }
}
