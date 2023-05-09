using System.Reflection;

namespace CustomerManagement.Api.Support
{
    public sealed class ApiAssemblyReference
    {
        public static readonly Assembly Assembly = typeof(ApiAssemblyReference).Assembly;
    }
}