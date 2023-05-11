using System.Collections;
using System.Reflection;
using CustomerManagement.Api.Support;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Towel;

namespace CustomerManagement.Tests.Controllers.Data
{
    /// <summary>
    ///     Uses reflection to return controllers, httpVerb methods and the XML documentation comments (if any) for that method
    /// </summary>
    public class AllControllersVerbMethodsAndDocumentationCommentsClassData : IEnumerable<object[]>
    {
        private readonly Assembly _assembly;

        public AllControllersVerbMethodsAndDocumentationCommentsClassData()
        {
            // Get the assembly of the following controller
            _assembly = ApiAssemblyReference.Assembly;
            _assembly.LoadXmlDocumentation();
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            // Grab types from assembly and limit to those inheriting from controller base
            var types = _assembly
                .GetExportedTypes()
                .Where(type => typeof(ControllerBase).IsAssignableFrom(type));

            foreach (var type in types)
            {
                // For each type grab only those public methods that have a HttpVerb attribute
                var httpVerbMethods = type.GetMethods()
                    .Where(method => method.GetCustomAttributes(typeof(HttpMethodAttribute)).Any())
                    .Where(method => method.IsPublic);

                foreach (var method in httpVerbMethods)
                    yield return new object[]
                    {
                        type.Name,
                        method.Name,
                        method.GetDocumentation()?.Trim()!
                    };
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}