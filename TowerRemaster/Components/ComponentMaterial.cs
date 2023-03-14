using TowerRemaster.Components.Interfaces;

namespace TowerRemaster.Components
{
    internal class ComponentMaterial : IComponent
    {
        public ComponentTypes ComponentType => ComponentTypes.COMPONENT_MATERIAL;

        public ComponentMaterial()
        {
        }
    }
}