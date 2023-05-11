using System.Collections;
using System.Reflection;
using CustomerManagement.Domain.Support;
using Swashbuckle.AspNetCore.SwaggerGen;
using Towel;

namespace CustomerManagement.Tests.Dtos.Data
{
    public class AllDtosDocumentationCommentsClassData : IEnumerable<object[]>
    {
        private readonly Assembly _assembly;

        public AllDtosDocumentationCommentsClassData()
        {
            // Get the assembly of the following controller
            _assembly = DomainAssemblyReference.Assembly;
            _assembly.LoadXmlDocumentation();
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            // Grab types from assembly and limit to those inheriting from controller base
            var types = _assembly
                .GetExportedTypes();

            foreach (var type in types.Where(x => x.Name.EndsWith("Dto", StringComparison.InvariantCultureIgnoreCase)))
                yield return new object[]
                {
                    type.Name,
                    type.GetDocumentation()?.Trim()!
                };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    /// <summary>
    ///     Uses reflection to return any types defined in DTO assembly
    /// </summary>
    public class AllDtoPocosAndPropertiesAndDocumentationCommentsClassData : IEnumerable<object[]>
    {
        private readonly Assembly _assembly;

        public AllDtoPocosAndPropertiesAndDocumentationCommentsClassData()
        {
            // Get the assembly of the following controller
            _assembly = DomainAssemblyReference.Assembly;
            _assembly.LoadXmlDocumentation();
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            // Grab types from assembly and limit to those inheriting from controller base
            var types = _assembly
                .GetExportedTypes();

            foreach (var type in types.Where(x => x.Name.EndsWith("Dto", StringComparison.InvariantCultureIgnoreCase)))
            {
                // For each type grab only those public methods that have a HttpVerb attribute
                var properties = type.GetProperties()
                    .Where(propertyInfo => propertyInfo.IsPubliclyReadable());

                foreach (var method in properties)
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