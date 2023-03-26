#version 330 core
layout (location = 0) in vec3 a_Position;
layout(location = 1) in vec2 a_TexCoord;
layout(location = 2) in vec3 a_Normal;
layout(location = 3) in vec3 a_BiTan;
layout(location = 4) in vec3 a_Tan;

struct PointLight {
    vec3 position;

    float constant;
    float linear;
    float quadratic;

    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};
//We have a total of 4 point lights now, so we define a preprossecor directive to tell the gpu the size of our point light array
#define NR_POINT_LIGHTS 4
uniform PointLight pointLights[NR_POINT_LIGHTS];

out VS_OUT {
    vec3 FragPos;
    vec2 TexCoords;
    vec3 TangentLightPos;
    vec3 TangentViewPos;
    vec3 TangentFragPos;
} vs_out;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

uniform vec3 lightPos;
uniform vec3 viewPos;

void main()
{
    vs_out.FragPos = vec3(model * vec4(a_Position, 1.0));   
    vs_out.TexCoords = a_TexCoord;

    mat3 normalMatrix = transpose(inverse(mat3(model)));
    vec3 T = normalize(normalMatrix * a_Tan);
    vec3 N = normalize(normalMatrix * a_Normal);
    vec3 B = normalize(normalMatrix * a_BiTan);

    mat3 TBN = transpose(mat3(T, B, N));
    
    // turn tanlightPos into
    vs_out.TangentLightPos = TBN * lightPos;
    vs_out.TangentViewPos  = TBN * viewPos;
    vs_out.TangentFragPos  = TBN * vs_out.FragPos;

    // Then all you have to do is multiply the vertices by the transformation matrix, and you'll see your transformation in the scene!
   gl_Position = vec4(a_Position, 1.0) * model * view * projection;
}