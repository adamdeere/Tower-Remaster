using OpenTK.Graphics.OpenGL4;

namespace TowerRemaster.Utility
{
    internal class Texture
    {
        private readonly int Handle;
        private readonly string Path;

        public Texture(int glHandle, string path)
        {
            Handle = glHandle;
            Path = path;
        }

        public void Use(TextureUnit unit)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, Handle);
        }

        public string GetPath
        {
            get { return Path; }
        }
    }
}