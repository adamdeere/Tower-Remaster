using OpenTK.Mathematics;
using TowerRemaster.Components.Interfaces;

namespace TowerRemaster.Components
{
    internal class ComponentTransform : IComponent
    {
        private Vector3 m_Position;
        private Vector3 m_Rotaion;
        private Vector3 m_Scale;

        public ComponentTypes ComponentType => ComponentTypes.COMPONENT_TRANSFORM;

        public ComponentTransform(Vector3 pos, Vector3 rot, Vector3 scale)
        {
            m_Position = pos;
            m_Rotaion = rot;
            m_Scale = scale;
        }

        public Vector3 Position
        {
            get { return m_Position; }
            set { m_Position = value; }
        }

        public Vector3 Rotation
        {
            get { return m_Rotaion; }
            set { m_Rotaion = value; }
        }

        public Vector3 Scale
        {
            get { return m_Scale; }
            set { m_Scale = value; }
        }
    }
}