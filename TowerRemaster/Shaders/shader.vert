﻿#version 330 core
layout (location = 0) in vec3 aPosition;
layout(location = 1) in vec2 aTexCoord;

out vec2 texCoord;

uniform mat4 mvp;

void main()
{
    texCoord = aTexCoord;
    // Then all you have to do is multiply the vertices by the transformation matrix, and you'll see your transformation in the scene!
   gl_Position = vec4(aPosition, 1.0) * mvp;
}