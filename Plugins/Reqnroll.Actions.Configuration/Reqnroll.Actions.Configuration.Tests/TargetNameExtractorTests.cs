using FluentAssertions;
using Xunit;

namespace Reqnroll.Actions.Configuration.Tests
{
    public class TargetNameExtractorTests
    {
        [Theory]
        [InlineData("reqnroll.actions.edge.json", "edge")]
        [InlineData("reqnroll.actions.edge.windows.json", "edge.windows")]
        public void Extract(string actual, string expected)
        {
            var targetNameExtractor = new TargetNameExtractor();
            var extractedTargetName = targetNameExtractor.Extract(actual);
            extractedTargetName.Should().Be(expected);
        }
    }
}