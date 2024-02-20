using Silk.NET.OpenGL;
using Silk.NET.SDL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpy.Rendering.OpenGL
{
    public static class OpenGlTypeAdapter
    {

        #region Adapter methods

        /// <summary>
        /// Converts shader attribute type to OpenGL type
        /// </summary>
        /// <param name="t_type">Shader attribute type to convert</param>
        /// <returns>OpenGL type. If no matching type found, it asserts and returns 0.</returns>
        public static GLEnum FromShaderAttributeType(ShaderAttribute.DataType t_type)
        {
            switch(t_type)
            {
                case ShaderAttribute.DataType.Float:
                case ShaderAttribute.DataType.Float2:
                case ShaderAttribute.DataType.Float3:
                case ShaderAttribute.DataType.Float4:
                    return GLEnum.Float;
            }

            Debug.Fail("Unknown shader attribute type");
            return GLEnum.None;
        }

        #endregion

    }
}
