using Quartz;

namespace OnionArchitecture.Infrastructure.Extensions;

public static class QuartzExtensions
{
    public static void AddCronJob<T>(
        this IServiceCollectionQuartzConfigurator config,
        string jobName,
        string cronSchedule) where T : IJob
    {
        if (!string.IsNullOrEmpty(jobName) &&
            !string.IsNullOrEmpty(cronSchedule))
        {
            var jobKey = new JobKey(jobName);
            config.AddJob<T>(opt => opt.WithIdentity(jobKey));
            config.AddTrigger(opt =>
                opt.ForJob(jobKey)
                    .WithIdentity(jobName)
                    .WithCronSchedule(cronSchedule));
        }
    }
}