using CustomerManagement.Tests.Dtos.Data;
using FluentAssertions;

namespace CustomerManagement.Tests.Dtos
{
    public class SwaggerSufficiencyTests
    {
        [Theory]
        [ClassData(typeof(AllDtoPocosAndPropertiesAndDocumentationCommentsClassData))]
        public void Given_Dto_Poco_Public_Property_Has_Xml_Documentation_Comment(string typeName, string propertyName,
            string xmlDocumentation)
        {
            xmlDocumentation
                .Should()
                .NotBeNullOrEmpty();
        }

        [Theory]
        [ClassData(typeof(AllDtosDocumentationCommentsClassData))]
        public void Given_Dto_Poco_Has_Xml_Documentation_Comment(string typeName, string xmlDocumentation)
        {
            xmlDocumentation
                .Should()
                .NotBeNullOrEmpty();
        }
    }
}