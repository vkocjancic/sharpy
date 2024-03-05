using Sharpy.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpy.Rendering
{

    /// <summary>
    /// Class for handling textures
    /// </summary>
    public class Texture
    {

        #region Declarations

        public byte[] m_rgbData = new byte[] { };

        #endregion


        #region Factory methods

        public static Texture CreateFromFile(string t_sTextureFilePath)
        {
            SharpyAssert.Assert(!string.IsNullOrEmpty(t_sTextureFilePath), "Texture file path not specified");
            var texture = new Texture();
            try
            {
                texture.m_rgbData = File.ReadAllBytes(t_sTextureFilePath);
            }
            catch (Exception ex)
            {
                Log.Error($"Could not load shader from path '{t_sTextureFilePath}'", ex);
            }
            return texture;
        }

        #endregion

    }

}
