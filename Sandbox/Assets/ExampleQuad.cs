using Sharpy.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Assets
{
    internal class ExampleQuad : RenderableObjectBase
    {

        #region RenderableObjectBase implmementation

        protected override void InitInternal()
        {
            float[] rgfVertices =
            {
                 0.5f,  0.5f, 0.0f, 0.8f, 0.2f, 0.8f, 1.0f,
                 0.5f, -0.5f, 0.0f, 0.2f, 0.3f, 0.8f, 1.0f,
                -0.5f, -0.5f, 0.0f, 0.2f, 0.8f, 0.8f, 1.0f,
                -0.5f,  0.5f, 0.5f, 0.8f, 0.8f, 0.2f, 1.0f
            };
            SetVertices(rgfVertices);

            uint[] rgunIndices =
            {
                0u, 1u, 3u,
                1u, 2u, 3u
            };
            SetIndices(rgunIndices);

            var shdrVertex = new Shader();
            shdrVertex.AddAttributes(
                new ShaderAttribute(ShaderAttribute.DataType.Float3, "a_Position"),
                new ShaderAttribute(ShaderAttribute.DataType.Float4, "a_Color")
            );


            shdrVertex.m_sSource = @"
#version 330 core

layout (location = 0) in vec3 a_Position;
layout (location = 1) in vec4 a_Color;

out vec3 v_Position;
out vec4 v_Color;
        
void main()
{
    v_Position = a_Position;
    v_Color = a_Color;
    gl_Position = vec4(a_Position, 1.0);
}
";
            AppendShader(ShaderType.VertexShader, shdrVertex);

            var shdrFragment = new Shader();
            shdrFragment.m_sSource = @"
#version 330 core

layout (location = 0) out vec4 color;

in vec3 v_Position;
in vec4 v_Color;

void main()
{
    color = vec4(v_Position * 0.5 + 0.5, 1.0f);
    color = v_Color;
}
";
            AppendShader(ShaderType.FragmentShader, shdrFragment);
        }

        #endregion

    }
}
