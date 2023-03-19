#version 330 core
out vec4 FragColor;

in vec2 texCoord;

uniform sampler2D texture1;
uniform sampler2D texture2;

void main()
{
     vec4 col = texture(texture2, texCoord);
     FragColor = texture(texture1, texCoord) + col;
}