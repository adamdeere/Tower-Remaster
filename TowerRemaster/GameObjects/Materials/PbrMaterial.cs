using OpenTK.Graphics.OpenGL4;
using TowerRemaster.Utility;

namespace TowerRemaster.GameObjects.Materials
{
    internal class PbrMaterial : IMaterial
    {
        private readonly Texture m_DiffuseMap;

        public PbrMaterial(string fileName)
        {
            m_DiffuseMap = TextureLoader.LoadFromFile(fileName);
        }

        public string Name => "PbrMaterial";

        public void SetMaterial(Shader shader)
        {
            m_DiffuseMap.Use(TextureUnit.Texture0);
            shader.SetInt("material.diffuse", 0);
        }
    }
}