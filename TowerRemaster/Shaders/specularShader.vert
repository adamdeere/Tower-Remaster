#version 330 core
layout (location = 0) in vec3 aPosition;
layout(location = 1) in vec2 aTexCoord;
layout(location = 2) in vec3 a_Normal;
//layout(location = 3) in vec3 a_BiTan;
//layout(location = 4) in vec3 a_Tan;

out vec2 texCoord;
out vec3 normal;
out vec3 FragPos;

uniform mat4 mvp;
uniform mat4 model;

void main()
{
    texCoord = aTexCoord;
    // Then all you have to do is multiply the vertices by the transformation matrix, and you'll see your transformation in the scene!
   gl_Position = vec4(aPosition, 1.0) * mvp;
   FragPos = vec3(vec4(aPosition, 1.0) * model);
   normal = a_Normal * mat3(transpose(inverse(model)));
}