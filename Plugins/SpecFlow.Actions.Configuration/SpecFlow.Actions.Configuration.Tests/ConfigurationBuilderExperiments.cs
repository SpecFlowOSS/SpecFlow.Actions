using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace SpecFlow.Actions.Configuration.Tests
{
    public class ConfigurationBuilderExperiments
    {
        private readonly string _baselineConfiguration = @"{ ""root_value"": ""value"", ""overwritten_in_overlay"":""No"", ""list"": [""rootentry""]}";
        private readonly string _overlayConfiguration = @"{""overlay_value"": ""othervalue"", ""overwritten_in_overlay"": ""Yes"", ""list"": [""overlayentry""]}";

        private static IConfigurationRoot ParseConfiguration(string baseline, string overlay)
        {
            var baselineStream = new MemoryStream(Encoding.UTF8.GetBytes(baseline));
            var overlayStream = new MemoryStream(Encoding.UTF8.GetBytes(overlay));


            var configuration = new ConfigurationBuilder()
                .AddJsonStream(baselineStream)
                .AddJsonStream(overlayStream)
                .Build();

            return configuration;
        }

        [Fact]
        public void Configuration_Value_Only_In_Root()
        {
            var configuration = ParseConfiguration(_baselineConfiguration, _overlayConfiguration);

            configuration["root_value"].Should().Be("value");
        }

        [Fact]
        public void Configuration_Value_Only_In_Overlay()
        {
            var configuration = ParseConfiguration(_baselineConfiguration, _overlayConfiguration);


            configuration["overlay_value"].Should().Be("othervalue");
        }

        [Fact]
        public void Configuration_Overwritten_With_Overlay()
        {
            var configuration = ParseConfiguration(_baselineConfiguration, _overlayConfiguration);

            configuration["overwritten_in_overlay"].Should().Be("Yes");
        }

        [Fact]
        public void Configuration_Lists_Are_Not_Getting_Merged()
        {
            var configuration = ParseConfiguration(_baselineConfiguration, _overlayConfiguration);
            var strings = configuration.GetSection("list").Get<string[]>();

            
            strings.Should().NotContain("rootentry");
            strings.Should().Contain("overlayentry");
        }
    }
}
