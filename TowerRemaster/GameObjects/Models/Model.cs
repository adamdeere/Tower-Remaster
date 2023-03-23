using TowerRemaster.Utility;

namespace TowerRemaster.GameObjects.Models
{
    internal class Model
    {
        private readonly MeshObject[] m_Geometry;

        public Model(string fileName, string shaderType)
        {
            m_Geometry = ModelLoader.ProcessGeometryArray(fileName, shaderType);
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
            foreach (var mesh in m_Geometry)
            {
                mesh.DisposeGeometry();
            }
        }
    }
}