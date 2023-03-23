using OpenTK.Graphics.OpenGL4;
using StbImageSharp;

namespace TowerRemaster.Utility
{
    internal static class TextureLoader
    {
        private static readonly Dictionary<string, Texture> m_LoadedTextures = new Dictionary<string, Texture>();

        public static Texture LoadFromFile(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException(path);

            m_LoadedTextures.TryGetValue(path, out Texture? texture);

            if (texture == null)
            {
                // Generate handle
                int handle = GL.GenTexture();

                // Bind the handle
                GL.ActiveTexture(TextureUnit.Texture0);
                GL.BindTexture(TextureTarget.Texture2D, handle);
                StbImage.stbi_set_flip_vertically_on_load(1);

                using (Stream stream = File.OpenRead(path))
                {
                    ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);

                    GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
                }
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
                Texture textureObject = new Texture(handle, path);
                m_LoadedTextures.Add(path, textureObject);
                return textureObject;
            }

            return texture;
        }
    }
}