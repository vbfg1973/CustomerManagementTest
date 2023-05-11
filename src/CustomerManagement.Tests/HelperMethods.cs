using System.Reflection;
using Towel;

namespace CustomerManagement.Tests
{
    public static class HelperMethods
    {
        public static void LoadXmlDocumentation(this Assembly assembly)
        {
            var directoryPath = assembly.GetDirectoryPath();

            if (directoryPath == null) return;

            var xmlFilePath = Path.Combine(directoryPath, assembly.GetName().Name + ".xml");

            if (!File.Exists(xmlFilePath)) return;

            Meta.LoadXmlDocumentation(File.ReadAllText(xmlFilePath));
        }
    }
}