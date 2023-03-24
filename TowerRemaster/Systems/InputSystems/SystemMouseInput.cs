using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using TowerRemaster.Components;
using TowerRemaster.Components.Interfaces;
using TowerRemaster.GameObjects;
using TowerRemaster.Managers;
using TowerRemaster.Systems.Interfaces;

namespace TowerRemaster.Systems.InputSystems
{
    internal class SystemMouseInput : IInputSystems
    {
        public string Name => "SystemMouseInput";
        private bool _firstMove = true;
        private Vector2 _lastPos;

        private const ComponentTypes MASK =
             ComponentTypes.COMPONENT_CAMERA |
             ComponentTypes.COMPONENT_INPUT;

        public void OnAction(EntityManager entityManager, KeyboardState key, MouseState mouse, float dt)
        {
            foreach (var entity in entityManager.Entities())
            {
                if ((entity.Mask & MASK) == MASK)
                {
                    if (entity.FindComponent(ComponentTypes.COMPONENT_CAMERA) is ComponentCamera cam)
                    {
                        CameraObject _camera = cam.CameraObject;
                        float sensitivity = cam.Sensitivity;
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
                }
            }
        }
    }
}