// unset

using FluentAssertions;
using Moq;
using Xunit;

namespace Reqnroll.Actions.Configuration.Tests
{
    public class ReqnrollActionsConfigurationTests
    {
        [Fact]
        public void Get_PathWithDots_Works()
        {
            var content = @"{""parent"": { ""child"": ""value"" }}";


            var reqnrollActionJsonLoaderMock = new Mock<IReqnrollActionJsonLoader>();
            reqnrollActionJsonLoaderMock.Setup(m => m.Load()).Returns(content);
            reqnrollActionJsonLoaderMock.Setup(m => m.LoadTarget()).Returns(content);

            var reqnrollActionsConfiguration = new ReqnrollActionsConfiguration(reqnrollActionJsonLoaderMock.Object);

            var actualValue = reqnrollActionsConfiguration.Get("parent:child");

            actualValue.Should().Be("value");
        }


        [Fact]
        public void Get_PathNotExists_ReturnsNull()
        {
            var content = @"{""parent"": { ""child"": ""value"" }}";


            var reqnrollActionJsonLoaderMock = new Mock<IReqnrollActionJsonLoader>();
            reqnrollActionJsonLoaderMock.Setup(m => m.Load()).Returns(content);
            reqnrollActionJsonLoaderMock.Setup(m => m.LoadTarget()).Returns(content);

            var reqnrollActionsConfiguration = new ReqnrollActionsConfiguration(reqnrollActionJsonLoaderMock.Object);

            var actualValue = reqnrollActionsConfiguration.Get("parent:the-other-child");

            actualValue.Should().BeNull();
        }

        [Fact]
        public void GetDictionary_ObjectDefinedInParentAndTarget_Merged()
        {
            var parentContent = @"{""parent"": { ""children"": { ""parentChild"":""a value""} }}";
            var targetContent = @"{""parent"": { ""children"": { ""childChild"":""another value""} }}";


            var reqnrollActionJsonLoaderMock = new Mock<IReqnrollActionJsonLoader>();
            reqnrollActionJsonLoaderMock.Setup(m => m.Load()).Returns(parentContent);
            reqnrollActionJsonLoaderMock.Setup(m => m.LoadTarget()).Returns(targetContent);

            var reqnrollActionsConfiguration = new ReqnrollActionsConfiguration(reqnrollActionJsonLoaderMock.Object);

            var actualValue = reqnrollActionsConfiguration.GetDictionary("parent:children");

            actualValue.Should().HaveCount(2);
        }
    }
}