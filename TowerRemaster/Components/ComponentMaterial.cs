using TowerRemaster.Components.Interfaces;
using TowerRemaster.GameObjects;

namespace TowerRemaster.Components
{
    internal class ComponentMaterial : IComponent
    {
        public ComponentTypes ComponentType => ComponentTypes.COMPONENT_MATERIAL;

        private Material m_Mat;

        public ComponentMaterial(Material mat)
        {
            m_Mat = mat;
        }

        public Material MatHandle
        {
            get { return m_Mat; }
            set { m_Mat = value; }
        }
    }
}