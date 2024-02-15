using FluentAssertions;
using Moq;
using Reqnroll.Actions.Configuration;
using Reqnroll.Actions.Selenium.Configuration;
using System;
using Xunit;

namespace Reqnroll.Actions.Selenium.Tests
{
    public class SeleniumConfigurationTests
    {
        private static SeleniumConfiguration GetSeleniumConfiguration(string reqnrollJsonContent)
        {
            var reqnrollActionJsonLoader = new Mock<IReqnrollActionJsonLoader>();
            reqnrollActionJsonLoader.Setup(m => m.Load()).Returns(reqnrollJsonContent);
            reqnrollActionJsonLoader.Setup(m => m.LoadTarget()).Returns(reqnrollJsonContent);

            return new SeleniumConfiguration(new ReqnrollActionsConfiguration(reqnrollActionJsonLoader.Object));
        }

        [Fact]
        public void Browser_EmptyJson_ReturnsNone()
        {
            var reqnrollJsonContent = "{}";

            var seleniumConfiguration = GetSeleniumConfiguration(reqnrollJsonContent);

            seleniumConfiguration.Browser.Should().Be(Browser.None);
        }

        [Fact]
        public void Browser_SeleniumNodeExists_NoBrowserSet_ReturnsNone()
        {
            var reqnrollJsonContent = @"{ ""selenium"": {} }";

            var seleniumConfiguration = GetSeleniumConfiguration(reqnrollJsonContent);

            seleniumConfiguration.Browser.Should().Be(Browser.None);
        }

        [Fact]
        public void Browser_SeleniumNodeExists_BrowserSet_ReturnsValue()
        {
            var reqnrollJsonContent = @"{ ""selenium"": { ""browser"":""Chrome"" } }";

            var seleniumConfiguration = GetSeleniumConfiguration(reqnrollJsonContent);

            seleniumConfiguration.Browser.Should().Be(Browser.Chrome);
        }

        [Fact]
        public void Browser_SeleniumNodeExists_BrowserSet_NoMatchingEnum_ReturnsException()
        {
            Browser GetBrowser(SeleniumConfiguration seleniumConfiguration)
            {
                return seleniumConfiguration.Browser;
            }

            var reqnrollJsonContent = @"{ ""selenium"": { ""browser"":""NoMatchingBrowser"" } }";

            Action action = () => GetBrowser(GetSeleniumConfiguration(reqnrollJsonContent));

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Browser_SeleniumNodeExists_BrowserSet_LowerCase_ReturnsValue()
        {
            var reqnrollJsonContent = @"{ ""selenium"": { ""browser"":""chrome"" } }";

            var seleniumConfiguration = GetSeleniumConfiguration(reqnrollJsonContent);

            seleniumConfiguration.Browser.Should().Be(Browser.Chrome);
        }

        [Fact]
        public void Arguments_SeleniumNodeExists_BrowserNodeExists_ArgumentsNotSet_ArgumentsReturnsNull()
        {
            var reqnrollJsonContent = @"{ ""selenium"": { ""browser"":""Chrome"" } }";

            var seleniumConfiguration = GetSeleniumConfiguration(reqnrollJsonContent);

            seleniumConfiguration.Arguments.Should().BeEmpty();
        }

        [Fact]
        public void Arguments_SeleniumNodeExists_BrowserNodeExists_ArgumentsNotSet_ArgumentsReturnsEmpty()
        {
            var reqnrollJsonContent = @"{ ""selenium"": { ""browser"":""Chrome"", ""arguments"": [] } }";

            var seleniumConfiguration = GetSeleniumConfiguration(reqnrollJsonContent);

            seleniumConfiguration.Arguments.Should().BeEmpty();
        }

        [Fact]
        public void Arguments_SeleniumNodeExists_BrowserNodeExists_ArgumentsSet_ReturnsValue()
        {
            var reqnrollJsonContent =
                @"{ ""selenium"": { ""browser"":""Chrome"", ""arguments"": [ ""--argument-one"", ""--argument-two"" ] } }";

            var seleniumConfiguration = GetSeleniumConfiguration(reqnrollJsonContent);

            seleniumConfiguration.Arguments.Should().Equal("--argument-one", "--argument-two");
        }


        [Fact]
        public void Arguments_SeleniumNodeExists_BrowserNodeExists_CapabilitiesSet_ReturnsValue()
        {
            var reqnrollJsonContent =
                @"{ ""selenium"": { ""browser"":""Chrome"", ""capabilities"" : { ""some_capability"": ""the value"", ""some_other_capability"": ""also a value"" }} }";

            var seleniumConfiguration = GetSeleniumConfiguration(reqnrollJsonContent);

            seleniumConfiguration.Capabilities["some_capability"].Should().Be("the value");
            seleniumConfiguration.Capabilities["some_other_capability"].Should().Be("also a value");
        }
    }
}