using System.Reflection;

namespace CustomerManagement.Data.Support
{
    public sealed class DataAssemblyReference
    {
        public static readonly Assembly Assembly = typeof(DataAssemblyReference).Assembly;
    }
}