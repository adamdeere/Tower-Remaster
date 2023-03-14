using TowerRemaster.Managers;

namespace TowerRemaster.Systems.Interfaces
{
    internal interface IUpdateSystems
    {
        void OnAction(EntityManager entity, float dt);

        // Property signatures:
        string Name
        {
            get;
        }
    }
}