#version 330 core
layout (location = 0) in vec3 a_Position;
layout(location = 1) in vec2 a_TexCoord;
layout(location = 2) in vec3 a_Normal;
layout(location = 3) in vec3 a_BiTan;
layout(location = 4) in vec3 a_Tan;

out vec2 texCoord;
out vec3 normal;
out vec3 FragPos;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
   gl_Position = vec4(a_Position, 1.0) * model * view * projection;
   FragPos = vec3(vec4(a_Position, 1.0) * model);
   normal = a_Normal * mat3(transpose(inverse(model)));
   texCoord = a_TexCoord;
}