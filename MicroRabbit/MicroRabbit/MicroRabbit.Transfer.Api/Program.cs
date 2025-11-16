using MicroRabbit.Banking.Domain.Events;
using MicroRabbit.Infra.IoC;
using MicroRabbit.Transfer.Data.Context;
using MicroRabbit.Transfer.Domain.EventHandlers;
using MicroRabbit.Transfer.Domain.Events;
using Microsoft.EntityFrameworkCore;
using MiroRabbit.Domain.Core.Bus;
using TrasferCreatedEvent = MicroRabbit.Transfer.Domain.Events.TrasferCreatedEvent;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddDbContext<TransferDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("TransferDbConnection"));
});

DependencyContainer.RegisterServices(builder.Services);


// Add services to the container.

using (ServiceProvider serviceProvider = builder.Services.BuildServiceProvider())
{
    var eventBus = serviceProvider.GetRequiredService<IEventBus>();
    eventBus.Subscribe<TrasferCreatedEvent, TransferEventHandler>();
}

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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
