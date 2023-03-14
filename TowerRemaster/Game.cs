using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using TowerRemaster.Utility;

namespace TowerRemaster
{
    internal class Game : GameWindow
    {
        int VertexBufferObject; 
        int VertexArrayObject;
        int ElementBufferObject;

        private Shader shader;
        // For documentation on this, check Texture.cs.
        private Texture _texture;
        private Texture _textureTwo;
        private readonly float[] vertices = {
            //Position          Texture coordinates
             0.5f,  0.5f, 0.0f, 1.0f, 1.0f, // top right
             0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f  // top left
        };
       private readonly uint[] indices = {  // note that we start from 0!
            0, 1, 3,   // first triangle
            1, 2, 3    // second triangle
        };
        public Game(int width, int height, string title)
            : base(GameWindowSettings.Default, new NativeWindowSettings()
            { Size = (width, height), Title = title })
        {
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            VertexBufferObject = GL.GenBuffer();
            VertexArrayObject = GL.GenVertexArray();

            GL.BindVertexArray(VertexArrayObject);
            // 2. copy our vertices array in a buffer for OpenGL to use
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            // 3. then set our vertex attributes pointers
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

            int texCoordLocation = shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);


            GL.EnableVertexAttribArray(0);

            _texture = Texture.LoadFromFile("Resources/container.png");
            _textureTwo = Texture.LoadFromFile("Resources/awesomeface.png");
            shader.Use();
            shader.SetInt("texture1", 0);
            shader.SetInt("texture2", 1);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            KeyboardState input = KeyboardState;

            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            
            _texture.Use(TextureUnit.Texture0);
            _textureTwo.Use(TextureUnit.Texture1);

            shader.Use();

            GL.BindVertexArray(VertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

            SwapBuffers();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, e.Width, e.Height);
        }
        protected override void OnUnload()
        {
            base.OnUnload();

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(VertexBufferObject);
        }
    }
}