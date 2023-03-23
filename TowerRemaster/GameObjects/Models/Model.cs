using OpenTK.Graphics.OpenGL4;

namespace TowerRemaster.GameObjects.Models
{
    internal class Model
    {
        private readonly int _vao;
        private readonly int _count;

        public Model(int vao, int count)
        {
            _vao = vao;
            _count = count;
        }

        private readonly MeshObject[]? m_Geometry;

        public Model(MeshObject[] geometry)
        {
            m_Geometry = geometry;
        }

        public void DrawModel()
        {
            GL.BindVertexArray(_vao);
            GL.DrawElements(PrimitiveType.Triangles, _count, DrawElementsType.UnsignedInt, 0);
        }

        public void DisposeModel()
        {
            GL.DeleteVertexArray(_vao);
        }

        public MeshObject[] GetMeshes
        {
            get { return m_Geometry; }
        }

        public int VaoHandle
        {
            get { return _vao; }
        }
    }
}