using System.Reflection;

namespace CustomerManagement.Api.Support
{
    /// <summary>
    ///     A reference to the domain assembly
    /// </summary>
    public sealed class ApiAssemblyReference
    {
        /// <summary>
        ///     The assembly reference
        /// </summary>
        public static readonly Assembly Assembly = typeof(ApiAssemblyReference).Assembly;
    }
}