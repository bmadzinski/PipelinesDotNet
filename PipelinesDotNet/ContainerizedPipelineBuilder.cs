using System;
using System.Threading.Tasks;

namespace PipelinesDotNet
{
    public class ContainerizedPipelineBuilder<TServiceProvider>
    {
        public Func<TServiceProvider, Type, object> Factory { get; }

        public ContainerizedPipelineBuilder(Func<TServiceProvider, Type, object> factory)
        {
            Factory = factory;
        }

        public ContainerizedPipelineBuilder<TServiceProvider, TInput, TResult> WithStep<TInput, TResult>(Func<TInput, TResult> action)
        {
            return new ContainerizedPipelineBuilder<TServiceProvider, TInput, TResult>(
                Factory,
                provider => input => action(input)
            );
        }

        public ContainerizedPipelineBuilder<TServiceProvider, TInput, TResult> WithStep<TStep, TInput, TResult>(
            Func<TStep, TInput, TResult> action
        )
        {
            return WithStep(
                provider => (TStep)Factory(provider, typeof(TStep)),
                action
            );
        }

        public ContainerizedPipelineBuilder<TServiceProvider, TInput, TResult> WithStep<TStep, TInput, TResult>(
            Func<TServiceProvider, TStep> factory,
            Func<TStep, TInput, TResult> action
        )
        {
            return new ContainerizedPipelineBuilder<TServiceProvider, TInput, TResult>(
                Factory,
                provider =>
                {
                    var stepService = factory(provider);

                    return input => action(stepService, input);
                }
            );
        }

        public AsyncContainerizedPipelineBuilder<TServiceProvider, TInput, TResult> WithStep<TInput, TResult>(Func<TInput, Task<TResult>> action)
        {
            return new AsyncContainerizedPipelineBuilder<TServiceProvider, TInput, TResult>(
                Factory,
                provider => input => action(input)
            );
        }

        public AsyncContainerizedPipelineBuilder<TServiceProvider, TInput, TResult> WithStep<TStep, TInput, TResult>(
            Func<TStep, TInput, Task<TResult>> action
        )
        {
            return WithStep(
                provider => (TStep)Factory(provider, typeof(TStep)),
                action
            );
        }

        public AsyncContainerizedPipelineBuilder<TServiceProvider, TInput, TResult> WithStep<TStep, TInput, TResult>(
            Func<TServiceProvider, TStep> factory,
            Func<TStep, TInput, Task<TResult>> action
        )
        {
            return new AsyncContainerizedPipelineBuilder<TServiceProvider, TInput, TResult>(
                Factory,
                provider =>
                {
                    var stepService = factory(provider);

                    return input => action(stepService, input);
                }
            );
        }
    }

    public class AsyncContainerizedPipelineBuilder<TServiceProvider, TInput, TResult> : ContainerizedPipelineBuilder<TServiceProvider>
    {
        public Func<TServiceProvider, Func<TInput, Task<TResult>>> PipelineFactory { get; }

        public AsyncContainerizedPipelineBuilder(
            Func<TServiceProvider, Type, object> factory,
            Func<TServiceProvider, Func<TInput, Task<TResult>>> pipelineFactory
        ) : base(factory)
        {
            PipelineFactory = pipelineFactory;
        }

        public AsyncContainerizedPipelineBuilder<TServiceProvider, TInput, TNewResult> WithStep<TNewResult>(Func<TResult, TNewResult> action)
        {
            return new AsyncContainerizedPipelineBuilder<TServiceProvider, TInput, TNewResult>(
                Factory,
                provider =>
                {
                    var pipeline = PipelineFactory(provider);

                    return async input => action(await pipeline(input));
                }
            );
        }

        public AsyncContainerizedPipelineBuilder<TServiceProvider, TInput, TNewResult> WithStep<TStep, TNewResult>(
            Func<TStep, TResult, TNewResult> action
        )
        {
            return WithStep(
                provider => (TStep)Factory(provider, typeof(TStep)),
                action
            );
        }

        public AsyncContainerizedPipelineBuilder<TServiceProvider, TInput, TNewResult> WithStep<TStep, TNewResult>(
            Func<TServiceProvider, TStep> factory,
            Func<TStep, TResult, TNewResult> action
        )
        {
            return new AsyncContainerizedPipelineBuilder<TServiceProvider, TInput, TNewResult>(
                Factory,
                provider =>
                {
                    var stepService = factory(provider);
                    var pipeline = PipelineFactory(provider);

                    return async input => action(stepService, await pipeline(input));
                }
            );
        }

        public AsyncContainerizedPipelineBuilder<TServiceProvider, TInput, TNewResult> WithStep<TNewResult>(Func<TResult, Task<TNewResult>> action)
        {
            return new AsyncContainerizedPipelineBuilder<TServiceProvider, TInput, TNewResult>(
                Factory,
                provider =>
                {
                    var pipeline = PipelineFactory(provider);

                    return async input => await action(await pipeline(input));
                }
            );
        }

        public AsyncContainerizedPipelineBuilder<TServiceProvider, TInput, TNewResult> WithStep<TStep, TNewResult>(
            Func<TStep, TResult, Task<TNewResult>> action
        )
        {
            return WithStep(
                provider => (TStep)Factory(provider, typeof(TStep)),
                action
            );
        }

        public AsyncContainerizedPipelineBuilder<TServiceProvider, TInput, TNewResult> WithStep<TStep, TNewResult>(
            Func<TServiceProvider, TStep> factory,
            Func<TStep, TResult, Task<TNewResult>> action
        )
        {
            return new AsyncContainerizedPipelineBuilder<TServiceProvider, TInput, TNewResult>(
                Factory,
                provider =>
                {
                    var stepService = factory(provider);
                    var pipeline = PipelineFactory(provider);

                    return async input => await action(stepService, await pipeline(input));
                }
            );
        }
    }

    public class ContainerizedPipelineBuilder<TServiceProvider, TInput, TResult> : ContainerizedPipelineBuilder<TServiceProvider>
    {
        public Func<TServiceProvider, Func<TInput, TResult>> PipelineFactory { get; }

        public ContainerizedPipelineBuilder(
            Func<TServiceProvider, Type, object> factory,
            Func<TServiceProvider, Func<TInput, TResult>> pipelineFactory
        ) : base(factory)
        {
            PipelineFactory = pipelineFactory;
        }

        public ContainerizedPipelineBuilder<TServiceProvider, TInput, TNewResult> WithStep<TNewResult>(Func<TResult, TNewResult> action)
        {
            return new ContainerizedPipelineBuilder<TServiceProvider, TInput, TNewResult>(
                Factory,
                provider =>
                {
                    var pipeline = PipelineFactory(provider);

                    return input => action(pipeline(input));
                }
            );
        }

        public ContainerizedPipelineBuilder<TServiceProvider, TInput, TNewResult> WithStep<TStep, TNewResult>(
            Func<TStep, TResult, TNewResult> action
        )
        {
            return WithStep(
                provider => (TStep)Factory(provider, typeof(TStep)),
                action
            );
        }

        public ContainerizedPipelineBuilder<TServiceProvider, TInput, TNewResult> WithStep<TStep, TNewResult>(
            Func<TServiceProvider, TStep> factory,
            Func<TStep, TResult, TNewResult> action
        )
        {
            return new ContainerizedPipelineBuilder<TServiceProvider, TInput, TNewResult>(
                Factory,
                provider =>
                {
                    var stepService = factory(provider);
                    var pipeline = PipelineFactory(provider);

                    return input => action(stepService, pipeline(input));
                }
            );
        }

        public AsyncContainerizedPipelineBuilder<TServiceProvider, TInput, TNewResult> WithStep<TNewResult>(Func<TResult, Task<TNewResult>> action)
        {
            return new AsyncContainerizedPipelineBuilder<TServiceProvider, TInput, TNewResult>(
                Factory,
                provider =>
                {
                    var pipeline = PipelineFactory(provider);

                    return async input => await action(pipeline(input));
                }
            );
        }

        public AsyncContainerizedPipelineBuilder<TServiceProvider, TInput, TNewResult> WithStep<TStep, TNewResult>(
            Func<TStep, TResult, Task<TNewResult>> action
        )
        {
            return WithStep(
                provider => (TStep)Factory(provider, typeof(TStep)),
                action
            );
        }

        public AsyncContainerizedPipelineBuilder<TServiceProvider, TInput, TNewResult> WithStep<TStep, TNewResult>(
            Func<TServiceProvider, TStep> factory,
            Func<TStep, TResult, Task<TNewResult>> action
        )
        {
            return new AsyncContainerizedPipelineBuilder<TServiceProvider, TInput, TNewResult>(
                Factory,
                provider =>
                {
                    var stepService = factory(provider);
                    var pipeline = PipelineFactory(provider);

                    return async input => await action(stepService, pipeline(input));
                }
            );
        }
    }
}