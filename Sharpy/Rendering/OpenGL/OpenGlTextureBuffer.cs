using Sharpy.Logging;
using Silk.NET.OpenGL;
using Silk.NET.Vulkan;
using StbImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharpy.Rendering.OpenGL
{

    /// <summary>
    /// OpenGL implementation of texture buffer
    /// </summary>
    internal class OpenGlTextureBuffer : TextureBufferBase
    {

        #region Declarations

        private readonly GL m_gl;
        private readonly uint m_unBufferId;

        #endregion


        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="t_gl">Render context</param>
        /// <param name="t_texture">Texture to buffer</param>
        /// <param name="t_unShaderProgramId">Shader program</param>
        public unsafe OpenGlTextureBuffer(GL t_gl, RenderableObjectBase t_obj) : base()
        {
            SharpyAssert.Assert(t_obj.m_texture != null, "Texture is not set");

            m_gl = t_gl;
            m_unBufferId = m_gl.GenTexture();
            m_gl.ActiveTexture(TextureUnit.Texture0);
            m_gl.BindTexture(TextureTarget.Texture2D, m_unBufferId);

            ImageResult imgTexture = ImageResult.FromMemory(t_obj.m_texture.m_rgbData, ColorComponents.RedGreenBlueAlpha);
            fixed(byte* pbyteData = imgTexture.Data)
            {
                m_gl.TexImage2D(
                    TextureTarget.Texture2D,
                    0,
                    InternalFormat.Rgba,
                    (uint)imgTexture.Width,
                    (uint)imgTexture.Height,
                    0,
                    PixelFormat.Rgba,
                    PixelType.UnsignedByte,
                    pbyteData
                );
            }

            m_gl.TextureParameter(m_unBufferId, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            m_gl.TextureParameter(m_unBufferId, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            m_gl.TextureParameter(m_unBufferId, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
            m_gl.TextureParameter(m_unBufferId, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            m_gl.GenerateMipmap(TextureTarget.Texture2D);
            m_gl.BindTexture(TextureTarget.Texture2D, 0);

            // TODO: there is got to be a better was than this!
            int nLocation = m_gl.GetUniformLocation(t_obj.m_unShaderProgramId, "u_Texture");
            m_gl.Uniform1(nLocation, 0);

            m_gl.Enable(EnableCap.Blend);
            m_gl.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }

        #endregion


        #region IndexBufferBase implementation

        public override unsafe void Bind()
        {
            m_gl.ActiveTexture(TextureUnit.Texture0);
            m_gl.BindTexture(TextureTarget.Texture2D, m_unBufferId);
        }

        public override void Unbind()
        {
            m_gl.DeleteTexture(m_unBufferId);
        }

        #endregion

    }
}
