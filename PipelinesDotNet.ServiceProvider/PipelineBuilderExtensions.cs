using Microsoft.Extensions.DependencyInjection;
using System;

namespace PipelinesDotNet.ServiceProvider
{
    public static class PipelineBuilderExtensions
    {
        public static ContainerizedPipelineBuilder<IServiceProvider> WithServiceProvider(this PipelineBuilder _)
        {
            return new ContainerizedPipelineBuilder<IServiceProvider>((provider, type) => provider.GetRequiredService(type));
        }

        public static ContainerizedPipelineBuilder<IServiceProvider, TInput, TResult> WithServiceProvider<TInput, TResult>(
            this PipelineBuilder<TInput, TResult> pipelineBuilder
        )
        {
            return new ContainerizedPipelineBuilder<IServiceProvider, TInput, TResult>(
                (provider, type) => provider.GetRequiredService(type),
                provider => pipelineBuilder.Pipeline
            );
        }

        public static AsyncContainerizedPipelineBuilder<IServiceProvider, TInput, TResult> WithServiceProvider<TInput, TResult>(
            this AsyncPipelineBuilder<TInput, TResult> pipelineBuilder
        )
        {
            return new AsyncContainerizedPipelineBuilder<IServiceProvider, TInput, TResult>(
                (provider, type) => provider.GetRequiredService(type),
                provider => pipelineBuilder.Pipeline
            );
        }
    }
}
