using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace PipelinesDotNet.ServiceProvider.Tests
{
    [TestFixture]
    public class PipelineBuilderTests
    {
        [Test]
        public void ShouldReturnContainerizedPipelineBuilder_WhenConverted()
        {
            using var serviceProvider = new ServiceCollection()
                .AddSingleton(new Addition(2))
                .BuildServiceProvider();


            var pipelineFactory = new PipelineBuilder()
                .WithStep<int, int>(x => x * 2)
                .WithStep(x => x + 2)
                .WithStep(x => x + 2)
                .WithStep(x => x + 2)
                .WithStep(x => x + 2)
                .WithStep(x => x + 2)
                .WithServiceProvider()
                .WithStep<Addition, int>((x, i) => x.Process(i))
                .PipelineFactory;


            var result = pipelineFactory(serviceProvider)(10);

            result.Should().Be(32);
        }

        [Test]
        public async Task ShouldReturnAsyncContainerizedPipelineBuilder_WhenConvertedAsyncPipeline()
        {
            using var serviceProvider = new ServiceCollection()
                .AddSingleton(new Addition(2))
                .BuildServiceProvider();


            var pipelineFactory = new PipelineBuilder()
                .WithStep<int, int>(x => x * 2)
                .WithStep(x => x + 2)
                .WithStep(x => x + 2)
                .WithStep(x => x + 2)
                .WithStep(x => x + 2)
                .WithStep(async x => x + 2)
                .WithServiceProvider()
                .WithStep<Addition, int>((x, i) => x.Process(i))
                .PipelineFactory;


            var result = await pipelineFactory(serviceProvider)(10);

            result.Should().Be(32);
        }

        private class Addition
        {
            private readonly int _term;

            public Addition(int term)
            {
                _term = term;
            }

            public int Process(int input)
            {
                return input + _term;
            }
        }
    }
}
