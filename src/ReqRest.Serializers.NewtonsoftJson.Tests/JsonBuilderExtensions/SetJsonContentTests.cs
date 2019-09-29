﻿namespace ReqRest.Serializers.NewtonsoftJson.Tests.JsonBuilderExtensions
{
    using System.Text;
    using System.Threading.Tasks;
    using Builders;
    using FluentAssertions;
    using Moq;
    using Newtonsoft.Json;
    using ReqRest.Serializers.NewtonsoftJson;
    using Xunit;

    public class SetJsonContentTests
    {
        
        private static readonly JsonHttpContentSerializer DefaultSerializer = new JsonHttpContentSerializer();
        private static readonly Encoding DefaultEncoding = Encoding.UTF8;
        
        [Theory]
        [MemberData(nameof(JsonTestData.Dtos), MemberType = typeof(JsonTestData))]
        public async Task Serializes_Object_And_Sets_Content(MockDto dto)
        {
            var builder = new HttpRequestMessageBuilder();
            var expectedContent = DefaultSerializer.Serialize(dto, DefaultEncoding);
            var actualContent = builder.SetJsonContent(dto).HttpRequestMessage.Content;

            var expectedString = await expectedContent.ReadAsStringAsync();
            var actualString = await actualContent.ReadAsStringAsync();
            actualString.Should().Be(expectedString);
        }

        [Fact]
        public void Uses_Specified_Serializer_To_Serialize_Object()
        {
            var serializerMock = new Mock<JsonHttpContentSerializer>(new JsonSerializer()) { CallBase = true };
            var builder = new HttpRequestMessageBuilder();
            var dto = new object();
            
            builder.SetJsonContent(dto, Encoding.ASCII, serializerMock.Object);
            serializerMock.Verify(x => x.Serialize(dto, typeof(object), Encoding.ASCII));
        }
        
    }

}