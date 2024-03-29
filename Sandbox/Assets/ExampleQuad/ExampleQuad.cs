﻿using Sharpy.Rendering;
using Silk.NET.Vulkan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Assets.ExampleQuad
{
    internal class ExampleQuad : RenderableObjectBase
    {

        #region RenderableObjectBase implmementation

        protected override void InitInternal()
        {
            float[] rgfVertices =
            {
            //   a_Position-------  a_Color---------------  a_txCoords
                 0.5f,  0.5f, 0.0f, 0.8f, 0.2f, 0.8f, 1.0f, 1.0f, 1.0f,
                 0.5f, -0.5f, 0.0f, 0.2f, 0.3f, 0.8f, 1.0f, 1.0f, 0.0f,
                -0.5f, -0.5f, 0.0f, 0.2f, 0.8f, 0.8f, 1.0f, 0.0f, 0.0f,
                -0.5f,  0.5f, 0.5f, 0.8f, 0.8f, 0.2f, 1.0f, 0.0f, 1.0f
            };
            SetVertices(rgfVertices);

            uint[] rgunIndices =
            {
                0u, 1u, 3u,
                1u, 2u, 3u
            };
            SetIndices(rgunIndices);

            var shdrVertex = Shader.CreateFromFile(@"Assets\ExampleQuad\ExampleQuad.vert");
            shdrVertex.AddAttributes(
                new ShaderAttribute(ShaderAttribute.DataType.Float3, "a_Position"),
                new ShaderAttribute(ShaderAttribute.DataType.Float4, "a_Color"),
                new ShaderAttribute(ShaderAttribute.DataType.Float2, "a_txCoords")
            );
            AppendShader(ShaderType.VertexShader, shdrVertex);

            var shdrFragment = Shader.CreateFromFile(@"Assets\ExampleQuad\ExampleQuad.frag");
            shdrFragment.AddUniforms(
                new ShaderUniform("u_Texture", 0)
            );
            AppendShader(ShaderType.FragmentShader, shdrFragment);

            var tx = Texture.CreateFromFile(@"Assets\ExampleQuad\ExampleQuadTx.png");
            AppendTexture(tx);
        }

        #endregion

    }
}
