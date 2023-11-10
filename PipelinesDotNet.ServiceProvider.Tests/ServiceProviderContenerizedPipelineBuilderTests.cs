using Microsoft.Extensions.DependencyInjection;
using PipelinesDotNet.ServiceProvider;

namespace PipelinesDotNet.Tests
{
    [TestFixture]
    public class ServiceProviderContenerizedPipelineBuilderTests
    {
        [Test]
        public void PipelineFactory_ShouldThrowExceptionOnInstantiation_WhenFirstStepIsNotRegistered()
        {
            using var serviceProvider = new ServiceCollection()
                //.AddSingleton(new Multiplication(10))
                .AddSingleton(new Addition(2))
                .AddSingleton(new Subtraction(10))
                .BuildServiceProvider();

            var pipelineFactory = new PipelineBuilder()
                .WithServiceProvider()
                .WithStep<Multiplication, int, int>((x, i) => x.Multiply(i))
                .WithStep<Addition, int>((x, i) => x.Add(i))
                .WithStep<Subtraction, int>((x, i) => x.Subtract(i))
                .PipelineFactory;

            Assert.Throws<InvalidOperationException>(() => pipelineFactory(serviceProvider));
        }

        [Test]
        public void PipelineFactory_ShouldThrowExceptionOnInstantiation_WhenSecondStepIsNotRegistered()
        {
            using var serviceProvider = new ServiceCollection()
                .AddSingleton(new Multiplication(10))
                //.AddSingleton(new Addition(2))
                .AddSingleton(new Subtraction(10))
                .BuildServiceProvider();

            var pipelineFactory = new PipelineBuilder()
                .WithServiceProvider()
                .WithStep<Multiplication, int, int>((x, i) => x.Multiply(i))
                .WithStep<Addition, int>((x, i) => x.Add(i))
                .WithStep<Subtraction, int>((x, i) => x.Subtract(i))
                .PipelineFactory;

            Assert.Throws<InvalidOperationException>(() => pipelineFactory(serviceProvider));
        }

        [Test]
        public void PipelineFactory_ShouldThrowExceptionOnInstantiation_WhenThirdStepIsNotRegistered()
        {
            using var serviceProvider = new ServiceCollection()
                .AddSingleton(new Multiplication(10))
                .AddSingleton(new Addition(2))
                //.AddSingleton(new Subtraction(10))
                .BuildServiceProvider();

            var pipelineFactory = new PipelineBuilder()
                .WithServiceProvider()
                .WithStep<Multiplication, int, int>((x, i) => x.Multiply(i))
                .WithStep<Addition, int>((x, i) => x.Add(i))
                .WithStep<Subtraction, int>((x, i) => x.Subtract(i))
                .PipelineFactory;

            Assert.Throws<InvalidOperationException>(() => pipelineFactory(serviceProvider));
        }

        [Test]
        public void PipelineFactory_ShouldInstantiatePipeline_WhenAllStepsAreRegistered()
        {
            using var serviceProvider = new ServiceCollection()
                .AddSingleton(new Multiplication(10))
                .AddSingleton(new Addition(2))
                .AddSingleton(new Subtraction(10))
                .BuildServiceProvider();

            var pipelineFactory = new PipelineBuilder()
                .WithServiceProvider()
                .WithStep<Multiplication, int, int>((x, i) => x.Multiply(i))
                .WithStep<Addition, int>((x, i) => x.Add(i))
                .WithStep<Subtraction, int>((x, i) => x.Subtract(i))
                .PipelineFactory;

            Assert.DoesNotThrow(() => pipelineFactory(serviceProvider));
        }

        private class Addition
        {
            private readonly int _term;

            public Addition(int term)
            {
                _term = term;
            }

            public int Add(int input)
            {
                return input + _term;
            }
        }

        private class Subtraction
        {
            private readonly int _term;

            public Subtraction(int term)
            {
                _term = term;
            }

            public int Subtract(int input)
            {
                return input - _term;
            }
        }

        private class Multiplication
        {
            private readonly int _factor;

            public Multiplication(int factor)
            {
                _factor = factor;
            }

            public int Multiply(int input)
            {
                return input * _factor;
            }
        }
    }
}
