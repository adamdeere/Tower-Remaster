using TowerRemaster.Components.Interfaces;
using TowerRemaster.GameObjects;

namespace TowerRemaster.Components
{
    internal class ComponentCamera : IComponent
    {
        private readonly float m_Speed;
        private readonly float m_Sensitivity;
        private readonly CameraObject m_CameraObject;

        public ComponentCamera(CameraObject cam, float speed, float sensitivity)
        {
            m_CameraObject = cam;
            m_Sensitivity = sensitivity;
            m_Speed = speed;
        }

        public ComponentTypes ComponentType => ComponentTypes.COMPONENT_CAMERA;
        public float Speed => m_Speed;
        public float Sensitivity => m_Sensitivity;
        public CameraObject CameraObject => m_CameraObject;
    }
}