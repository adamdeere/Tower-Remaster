using OpenTK.Graphics.OpenGL4;

namespace TowerRemaster.Utility
{
    internal static class ModelLoader
    {
        public static Model LoadFromFile(string fileName)
        {
            List<float> vertices = new List<float>();
            List<int> indices = new List<int>();
            string[] separatingStrings = { " ", "," };
            using (StreamReader sr = File.OpenText("Assets/Models/" + fileName))
            {
                string s = string.Empty;
                while (sr.Peek() != -1)
                {
                    string? line = sr.ReadLine();

                    if (line != null)
                    {
                        string[] vertString = line.Split(separatingStrings, StringSplitOptions.RemoveEmptyEntries);
                        if (vertString[0] == "verts")
                        {
                            for (int i = 1; i < vertString.Length; i++)
                            {
                                if (float.TryParse(vertString[i], out float numValue))
                                {
                                    vertices.Add(numValue);
                                }
                            }
                        }
                        else if (vertString[0] == "inds")
                        {
                            if (int.TryParse(vertString[1], out int numValue))
                            {
                                indices.Add(numValue);
                            }
                        }
                    }
                }
            }

            int VertexBufferObject = GL.GenBuffer();
            int VertexArrayObject = GL.GenVertexArray();

            GL.BindVertexArray(VertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Count * sizeof(float), vertices.ToArray(), BufferUsageHint.StaticDraw);

            GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out int size);
            if (vertices.Count * sizeof(float) != size)
            {
                throw new ApplicationException("Vertex data not loaded onto graphics card correctly");
            }

            int ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Count * sizeof(int), indices.ToArray(), BufferUsageHint.StaticDraw);

            GL.GetBufferParameter(BufferTarget.ElementArrayBuffer, BufferParameterName.BufferSize, out size);
            if (indices.Count * sizeof(int) != size)
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

            return new Model(VertexArrayObject, indices.Count);
        }
    }
}