using OpenTK.Windowing.GraphicsLibraryFramework;
using TowerRemaster.Managers;

namespace TowerRemaster.Systems.Interfaces
{
    internal interface IInputSystems
    {
        void OnAction(EntityManager entityManager, KeyboardState key, MouseState mouse, float dt);

        // Property signatures:
        string Name
        {
            get;
        }
    }
}