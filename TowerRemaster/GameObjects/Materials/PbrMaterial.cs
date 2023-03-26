using OpenTK.Graphics.OpenGL4;
using TowerRemaster.Utility;

namespace TowerRemaster.GameObjects.Materials
{
    internal class PbrMaterial : IMaterial
    {
        private readonly Texture m_DiffuseMap;
        private readonly Texture m_NormalMap;

        public PbrMaterial(string diffuse, string normal)
        {
            m_DiffuseMap = TextureLoader.LoadFromFile(diffuse);
            m_NormalMap = TextureLoader.LoadFromFile(normal);
        }

        public string Name => "PbrMaterial";

        public void SetMaterial(Shader shader)
        {
            m_DiffuseMap.Use(TextureUnit.Texture0);
            shader.SetInt("material.diffuseMap", 0);

            m_NormalMap.Use(TextureUnit.Texture1);
            shader.SetInt("material.normalMap", 1);
        }
    }
}