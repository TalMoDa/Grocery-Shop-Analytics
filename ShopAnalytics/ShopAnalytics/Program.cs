using Serilog;
using ShopAnalytics.Build.DependencyInjection;
using ShopAnalytics.Build.Logger;
using ShopAnalytics.Build.RequestPipeline;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSettings();
builder.Services.AddDbContext();
builder.Services.AddAppMediatR();
builder.Services.AddServices();
builder.Host.UseSerilog((context, configuration) => { configuration.ReadFrom.Configuration(context.Configuration); });
builder.Services.UseProblemDetails();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policyBuilder => policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureSerilogLogger();
app.UseGlobalErrorHandling();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors("AllowAll");

app.Run();