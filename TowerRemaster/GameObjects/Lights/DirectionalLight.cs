using OpenTK.Mathematics;
using TowerRemaster.Utility;

namespace TowerRemaster.GameObjects.Lights
{
    internal class DirectionalLight : ILightObject
    {
        public string Type => "DirectionalLight";

        public void SetLights(Shader shader, CameraObject cam)
        {
            // Directional light
            shader.SetVector3("dirLight.direction", new Vector3(-0.2f, -1.0f, -0.3f));
            shader.SetVector3("dirLight.ambient", new Vector3(0.05f, 0.05f, 0.05f));
            shader.SetVector3("dirLight.diffuse", new Vector3(0.4f, 0.4f, 0.4f));
            shader.SetVector3("dirLight.specular", new Vector3(0.5f, 0.5f, 0.5f));
        }
    }
}