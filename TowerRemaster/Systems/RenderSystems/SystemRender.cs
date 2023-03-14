using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using TowerRemaster.Components.Interfaces;
using TowerRemaster.GameObjects;
using TowerRemaster.Managers;
using TowerRemaster.Systems.Interfaces;

namespace TowerRemaster.Systems.RenderSystems
{
    internal class SystemRender : IRenderSystems
    {
        public string Name => "SystemRenderColour";

        private const ComponentTypes MASK =
              ComponentTypes.COMPONENT_TRANSFORM
            | ComponentTypes.COMPONENT_MODEL;

        public SystemRender()
        {
            pgmID = GL.CreateProgram();
            LoadShader("Shaders/vs.glsl", ShaderType.VertexShader, pgmID, out vsID);
            LoadShader("Shaders/fs.glsl", ShaderType.FragmentShader, pgmID, out fsID);
            GL.LinkProgram(pgmID);
            Console.WriteLine(GL.GetProgramInfoLog(pgmID));

            uniform_mview = GL.GetUniformLocation(pgmID, "WorldViewProj");

            if (uniform_mview == -1)
            {
                Console.WriteLine("Error binding attributes");
            }
        }

        public void OnAction(EntityManager entityManager)
        {
            CameraObject camera = entityManager.CurrentCam;
            foreach (var entity in entityManager.Entities())
            {
                if ((entity.Mask & MASK) == MASK)
                {
                    IComponent modelComponent = components.Find(delegate (IComponent component)
                    {
                        return component.ComponentType == ComponentTypes.COMPONENT_MODEL;
                    });

                    int vao = ((ComponentModel)modelComponent).VaoHandle;

                    IComponent transformComponent = components.Find(delegate (IComponent component)
                    {
                        return component.ComponentType == ComponentTypes.COMPONENT_TRANSFORM;
                    });
                    Vector2 position = ((ComponentTransform)transformComponent).Position;
                    Draw(vao, position, camera);
                }
            }
            GL.UseProgram(0);
        }

        private void Draw(int vao_Handle, Vector2 pos, CameraObject cam)
        {
        }
    }
}