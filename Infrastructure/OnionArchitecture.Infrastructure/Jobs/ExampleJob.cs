using Quartz;

namespace OnionArchitecture.Infrastructure.Jobs;

public class ExampleJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("Example job is worked");

        return Task.CompletedTask;
    }
}