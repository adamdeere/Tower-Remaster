﻿using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using TowerRemaster.Components;
using TowerRemaster.Components.Interfaces;
using TowerRemaster.GameObjects;
using TowerRemaster.GameObjects.Materials;
using TowerRemaster.Managers;
using TowerRemaster.Systems.Interfaces;
using TowerRemaster.Utility;

namespace TowerRemaster.Systems.RenderSystems
{
    internal class SystemRenderPbrMaterial : IRenderSystems
    {
        private readonly Shader shader;
        public string Name => "SystemRenderPbrMaterial";
        private CameraObject? camera;

        private List<Entity> m_Entities = new List<Entity>();

        private const ComponentTypes MASK =
              ComponentTypes.COMPONENT_TRANSFORM
            | ComponentTypes.COMPONENT_MODEL
            | ComponentTypes.COMPONENT_MATERIAL;

        public SystemRenderPbrMaterial()
        {
            shader = new Shader("Shaders/pbr.vert", "Shaders/pbr.frag");
        }

        public void OnAction(EntityManager entityManager)
        {
            shader.Use();
            foreach (var entity in m_Entities)
            {
                if (entity.FindComponent(ComponentTypes.COMPONENT_MODEL) is ComponentModel modelComp
                    && entity.FindComponent(ComponentTypes.COMPONENT_TRANSFORM) is ComponentTransform transformComp
                    && entity.FindComponent(ComponentTypes.COMPONENT_MATERIAL) is ComponentMaterial matComp)
                {
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
                    if (camera != null)
                    {
                        shader.SetMatrix4("view", camera.GetViewMatrix());
                        shader.SetMatrix4("projection", camera.GetProjectionMatrix());
                    }
                    matComp?.MatHandle.SetMaterial(shader);
                    modelComp?.ModelHandle.DrawMesh();
                }
            }
            GL.UseProgram(0);
        }

        public void OnLoad(EntityManager entityManager)
        {
            foreach (var entity in entityManager.Entities())
            {
                if (entity.Name == "MainCam")
                {
                    if (entity.FindComponent(ComponentTypes.COMPONENT_CAMERA) is ComponentCamera cam)
                    {
                        camera = cam.CameraObject;
                    }
                }
                if ((entity.Mask & MASK) == MASK)
                {
                    if (entity.FindComponent(ComponentTypes.COMPONENT_MATERIAL) is ComponentMaterial matComp)
                    {
                        if (matComp.MatHandle is PbrMaterial)
                        {
                            m_Entities.Add(entity);
                        }
                    }
                }
            }
        }
    }
}