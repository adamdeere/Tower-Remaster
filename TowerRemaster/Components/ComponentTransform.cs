using OpenTK.Mathematics;
using TowerRemaster.Components.Interfaces;

namespace TowerRemaster.Components
{
    internal class ComponentTransform : IComponent
    {
        private Vector3 m_Position;

        public ComponentTransform(float x, float y, float z)
        {
            m_Position = new Vector3(x, y, z);
        }

        public Vector3 Position
        {
            get { return m_Position; }
            set { m_Position = value; }
        }

        public ComponentTypes ComponentType => ComponentTypes.COMPONENT_TRANSFORM;
    }
}