using FluentAssertions;
using Moq;
using Reqnroll.Actions.Configuration;
using Reqnroll.Actions.WindowsAppDriver.Configuration;
using System.Collections.Generic;
using Xunit;

namespace Reqnroll.Actions.WindowsAppDriver.Tests
{
    public class WindowsAppDriverConfigurationTests
    {
        private static WindowsAppDriverConfiguration GetAppDriverConfiguration(string reqnrollJsonContent)
        {
            var reqnrollActionJsonLoader = new Mock<IReqnrollActionJsonLoader>();
            reqnrollActionJsonLoader.Setup(m => m.Load()).Returns(reqnrollJsonContent);

            return new WindowsAppDriverConfiguration(reqnrollActionJsonLoader.Object);
        }

        [Fact]
        public void Capabilities_IsEmpty_If_Json_IsEmpty()
        {
            var reqnrollJsonContent = string.Empty;

            var appDriverConfiguration = GetAppDriverConfiguration(reqnrollJsonContent);

            appDriverConfiguration.Capabilities.Should().BeEmpty();
        }

        [Fact]
        public void Capabilities_IsEmpty_If_ValuesNotProvided()
        {
            var reqnrollJsonContent = "{\"windowsAppDriver\": {\"capabilities\": {}}}";

            var appDriverConfiguration = GetAppDriverConfiguration(reqnrollJsonContent);

            appDriverConfiguration.Capabilities.Should().BeEmpty();
        }

        [Fact]
        public void WindowsAppDriverPath_IsNull_If_Json_IsEmpty()
        {
            var reqnrollJsonContent = string.Empty;

            var appDriverConfiguration = GetAppDriverConfiguration(reqnrollJsonContent);

            appDriverConfiguration.WindowsAppDriverPath.Should().BeNull();
        }

        [Fact]
        public void EnableScreenshots_IsNull_If_Json_IsEmpty()
        {
            var reqnrollJsonContent = string.Empty;

            var appDriverConfiguration = GetAppDriverConfiguration(reqnrollJsonContent);

            appDriverConfiguration.EnableScreenshots.Should().BeNull();
        }

        [Fact]
        public void LoadReqnrollJson_Ignores_Casing()
        {
            var reqnrollJsonContent = "{\"WINDOWSAPPDRIVER\": {\"CAPABILITIES\": {\"APP\": \"path\"}}}";
            var expected = new KeyValuePair<string, string>("APP", "path");

            var appDriverConfiguration = GetAppDriverConfiguration(reqnrollJsonContent);

            appDriverConfiguration.Capabilities!.Should().Contain(expected);
        }

        [Fact]
        public void Capabilities_ContainsAdditionalCapabilities_If_ValuesSpecified()
        {
            var reqnrollJsonContent = "{\"windowsAppDriver\": {\"capabilities\": {\"app\": \"path\", \"appArguments\": \"-env local\"}}}";

            var expectedApp = new KeyValuePair<string, string>("app", "path");
            var expectedAppArguments = new KeyValuePair<string, string>("appArguments", "-env local");

            var appDriverConfiguration = GetAppDriverConfiguration(reqnrollJsonContent);

            appDriverConfiguration.Capabilities.Should().Contain(expectedApp, expectedAppArguments);
        }
    }
}