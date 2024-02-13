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

        public override void Init()
        {
            float[] rgfVertices =
            {
                 0.5f,  0.5f, 0.0f,
                 0.5f, -0.5f, 0.0f,
                -0.5f, -0.5f, 0.0f,
                -0.5f,  0.5f, 0.5f
            };

            uint[] rgunIndices =
            {
                0u, 1u, 3u,
                1u, 2u, 3u
            };

            var shdrVertex = new Shader();
            shdrVertex.m_sSource = @"
#version 330 core

layout (location = 0) in vec3 a_Position;

out vec3 v_Position;
        
void main()
{
    v_Position = a_Position;
    gl_Position = vec4(a_Position, 1.0);
}
";

            shdrVertex.AddAttribute(new ShaderAttribute("a_Position", 0, 3));

            var shdrFragment = new Shader();
            shdrFragment.m_sSource = @"
#version 330 core

layout (location = 0) out vec4 color;

in vec3 v_Position;

void main()
{
    color = vec4(v_Position * 0.5 + 0.5, 1.0f);
}
";
            Init(rgfVertices, rgunIndices, shdrVertex, shdrFragment);
        }

        #endregion

    }
}
