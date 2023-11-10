using FluentAssertions;

namespace PipelinesDotNet.Tests
{
    [TestFixture]
    public class PipelineBuilderTests
    {
        [Test]
        public void Pipeline_ShouldReturnEntirePipline_WhenRegisteredOnlyOneStep()
        {
            var pipeline = new PipelineBuilder()
                .WithStep<int, int>(x => x * 2)
                .Pipeline;

            var result = pipeline(10);

            result.Should().Be(20);
        }

        [Test]
        public void Pipeline_ShouldReturnEntirePipline_WhenTwoStepsRegistered()
        {
            var pipeline = new PipelineBuilder()
                .WithStep<int, int>(x => x * 2)
                .WithStep(x => x + 2)
                .Pipeline;

            var result = pipeline(10);

            result.Should().Be(22);
        }

        [Test]
        public void Pipeline_ShouldReturnEntirePipline_WhenMultipleStepsRegistered()
        {
            var pipeline = new PipelineBuilder()
                .WithStep<int, int>(x => x * 2)
                .WithStep(x => x + 2)
                .WithStep(x => x + 2)
                .WithStep(x => x + 2)
                .WithStep(x => x + 2)
                .WithStep(x => x + 2)
                .WithStep(x => x + 2)
                .Pipeline;

            var result = pipeline(10);

            result.Should().Be(32);
        }

        [Test]
        public async Task Pipeline_ShouldReturnAsyncPipline_WhenRegisteredOnlyOneAndAsyncStep()
        {
            var pipeline = new PipelineBuilder()
                .WithStep<int, int>(x => Task.FromResult(x * 2))
                .Pipeline;

            var result = await pipeline(10);

            result.Should().Be(20);
        }

        [Test]
        public async Task Pipeline_ShouldReturnAsyncPipline_WhenTwoAsyncStepsRegistered()
        {
            var pipeline = new PipelineBuilder()
                .WithStep<int, int>(x => Task.FromResult(x * 2))
                .WithStep(x => Task.FromResult(x + 2))
                .Pipeline;

            var result = await pipeline(10);

            result.Should().Be(22);
        }

        [Test]
        public async Task Pipeline_ShouldReturnAsyncPipline_WhenMultipleAsyncStepsRegistered()
        {
            var pipeline = new PipelineBuilder()
                .WithStep<int, int>(x => Task.FromResult(x * 2))
                .WithStep(x => Task.FromResult(x + 2))
                .WithStep(x => Task.FromResult(x + 2))
                .WithStep(x => Task.FromResult(x + 2))
                .WithStep(x => Task.FromResult(x + 2))
                .WithStep(x => Task.FromResult(x + 2))
                .WithStep(x => Task.FromResult(x + 2))
                .Pipeline;

            var result = await pipeline(10);

            result.Should().Be(32);
        }

        [Test]
        public async Task Pipeline_ShouldReturnAsyncPipline_WhenOneRegisteredStepIsNotAsync()
        {
            var pipeline = new PipelineBuilder()
                .WithStep<int, int>(x => Task.FromResult(x * 2))
                .WithStep(x => Task.FromResult(x + 2))
                .WithStep(x => x + 2)
                .WithStep(x => Task.FromResult(x + 2))
                .WithStep(x => Task.FromResult(x + 2))
                .WithStep(x => Task.FromResult(x + 2))
                .WithStep(x => Task.FromResult(x + 2))
                .Pipeline;

            var result = await pipeline(10);

            result.Should().Be(32);
        }

        [Test]
        public async Task Pipeline_ShouldReturnAsyncPipline_WhenFirstRegisteredStepIsNotAsync()
        {
            var pipeline = new PipelineBuilder()
                .WithStep<int, int>(x => x * 2)
                .WithStep(x => Task.FromResult(x + 2))
                .WithStep(x => Task.FromResult(x + 2))
                .WithStep(x => Task.FromResult(x + 2))
                .WithStep(x => Task.FromResult(x + 2))
                .WithStep(x => Task.FromResult(x + 2))
                .WithStep(x => Task.FromResult(x + 2))
                .Pipeline;

            var result = await pipeline(10);

            result.Should().Be(32);
        }
    }
}
