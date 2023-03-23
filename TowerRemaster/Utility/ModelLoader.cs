using Assimp;
using OpenTK.Graphics.OpenGL4;
using TowerRemaster.GameObjects.Models;

namespace TowerRemaster.Utility
{
    internal static class ModelLoader
    {
        private static readonly Dictionary<string, Model> m_ModelDictionary = new Dictionary<string, Model>();

        public static void DisposeModels()
        {
            foreach (var item in m_ModelDictionary)
            {
                item.Value.DisposeModel();
            }
        }

        public static MeshObject[] ProcessGeometryArray(string fileName, string shaderType)
        {
            Scene scene;
            AssimpContext importer = new AssimpContext();
            importer.SetConfig(new Assimp.Configs.NormalSmoothingAngleConfig(66.0f));
            scene = importer.ImportFile(fileName,
                       PostProcessPreset.TargetRealTimeMaximumQuality);
            MeshObject[] m_Geometry = new MeshObject[scene.MeshCount];

            for (int i = 0; i < scene.Meshes.Count; i++)
            {
                List<Vector3D[]> vertList = new List<Vector3D[]>
                {
                        scene.Meshes[i].Vertices.ToArray(),
                        scene.Meshes[i].TextureCoordinateChannels[0].ToArray(),
                        scene.Meshes[i].Normals.ToArray(),
                        scene.Meshes[i].BiTangents.ToArray(),
                        scene.Meshes[i].Tangents.ToArray()
                };
                m_Geometry[i] = ProcessGeometry(vertList, scene.Meshes[i].GetIndices(), scene.Meshes[i].VertexCount, shaderType);
            }
            return m_Geometry;
        }

        private static MeshObject ProcessGeometry(List<Vector3D[]> vertList, int[] indices, int vertexCount, string shaderType)
        {
            List<float> vertices = new();
            for (int i = 0; i < vertexCount; i++)
            {
                //loops over a list of arrays to keep the data in order
                for (int j = 0; j < vertList.Count; j++)
                {
                    Vector3D[] vert = vertList[j];
                    Vector3D k = vert[i];
                    vertices.Add(k.X);
                    vertices.Add(k.Y);
                    //ignores the 3rd element for the tex coord as there is only two and would be a large waste of memory on larger models
                    if (j != 1)
                        vertices.Add(k.Z);
                }
            }
            float[] verts = vertices.ToArray();
            int VertexBufferObject = GL.GenBuffer();
            int VertexArrayObject = GL.GenVertexArray();

            GL.BindVertexArray(VertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, verts.Length * sizeof(float), verts, BufferUsageHint.StaticDraw);

            GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out int size);
            if (verts.Length * sizeof(float) != size)
            {
                throw new ApplicationException("Vertex data not loaded onto graphics card correctly");
            }

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            GL.GetBufferParameter(BufferTarget.ElementArrayBuffer, BufferParameterName.BufferSize, out size);
            if (indices.Length * sizeof(int) != size)
            {
                throw new ApplicationException("Index data not loaded onto graphics card correctly");
            }

            int bufferSize = 14 * sizeof(float);

            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, bufferSize, 0);

            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, bufferSize, 3 * sizeof(float));

            GL.EnableVertexAttribArray(2);
            GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, bufferSize, 5 * sizeof(float));

            GL.EnableVertexAttribArray(3);
            GL.VertexAttribPointer(3, 3, VertexAttribPointerType.Float, false, bufferSize, 8 * sizeof(float));

            GL.EnableVertexAttribArray(4);
            GL.VertexAttribPointer(4, 3, VertexAttribPointerType.Float, false, bufferSize, 11 * sizeof(float));

            return new MeshObject(VertexArrayObject, indices.Length);
        }
    }
}