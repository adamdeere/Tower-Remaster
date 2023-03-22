using TowerRemaster.Components.Interfaces;
using TowerRemaster.GameObjects.Materials;

namespace TowerRemaster.Components
{
    internal class ComponentMaterial : IComponent
    {
        public ComponentTypes ComponentType => ComponentTypes.COMPONENT_MATERIAL;

        private SpecularMaterial m_Mat;

        public ComponentMaterial(SpecularMaterial mat)
        {
            m_Mat = mat;
        }

        public SpecularMaterial MatHandle
        {
            get { return m_Mat; }
            set { m_Mat = value; }
        }
    }
}