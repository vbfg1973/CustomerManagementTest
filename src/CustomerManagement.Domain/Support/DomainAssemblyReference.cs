using System.Reflection;

namespace CustomerManagement.Domain.Support
{
    public sealed class DomainAssemblyReference
    {
        public static readonly Assembly Assembly = typeof(DomainAssemblyReference).Assembly;
    }
}