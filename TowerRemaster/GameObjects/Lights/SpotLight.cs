using OpenTK.Mathematics;
using TowerRemaster.Utility;

namespace TowerRemaster.GameObjects.Lights
{
    internal class SpotLight : ILightObject
    {
        public string Type => "SpotLight";

        public void SetLights(Shader shader, CameraObject cam)
        {
            // Spot light
            shader.SetVector3("spotLight.position", cam.Position);
            shader.SetVector3("spotLight.direction", cam.Front);
            shader.SetVector3("spotLight.ambient", new Vector3(0));
            shader.SetVector3("spotLight.diffuse", new Vector3(1));
            shader.SetVector3("spotLight.specular", new Vector3(1));
            shader.SetFloat("spotLight.constant", 1.0f);
            shader.SetFloat("spotLight.linear", 0.09f);
            shader.SetFloat("spotLight.quadratic", 0.032f);
            shader.SetFloat("spotLight.cutOff", MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
            shader.SetFloat("spotLight.outerCutOff", MathF.Cos(MathHelper.DegreesToRadians(17.5f)));
        }
    }
}