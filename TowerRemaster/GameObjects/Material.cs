using OpenTK.Graphics.OpenGL4;
using TowerRemaster.Utility;

namespace TowerRemaster.GameObjects
{
    internal class Material
    {
        // For documentation on this, check Texture.cs.
        private readonly Texture m_Diffuse;
        private readonly Texture m_Normal;
        private readonly Texture m_Metalic;
        private readonly Texture m_Roughness;
        private readonly Texture m_Height;

        // private readonly Texture _textureTwo;

        public Material(string one)
        { 
            m_Diffuse = TextureLoader.LoadFromFile(one + "Doom_Sphere_Base_color.png");
            m_Normal = TextureLoader.LoadFromFile(one + "Doom_Sphere_Normal_OpenGL.png");
            m_Metalic = TextureLoader.LoadFromFile(one + "Doom_Sphere_Metallic.png");
            m_Roughness = TextureLoader.LoadFromFile(one + "Doom_Sphere_Roughness.png");
            m_Height = TextureLoader.LoadFromFile(one + "Doom_Sphere_Height.png");
           
        }

        public void SetMaterial()
        {
            m_Diffuse.Use(TextureUnit.Texture0);
           // m_Normal.Use(TextureUnit.Texture1);
           // m_Metalic.Use(TextureUnit.Texture2);
           // m_Roughness.Use(TextureUnit.Texture3);
           // m_Height.Use(TextureUnit.Texture4);
        }
    }
}