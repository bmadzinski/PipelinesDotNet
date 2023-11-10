using System;
using System.Threading.Tasks;

namespace PipelinesDotNet
{
    public class PipelineBuilder
    {
        public PipelineBuilder<TInput, TResult> WithStep<TInput, TResult>(Func<TInput, TResult> action)
        {
            return new PipelineBuilder<TInput, TResult>(input => action(input));
        }

        public AsyncPipelineBuilder<TInput, TResult> WithStep<TInput, TResult>(Func<TInput, Task<TResult>> action)
        {
            return new AsyncPipelineBuilder<TInput, TResult>(input => action(input));
        }
    }

    public class AsyncPipelineBuilder<TInput, TResult>
    {
        public Func<TInput, Task<TResult>> Pipeline { get; }

        public AsyncPipelineBuilder(Func<TInput, Task<TResult>> pipeline)
        {
            Pipeline = pipeline;
        }

        public AsyncPipelineBuilder<TInput, TNewResult> WithStep<TNewResult>(Func<TResult, TNewResult> action)
        {
            return new AsyncPipelineBuilder<TInput, TNewResult>(async input => action(await Pipeline(input)));
        }

        public AsyncPipelineBuilder<TInput, TNewResult> WithStep<TNewResult>(Func<TResult, Task<TNewResult>> action)
        {
            return new AsyncPipelineBuilder<TInput, TNewResult>(async input => await action(await Pipeline(input)));
        }
    }

    public class PipelineBuilder<TInput, TResult>
    {
        public Func<TInput, TResult> Pipeline { get; }

        public PipelineBuilder(Func<TInput, TResult> pipeline)
        {
            Pipeline = pipeline;
        }

        public PipelineBuilder<TInput, TNewResult> WithStep<TNewResult>(Func<TResult, TNewResult> action)
        {
            return new PipelineBuilder<TInput, TNewResult>(input => action(Pipeline(input)));
        }

        public AsyncPipelineBuilder<TInput, TNewResult> WithStep<TNewResult>(Func<TResult, Task<TNewResult>> action)
        {
            return new AsyncPipelineBuilder<TInput, TNewResult>(async input => await action(Pipeline(input)));
        }
    }
}
