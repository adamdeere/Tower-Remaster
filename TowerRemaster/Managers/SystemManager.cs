using OpenTK.Windowing.GraphicsLibraryFramework;
using TowerRemaster.Systems.Interfaces;

namespace TowerRemaster.Managers
{
    internal class SystemManager
    {
        private readonly List<IRenderSystems> m_RenderSystems;
        private readonly List<IUpdateSystems> m_UpdateSystems;
        private readonly List<IInputSystems> m_InputSystems;

        public SystemManager()
        {
            m_RenderSystems = new List<IRenderSystems>();
            m_UpdateSystems = new List<IUpdateSystems>();
            m_InputSystems = new List<IInputSystems>();
        }

        public void ActionRenderSystems(EntityManager entityManager)
        {
            foreach (var system in m_RenderSystems)
            {
                system.OnAction(entityManager);
            }
        }

        public void AddRenderSystem(IRenderSystems system)
        {
            // Debug.Assert(result != null, "System '" + system.Name + "' already exists");
            m_RenderSystems.Add(system);
        }

        public void ActionUpdateSystems(EntityManager entityManager, float dt)
        {
            foreach (var system in m_UpdateSystems)
            {
                system.OnAction(entityManager, dt);
            }
        }

        public void AddInputSystem(IInputSystems system)
        {
            // Debug.Assert(result != null, "System '" + system.Name + "' already exists");
            m_InputSystems.Add(system);
        }

        public void ActionInputSystems(EntityManager entityManager, KeyboardState state, MouseState mouse, float dt)
        {
            foreach (var system in m_InputSystems)
            {
                system.OnAction(entityManager, state, mouse, dt);
            }
        }

        public void AddUpdateSystem(IUpdateSystems system)
        {
            // Debug.Assert(result != null, "System '" + system.Name + "' already exists");
            m_UpdateSystems.Add(system);
        }

        public void OnSystemLoad(EntityManager entityManager)
        {
            foreach (var system in m_RenderSystems)
            {
                system.OnLoad(entityManager);
            }
        }
    }
}