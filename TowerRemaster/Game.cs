using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using TowerRemaster.Components;
using TowerRemaster.GameObjects;
using TowerRemaster.Managers;
using TowerRemaster.Systems.InputSystems;
using TowerRemaster.Systems.RenderSystems;
using TowerRemaster.Utility;

namespace TowerRemaster
{
    internal class Game : GameWindow
    {
        private Model _model;
        private readonly CameraObject _camera;

        private bool _firstMove = true;

        private Vector2 _lastPos;

        private readonly EntityManager m_EntityManager;
        private readonly SystemManager m_SystemManager;

        public Game(int width, int height, string title)
            : base(GameWindowSettings.Default, new NativeWindowSettings()
            { Size = (width, height), Title = title })
        {
            _camera = new CameraObject(Vector3.UnitZ * 3, Size.X / (float)Size.Y);
            m_EntityManager = new EntityManager(_camera);
            m_SystemManager = new SystemManager();
        }

        private void CreateEntites()
        {
            Vector3 rot = new Vector3(0);
            Vector3 scale = new Vector3(1);
            Vector3 pos = new Vector3(0, 0, 0);

            string two = "Resources/container.png";
            string one = "Assets/Textures/Sphere/Doom/Doom_Sphere_Base_color.png";

            Entity newEntity;

            newEntity = new Entity("Doom");
            newEntity.AddComponent(new ComponentModel(_model));
            newEntity.AddComponent(new ComponentTransform(pos, rot, scale));
            newEntity.AddComponent(new ComponentMaterial(new Material(one, two)));
            m_EntityManager.AddEntity(newEntity);

            // newEntity = new Entity("MainCam");
            // newEntity.AddComponent(new ComponentModel(_model));
            // newEntity.AddComponent(new ComponentTransform(pos, rot, scale));
            // newEntity.AddComponent(new ComponentMaterial(new Material(one, two)));
            // m_EntityManager.AddEntity(newEntity);
        }

        private void CreateSystems()
        {
            // add render system
            m_SystemManager.AddRenderSystem(new SystemRender());
            m_SystemManager.AddInputSystem(new SystemInput());
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            _model = ModelLoader.LoadFromFile("Sphere.txt");
            CreateEntites();
            CreateSystems();
            // We make the mouse cursor invisible and captured so we can have proper FPS-camera movement.
            // CursorState = CursorState.Grabbed;
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            var input = KeyboardState;
            // m_SystemManager.ActionInputSystems(m_EntityManager, input);
            m_SystemManager.ActionUpdateSystems(m_EntityManager, (float)e.Time);
            if (!IsFocused) // Check to see if the window is focused
                return;

            if (input.IsKeyDown(Keys.Escape))
                Close();

            const float cameraSpeed = 1.5f;
            const float sensitivity = 0.2f;

            if (input.IsKeyDown(Keys.W))
            {
                _camera.Position += _camera.Front * cameraSpeed * (float)e.Time; // Forward
            }

            if (input.IsKeyDown(Keys.S))
            {
                _camera.Position -= _camera.Front * cameraSpeed * (float)e.Time; // Backwards
            }
            if (input.IsKeyDown(Keys.A))
            {
                _camera.Position -= _camera.Right * cameraSpeed * (float)e.Time; // Left
            }
            if (input.IsKeyDown(Keys.D))
            {
                _camera.Position += _camera.Right * cameraSpeed * (float)e.Time; // Right
            }
            if (input.IsKeyDown(Keys.Space))
            {
                _camera.Position += _camera.Up * cameraSpeed * (float)e.Time; // Up
            }
            if (input.IsKeyDown(Keys.LeftShift))
            {
                _camera.Position -= _camera.Up * cameraSpeed * (float)e.Time; // Down
            }
            // Get the mouse state
            var mouse = MouseState;

            if (_firstMove) // This bool variable is initially set to true.
            {
                _lastPos = new Vector2(mouse.X, mouse.Y);
                _firstMove = false;
            }
            else
            {
                // Calculate the offset of the mouse position
                var deltaX = mouse.X - _lastPos.X;
                var deltaY = mouse.Y - _lastPos.Y;
                _lastPos = new Vector2(mouse.X, mouse.Y);

                // Apply the camera pitch and yaw (we clamp the pitch in the camera class)
                _camera.Yaw += deltaX * sensitivity;
                _camera.Pitch -= deltaY * sensitivity; // Reversed since y-coordinates range from bottom to top
            }
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            _camera.Fov -= e.OffsetY;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Enable(EnableCap.DepthTest);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            m_SystemManager.ActionRenderSystems(m_EntityManager);
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

            _model.DisposeModel();
        }
    }
}