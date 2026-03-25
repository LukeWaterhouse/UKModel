using Energy.Infrastructure.DependencyInjection;
using UKModel.DbLoader.Elexon;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>()
    ?? [];

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.SetIsOriginAllowed(origin =>
                new Uri(origin).Host == "localhost" ||
                allowedOrigins.Contains(origin, StringComparer.OrdinalIgnoreCase))
              .AllowAnyHeader()
              .AllowAnyMethod());
});

builder.Services.AddEnergyInfrastructureServices(builder.Configuration);
builder.Services.AddTransient<FuelHalfHourLoader>();
builder.Services.AddHostedService<FuelHalfHourIngestService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

app.UseCors();

app.MapControllers();

app.Run();