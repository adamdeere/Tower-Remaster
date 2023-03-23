using TowerRemaster.Utility;

namespace TowerRemaster.Managers
{
    internal static class ShaderManager
    {
        public static Dictionary<string, Shader> shaderDictionary = new();

        public static Shader? FindShader(string shader)
        {
            shaderDictionary.TryGetValue(shader, out var result);
            if (result != null)
                return result;

            return null;
        }
    }
}