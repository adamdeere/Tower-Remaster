using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using TowerRemaster.Components;
using TowerRemaster.Components.Interfaces;
using TowerRemaster.GameObjects;
using TowerRemaster.Managers;
using TowerRemaster.Systems.Interfaces;

namespace TowerRemaster.Systems.InputSystems
{
    internal class SystemKeyboardInput : IInputSystems
    {
        public string Name => "SystemKeyboardInput";

        private const ComponentTypes MASK =
             ComponentTypes.COMPONENT_CAMERA |
             ComponentTypes.COMPONENT_INPUT;

        private Vector3 MoveCam(Vector3 direction, float speed, float dt)
        {
            return direction * speed * dt;
        }

        public void OnAction(EntityManager entityManager, KeyboardState input, MouseState mouse, float dt)
        {
            foreach (var entity in entityManager.Entities())
            {
                if ((entity.Mask & MASK) == MASK)
                {
                    if (entity.FindComponent(ComponentTypes.COMPONENT_CAMERA) is ComponentCamera cam)
                    {
                        CameraObject _camera = cam.CameraObject;
                        if (input.IsKeyDown(Keys.W))
                        {
                            cam.CameraObject.Position += MoveCam(_camera.Front, cam.Speed, dt); // Forward
                        }
                        if (input.IsKeyDown(Keys.S))
                        {
                            _camera.Position -= MoveCam(_camera.Front, cam.Speed, dt); // Backwards
                        }
                        if (input.IsKeyDown(Keys.A))
                        {
                            _camera.Position -= MoveCam(_camera.Right, cam.Speed, dt);// Left
                        }
                        if (input.IsKeyDown(Keys.D))
                        {
                            _camera.Position += MoveCam(_camera.Right, cam.Speed, dt); // Right
                        }
                        if (input.IsKeyDown(Keys.Space))
                        {
                            _camera.Position += MoveCam(_camera.Up, cam.Speed, dt);// Up
                        }
                        if (input.IsKeyDown(Keys.LeftShift))
                        {
                            _camera.Position -= MoveCam(_camera.Up, cam.Speed, dt); // Down
                        }
                    }
                }
            }
        }
    }
}