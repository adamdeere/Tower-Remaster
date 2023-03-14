﻿using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using TowerRemaster.Components;
using TowerRemaster.Components.Interfaces;
using TowerRemaster.GameObjects;
using TowerRemaster.Managers;
using TowerRemaster.Systems.Interfaces;
using TowerRemaster.Utility;

namespace TowerRemaster.Systems.RenderSystems
{
    internal class SystemRender : IRenderSystems
    {
        private readonly Shader shader;
        public string Name => "SystemRenderColour";

        private const ComponentTypes MASK =
              ComponentTypes.COMPONENT_TRANSFORM
            | ComponentTypes.COMPONENT_MODEL;

        public SystemRender()
        {
            shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
        }

        public void OnAction(EntityManager entityManager)
        {
            CameraObject camera = entityManager.CurrentCam;
            shader.Use();
            shader.SetInt("texture1", 0);
            shader.SetInt("texture2", 1);
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
                    model *= Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation.Value.Z));
                    model *= Matrix4.CreateScale(scale.Value.X, scale.Value.Y, scale.Value.Z);
                    model *= Matrix4.CreateTranslation(position.Value.X, position.Value.Y, position.Value.Z);

                    Matrix4 mvp = model * camera.GetViewMatrix() * camera.GetProjectionMatrix();
                    shader.SetMatrix4("mvp", mvp);

                    matComp?.MatHandle.SetMaterial();
                    modelComp?.ModelHandle.DrawModel();
                }
            }
            GL.UseProgram(0);
        }
    }
}