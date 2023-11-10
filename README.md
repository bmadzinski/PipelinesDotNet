# PipelinesDotNet - simple pipeline creation tool for .Net
Create simple pipelines to process data. Build in support for dependency injection.

## Packages
* `PipelinesDotNet`
	* Core library
* `PipelinesDotNet.ServiceProvider`
	* Library with extension method to use `IServiceProvider` as container

## Examples

### Without using container
```csharp
var pipeline = new PipelineBuilder()
    .WithStep<int, int>(x => x * 2)
    .WithStep<int, int>(x => x + 2)
    .Pipeline;

var result = pipeline(10); //22
```

#### With async step
```csharp
var pipeline = new PipelineBuilder()
    .WithStep<int, int>(x => x * 2)
    .WithStep(async x =>
    {
        await Task.Delay(1000);
        return x + 2;
    })
    .Pipeline;

var result = await pipeline(10); //22
```

### Using container
```csharp
var pipelineFactory = new PipelineBuilder()
    .WithServiceProvider()
    .WithStep<Multiplication, int, int>((x, i) => x.Multiply(i))
    .WithStep<Addition, int>((x, i) => x.Add(i))
    .PipelineFactory;

var pipeline = pipelineFactory(serviceProvider)

var result = pipeline(10); //22
```

#### With async step
```csharp
var pipelineFactory = new PipelineBuilder()
    .WithServiceProvider()
    .WithStep<Multiplication, int, int>((x, i) => x.MultiplyAsync(i))
    .WithStep<Addition, int>((x, i) => x.AddAsync(i))
    .PipelineFactory;

var pipeline = pipelineFactory(serviceProvider)

var result = await pipeline(10); //22
```

### More examples
For more examples go to unit tests: 
[PipelineBuilderTests](PipelinesDotNet.Tests/PipelineBuilderTests.cs) or 
[ServiceProviderContenerizedPipelineBuilderTests](PipelinesDotNet.ServiceProvider.Tests/ServiceProviderContenerizedPipelineBuilderTests.cs).