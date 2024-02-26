using Sharpy.Logging;
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


        #region Factory methods

        /// <summary>
        /// Creates shader from source file
        /// </summary>
        /// <param name="t_sShaderFilePath">Shader source file path</param>
        /// <returns>Shader object</returns>
        public static Shader CreateFromFile(string t_sShaderFilePath)
        {
            SharpyAssert.Assert(!string.IsNullOrEmpty(t_sShaderFilePath), "Shader file path not specified");
            var shader = new Shader();
            try
            {
                shader.m_sSource = File.ReadAllText(t_sShaderFilePath);
            }
            catch(Exception ex)
            {
                Log.Error($"Could not load shader from path '{t_sShaderFilePath}'", ex);
            }
            return shader;
        }

        #endregion

    }
}
