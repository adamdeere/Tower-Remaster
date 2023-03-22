using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using TowerRemaster.Utility;

namespace TowerRemaster.GameObjects.Materials
{
    internal class SpecularMaterial : IMaterial
    {
        private float m_Shininess;
        private Vector3 m_SpecularLight;

        private readonly Texture m_DiffuseMap;
        private readonly Texture m_SpecularMap;
        private readonly Texture m_NormalMap;

        public string Name => "SpecularMaterial";

        public SpecularMaterial(string one)
        {
            m_DiffuseMap = TextureLoader.LoadFromFile(one + "Doom_Sphere_Base_color.png");
        }

        public void SetMaterial(Shader shader)
        {
            m_DiffuseMap.Use(TextureUnit.Texture0);
           // shader.SetInt("material.diffuse", 0);

           // m_SpecularMap.Use(TextureUnit.Texture1);
           // shader.SetInt("material.specular", 1);

            // m_NormalMap.Use(TextureUnit.Texture2);
            //  shader.SetInt("material.normal", 2);

           // shader.SetVector3("material.specular", m_SpecularLight);
           // shader.SetFloat("material.shininess", m_Shininess);
        }
    }
}