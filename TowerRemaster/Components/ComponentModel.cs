using TowerRemaster.Components.Interfaces;
using TowerRemaster.Utility;

namespace TowerRemaster.Components
{
    internal class ComponentModel : IComponent
    {
        private readonly Model m_Model;

        public ComponentTypes ComponentType => ComponentTypes.COMPONENT_MODEL;

        public ComponentModel(Model model)
        {
            m_Model = model;
        }

        public Model ModelHandle
        {
            get { return m_Model; }
        }
    }
}