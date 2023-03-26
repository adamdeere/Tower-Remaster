﻿#version 330 core
out vec4 FragColor;

in vec2 texCoord;

in VS_OUT {
    vec3 FragPos;
    vec2 TexCoords;
    vec3 TangentLightPos;
    vec3 TangentViewPos;
    vec3 TangentFragPos;
} fs_in;


struct Material {

    sampler2D diffuseMap;
    sampler2D normalMap;
};

uniform Material material;

vec3 CalcPointLight(vec3 normal, vec3 color);

void main()
{
     // obtain normal from normal map in range [0,1]
    vec3 normal = texture(material.normalMap, fs_in.TexCoords).rgb;
    // transform normal vector to range [-1,1]
    normal = normalize(normal * 2.0 - 1.0);  // this normal is in tangent space
   
    // get diffuse color
    vec3 color = texture(material.diffuseMap, fs_in.TexCoords).rgb;
    // ambient
    vec3 result = CalcPointLight(normal, color);
    FragColor = vec4(result, 1.0);
}
vec3 CalcPointLight(vec3 normal, vec3 color)
{
    // ambient
    vec3 ambient = 0.1 * color;
    // diffuse
    vec3 lightDir = normalize(fs_in.TangentLightPos - fs_in.TangentFragPos);
    float diff = max(dot(normal, lightDir), 0.0);
    vec3 diffuse = diff * color;
    // specular
    vec3 viewDir = normalize(fs_in.TangentViewPos - fs_in.TangentFragPos);
    vec3 reflectDir = reflect(-lightDir, normal);
    vec3 halfwayDir = normalize(lightDir + viewDir);  
    float spec = pow(max(dot(normal, halfwayDir), 0.0), 32.0);

    vec3 specular = vec3(0.2) * spec;
    return (ambient + diffuse + specular);
}
