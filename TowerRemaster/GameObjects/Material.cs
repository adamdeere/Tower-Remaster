using OpenTK.Graphics.OpenGL4;
using TowerRemaster.Utility;

namespace TowerRemaster.GameObjects
{
    internal class Material
    {
        // For documentation on this, check Texture.cs.
        private readonly Texture _texture;

        private readonly Texture _textureTwo;

        public Material(string one, string two)
        {
            _texture = TextureLoader.LoadFromFile(one);
            _textureTwo = TextureLoader.LoadFromFile(two);
        }

        public void SetMaterial()
        {
            _texture.Use(TextureUnit.Texture0);
            _textureTwo.Use(TextureUnit.Texture1);
        }
    }
}