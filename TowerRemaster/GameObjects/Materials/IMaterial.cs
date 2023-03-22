using TowerRemaster.Utility;

namespace TowerRemaster.GameObjects.Materials
{
    internal interface IMaterial
    {
        void SetMaterial(Shader shader);

        // Property signatures:
        string Name
        {
            get;
        }
    }
}