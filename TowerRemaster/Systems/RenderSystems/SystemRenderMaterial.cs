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
        private readonly Shader shader;
        public string Name => "SystemRenderColour";

        private const ComponentTypes MASK =
              ComponentTypes.COMPONENT_TRANSFORM
            | ComponentTypes.COMPONENT_MODEL;

        public SystemRenderMaterial()
        {
            shader = new Shader("Shaders/pbr.vert", "Shaders/pbr.frag");
        }

        ~SystemRenderMaterial()
        {
            shader.Dispose();
        }

        public void OnAction(EntityManager entityManager)
        {
            CameraObject camera = entityManager.CurrentCam;
            shader.Use();
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

                    shader.SetMatrix4("model", model);
                    shader.SetMatrix4("view", camera.GetViewMatrix());
                    shader.SetMatrix4("projection", camera.GetProjectionMatrix());
                    shader.SetVector3("viewPos", camera.Position);
                    matComp?.MatHandle.SetMaterial(shader);
                    entityManager.OnLightsAction(shader);
                    modelComp?.ModelHandle.DrawMesh();
                }
            }
            GL.UseProgram(0);
        }
    }
}