using TowerRemaster.Managers;

namespace TowerRemaster.Systems.Interfaces
{
    internal interface IRenderSystems
    {
        void OnAction(EntityManager entityManager);
        void OnLoad(EntityManager entityManager);

        // Property signatures:
        string Name
        {
            get;
        }
    }
}