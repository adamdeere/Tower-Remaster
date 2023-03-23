using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using TowerRemaster.Components;
using TowerRemaster.Components.Interfaces;
using TowerRemaster.GameObjects;
using TowerRemaster.Managers;
using TowerRemaster.Systems.Interfaces;
using TowerRemaster.Utility;

namespace TowerRemaster.Systems.RenderSystems
{
    internal class SystemRenderMaterial : IRenderSystems
    {
        private readonly Vector3 _lightPos = new Vector3(1.2f, 1.0f, 2.0f);
        private readonly Shader shader;
        public string Name => "SystemRenderColour";

        private const ComponentTypes MASK =
              ComponentTypes.COMPONENT_TRANSFORM
            | ComponentTypes.COMPONENT_MODEL;

        public SystemRenderMaterial()
        {
            shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
            ShaderManager.shaderDictionary.Add("pbr", shader);
        }

        public void OnAction(EntityManager entityManager)
        {
            CameraObject camera = entityManager.CurrentCam;
            shader.Use();
            shader.SetInt("texture1", 0);
            foreach (var entity in entityManager.Entities())
            {
                if ((entity.Mask & MASK) == MASK)
                {
                    ComponentModel? modelComp = entity.FindComponent(ComponentTypes.COMPONENT_MODEL) as ComponentModel;
                    ComponentTransform? transformComp = entity.FindComponent(ComponentTypes.COMPONENT_TRANSFORM) as ComponentTransform;
                    ComponentMaterial? matComp = entity.FindComponent(ComponentTypes.COMPONENT_MATERIAL) as ComponentMaterial;

                    Vector3? position = transformComp?.Position;
                    Vector3? rotation = transformComp?.Rotation;
                    Vector3? scale = transformComp?.Scale;

                    var model = Matrix4.Identity;
                    if (position != null && rotation != null && scale != null)
                    {
                        model *= Matrix4.CreateScale(scale.Value.X, scale.Value.Y, scale.Value.Z);
                        model *= Matrix4.CreateRotationX(MathHelper.DegreesToRadians(rotation.Value.X));
                        model *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(rotation.Value.Y));
                        model *= Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation.Value.Z));

                        model *= Matrix4.CreateTranslation(position.Value.X, position.Value.Y, position.Value.Z);
                    }

                    Matrix4 mvp = model * camera.GetViewMatrix() * camera.GetProjectionMatrix();
                    shader.SetMatrix4("mvp", mvp);

                    matComp?.MatHandle.SetMaterial(shader);
                    modelComp?.ModelHandle.DrawMesh();
                }
            }
            GL.UseProgram(0);
        }
    }
}