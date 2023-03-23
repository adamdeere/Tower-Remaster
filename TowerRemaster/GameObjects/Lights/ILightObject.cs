using TowerRemaster.Utility;

namespace TowerRemaster.GameObjects.Lights
{
    internal interface ILightObject
    {
        void SetLights(Shader shader, CameraObject cam);

        // Property signatures:
        string Type
        {
            get;
        }
    }
}