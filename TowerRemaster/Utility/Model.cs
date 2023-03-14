using OpenTK.Graphics.OpenGL4;

namespace TowerRemaster.Utility
{
    internal class Model
    {
        private readonly int _vao;
        private readonly int _count;

        public Model(int vao, int count)
        {
            _vao = vao;
            _count= count;
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
    }
}
