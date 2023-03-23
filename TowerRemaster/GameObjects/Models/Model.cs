using OpenTK.Graphics.OpenGL4;
using TowerRemaster.Utility;

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

        private readonly MeshObject[] m_Geometry;

        public Model(string fileName, string shaderType)
        {
            m_Geometry = ModelLoader.ProcessGeometryArray(fileName, shaderType);
        }

        public void DrawModel()
        {
            GL.BindVertexArray(_vao);
            GL.DrawElements(PrimitiveType.Triangles, _count, DrawElementsType.UnsignedInt, 0);
        }
        public void DrawMesh()
        {
            foreach (var mesh in m_Geometry)
            {
                mesh.DrawGeometry();
            }
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