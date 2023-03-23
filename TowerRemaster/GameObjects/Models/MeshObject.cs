using OpenTK.Graphics.OpenGL;

namespace TowerRemaster.GameObjects.Models
{
    internal class MeshObject
    {
        private readonly int m_VAO;
        private readonly int m_TriangleCount;

        public MeshObject(int vao, int triangleCount)
        {
            m_VAO = vao;
            m_TriangleCount = triangleCount;
        }

        public void DrawGeometry()
        {
            GL.BindVertexArray(m_VAO);
            GL.DrawElements(PrimitiveType.Triangles, m_TriangleCount, DrawElementsType.UnsignedInt, 0);
        }
    }
}