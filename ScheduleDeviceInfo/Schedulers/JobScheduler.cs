using Quartz;
using Quartz.Impl;

namespace ScheduleDeviceInfo.Schedulers
{
    public class JobScheduler
    {
        public static async Task StartAsync()

        {
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();

            await scheduler.Start();
            IJobDetail job = JobBuilder.Create<GetDeviceInfoJob>().Build();
            ITrigger trigger = TriggerBuilder.Create().StartNow()
            .WithSimpleSchedule(x => x
                .WithIntervalInSeconds(10)
                .RepeatForever())
            .Build();
           await scheduler.ScheduleJob(job, trigger);
        }
    }
}
