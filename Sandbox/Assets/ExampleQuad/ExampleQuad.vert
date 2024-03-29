#version 330 core

layout (location = 0) in vec3 a_Position;
layout (location = 1) in vec4 a_Color;
layout (location = 2) in vec2 a_txCoords;

out vec3 v_Position;
out vec4 v_Color;
out vec2 v_txCoords;
        
void main()
{
    v_Position = a_Position;
    v_Color = a_Color;
    v_txCoords = a_txCoords;
    gl_Position = vec4(a_Position, 1.0);
}