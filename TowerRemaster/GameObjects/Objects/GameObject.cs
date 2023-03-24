using System.Diagnostics;
using TowerRemaster.Components.Interfaces;

namespace TowerRemaster.GameObjects.Objects
{
    internal class GameObject
    {
        private string m_Name;
        private readonly List<IComponent> m_ComponentList = new();
        private ComponentTypes mask;

        public GameObject(string name)
        {
            m_Name = name;
        }

        public IComponent FindComponent(ComponentTypes type)
        {
            IComponent? component = m_ComponentList.Find(delegate (IComponent component)
            {
                return component.ComponentType == type;
            });

            return component;
        }

        public void AddComponent(IComponent component)
        {
            Debug.Assert(component != null, "Component cannot be null");

            m_ComponentList.Add(component);
            mask |= component.ComponentType;
        }

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public ComponentTypes Mask
        {
            get { return mask; }
        }
    }
}