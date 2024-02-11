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
            shdrVertex.Source = @"
#version 330 core

layout (location = 0) in vec4 vPos;
        
void main()
{
    gl_Position = vec4(vPos.x, vPos.y, vPos.z, 1.0);
}
";

            var shdrFragment = new Shader();
            shdrFragment.Source = @"
#version 330 core

out vec4 FragColor;

void main()
{
    FragColor = vec4(1.0f, 0.5f, 0.2f, 1.0f);
}
";
            Init(rgfVertices, rgunIndices, shdrVertex, shdrFragment);
        }

        #endregion

    }
}
