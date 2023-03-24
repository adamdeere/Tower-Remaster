using TowerRemaster.Components.Interfaces;
using TowerRemaster.GameObjects.Materials;

namespace TowerRemaster.Components
{
    internal class ComponentMaterial : IComponent
    {
        public ComponentTypes ComponentType => ComponentTypes.COMPONENT_MATERIAL;

        private IMaterial m_Mat;

        public ComponentMaterial(IMaterial mat)
        {
            m_Mat = mat;
        }

        public IMaterial MatHandle
        {
            get { return m_Mat; }
            set { m_Mat = value; }
        }
    }
}