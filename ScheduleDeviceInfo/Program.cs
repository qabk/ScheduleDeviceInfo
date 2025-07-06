using Quartz;
using ScheduleDeviceInfo.Entities;
using ScheduleDeviceInfo.Interfaces;
using ScheduleDeviceInfo.Repositories;
using ScheduleDeviceInfo.Schedulers;
using ScheduleDeviceInfo.Services;
using ScheduleDeviceInfo.Settings;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection(nameof(MongoDbSettings)));


builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IRawInfoDeviceRepository, RawInfoDeviceRepository>();
builder.Services.AddScoped<IRawInfoDeviceService, RawInfoDeviceService>();
builder.Services.AddScoped<GetDeviceInfoJob>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();

    var jobKey = new JobKey("GetDeviceInfoJob");

    q.AddJob<GetDeviceInfoJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("GetDeviceInfoJob-trigger")
        .WithSimpleSchedule(x => x
            .WithIntervalInSeconds(5)
            .RepeatForever())
    );
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

// Register your job and services
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();



