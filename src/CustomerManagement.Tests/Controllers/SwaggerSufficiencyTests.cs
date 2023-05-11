using CustomerManagement.Tests.Controllers.Data;
using FluentAssertions;

namespace CustomerManagement.Tests.Controllers
{
    public class SwaggerSufficiencyTests
    {
        [Theory]
        [ClassData(typeof(AllControllersVerbMethodsAndDocumentationCommentsClassData))]
        public void Given_Public_Controller_Http_Method_Has_Xml_Documentation_Comment(string typeName,
            string methodName,
            string xmlDocumentation)
        {
            xmlDocumentation
                .Should()
                .NotBeNullOrEmpty();
        }
    }
}