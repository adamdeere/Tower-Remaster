﻿using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using TowerRemaster.Components;
using TowerRemaster.GameObjects;
using TowerRemaster.GameObjects.Lights;
using TowerRemaster.GameObjects.Materials;
using TowerRemaster.GameObjects.Models;
using TowerRemaster.Managers;
using TowerRemaster.Systems.InputSystems;
using TowerRemaster.Systems.RenderSystems;
using TowerRemaster.Utility;

namespace TowerRemaster
{
    internal class Game : GameWindow
    {
        private readonly EntityManager m_EntityManager;
        private readonly SystemManager m_SystemManager;

        private readonly Vector3[] _pointLightPositions =
        {
            new Vector3(0.7f, 0.2f, 2.0f),
            new Vector3(2.3f, -3.3f, -4.0f),
            new Vector3(-4.0f, 2.0f, -1),
            new Vector3(0.0f, 0.0f, -3.0f)
        };

        public Game(int width, int height, string title)
            : base(GameWindowSettings.Default, new NativeWindowSettings()
            { Size = (width, height), Title = title })
        {
            m_EntityManager = new EntityManager();
            m_SystemManager = new SystemManager();
        }

        private void CreateLights()
        {
            m_EntityManager.AddLight(new DirectionalLight());
            m_EntityManager.AddLight(new SpotLight());
            m_EntityManager.AddLight(new PointLight(_pointLightPositions));
        }

        private void CreateEntites()
        {
            Vector3 rot = new Vector3(0);
            Vector3 scale = new Vector3(1);
            Vector3 pos = new Vector3(0);

            string one = "Assets/Textures/Backpack/";

            Entity newEntity;

            newEntity = new Entity("Backpack");
            newEntity.AddComponent(new ComponentModel(new Model("Assets/Models/backpackFBX.fbx")));
            newEntity.AddComponent(new ComponentTransform(pos, rot, scale));
            newEntity.AddComponent(new ComponentMaterial(new SpecularMaterial(one)));
            m_EntityManager.AddEntity(newEntity);

            const float cameraSpeed = 1.5f;
            const float sensitivity = 0.2f;
            newEntity = new Entity("MainCam");
            newEntity.AddComponent(new ComponentCamera(new CameraObject(Vector3.UnitZ * 3, Size.X / (float)Size.Y), cameraSpeed, sensitivity));
            newEntity.AddComponent(new ComponentInput());

            m_EntityManager.AddEntity(newEntity);
        }

        private void CreateSystems()
        {
            // add render system
            m_SystemManager.AddRenderSystem(new SystemRenderMaterial());
            m_SystemManager.AddInputSystem(new SystemKeyboardInput());
            m_SystemManager.AddInputSystem(new SystemMouseInput());
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            CreateSystems();
            CreateEntites();
            CreateLights();
            // We make the mouse cursor invisible and captured so we can have proper FPS-camera movement.
            // CursorState = CursorState.Grabbed;
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            if (!IsFocused) // Check to see if the window is focused
                return;
            var input = KeyboardState;
            var mouse = MouseState;

            if (input != null && mouse != null)
            {
                if (input.IsKeyDown(Keys.Escape))
                    Close();

                m_SystemManager.ActionInputSystems(m_EntityManager, input, mouse, (float)e.Time);
                m_SystemManager.ActionUpdateSystems(m_EntityManager, (float)e.Time);

            }
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
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
            ModelLoader.DisposeModels();
        }
    }
}