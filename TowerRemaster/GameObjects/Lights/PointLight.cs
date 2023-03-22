using OpenTK.Mathematics;
using TowerRemaster.Utility;

namespace TowerRemaster.GameObjects.Lights
{
    internal class PointLight : ILightObject
    {
        public string Type => "PointLight";
        private readonly Vector3[]? pointLights;

        public PointLight(Vector3[] p)
        {
            pointLights = p;
        }

        public void SetLights(Shader shader, CameraObject cam)
        {
            if (pointLights != null)
            {
                // Point lights
                for (int i = 0; i < pointLights.Length; i++)
                {
                    shader.SetVector3($"pointLights[{i}].position", pointLights[i]);
                    shader.SetVector3($"pointLights[{i}].ambient", new Vector3(0.05f, 0.05f, 0.05f));
                    shader.SetVector3($"pointLights[{i}].diffuse", new Vector3(0.8f, 0.8f, 0.8f));
                    shader.SetVector3($"pointLights[{i}].specular", new Vector3(1.0f, 1.0f, 1.0f));
                    shader.SetFloat($"pointLights[{i}].constant", 1.0f);
                    shader.SetFloat($"pointLights[{i}].linear", 0.09f);
                    shader.SetFloat($"pointLights[{i}].quadratic", 0.032f);
                }
            }
        }
    }
}