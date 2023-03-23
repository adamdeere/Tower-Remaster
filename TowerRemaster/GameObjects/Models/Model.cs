using TowerRemaster.Utility;

namespace TowerRemaster.GameObjects.Models
{
    internal class Model
    {
        private readonly MeshObject[] m_Geometry;

        public Model(string fileName)
        {
            m_Geometry = ModelLoader.ProcessGeometryArray(fileName);
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