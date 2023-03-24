#version 330 core
out vec4 FragColor;

in vec2 texCoord;

struct Material {

    sampler2D diffuse;
};

uniform Material material;

void main()
{
     //vec4 col = texture(texture2, texCoord);
     FragColor = texture(material.diffuse, texCoord);
}