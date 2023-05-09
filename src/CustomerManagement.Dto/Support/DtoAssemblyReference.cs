using System.Reflection;

namespace CustomerManagement.Dto.Support
{
    public sealed class DtoAssemblyReference
    {
        public static readonly Assembly Assembly = typeof(DtoAssemblyReference).Assembly;
    }
}