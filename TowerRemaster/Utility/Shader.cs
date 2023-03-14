using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace TowerRemaster.Utility
{
    internal class Shader
    {
        private readonly int pgmID;
        private bool disposedValue = false;

        private readonly Dictionary<string, int> _uniformLocations = new Dictionary<string, int>();

        public Shader(string vertexPath, string fragmentPath)
        {
            int VertexShader = CompileShader(File.ReadAllText(vertexPath), ShaderType.VertexShader);
            int FragmentShader = CompileShader(File.ReadAllText(fragmentPath), ShaderType.FragmentShader);

            pgmID = GL.CreateProgram();
            if (VertexShader >= 0 && FragmentShader >= 0)
            {
                GL.AttachShader(pgmID, VertexShader);
                GL.AttachShader(pgmID, FragmentShader);

                LinkProgram(pgmID);

                // First, we have to get the number of active uniforms in the shader.
                GL.GetProgram(pgmID, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);

                // Loop over all the uniforms,
                for (var i = 0; i < numberOfUniforms; i++)
                {
                    // get the name of this uniform,
                    var key = GL.GetActiveUniform(pgmID, i, out _, out _);

                    // get the location,
                    var location = GL.GetUniformLocation(pgmID, key);

                    // and then add it to the dictionary.
                    _uniformLocations.Add(key, location);
                }

                GL.DetachShader(pgmID, VertexShader);
                GL.DetachShader(pgmID, FragmentShader);
                GL.DeleteShader(FragmentShader);
                GL.DeleteShader(VertexShader);
            }
        }

        ~Shader()
        {
            GL.DeleteProgram(pgmID);
        }

        private static void LinkProgram(int program)
        {
            GL.LinkProgram(program);
            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out int success);
            if (success == 0)
            {
                string infoLog = GL.GetProgramInfoLog(program);
                Console.WriteLine(infoLog);
                throw new Exception($"Error occurred whilst linking Program({program})");
            }
        }

        public void Use()
        {
            GL.UseProgram(pgmID);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                GL.DeleteProgram(pgmID);

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private static int CompileShader(string source, ShaderType type)
        {
            int shader = GL.CreateShader(type);
            GL.ShaderSource(shader, source);

            GL.CompileShader(shader);

            GL.GetShader(shader, ShaderParameter.CompileStatus, out int success);
            if (success != (int)All.True)
            {
                string infoLog = GL.GetShaderInfoLog(shader);
                Console.WriteLine(infoLog);
                throw new Exception($"Error occurred whilst compiling Shader({shader}).\n\n{infoLog}");
            }
            return shader;
        }

        public int GetAttribLocation(string attribName)
        {
            return GL.GetAttribLocation(pgmID, attribName);
        }

        public void SetInt(string name, int data)
        {
            GL.Uniform1(_uniformLocations[name], data);
        }

        public void SetMatrix4(string name, Matrix4 mat)
        {
            GL.UniformMatrix4(_uniformLocations[name], true, ref mat);
        }

        public void SetVector3(string name, Vector3 data)
        {
            GL.Uniform3(_uniformLocations[name], data);
        }

        public void SetFloat(string name, float data)
        {
            GL.Uniform1(_uniformLocations[name], data);
        }
    }
}