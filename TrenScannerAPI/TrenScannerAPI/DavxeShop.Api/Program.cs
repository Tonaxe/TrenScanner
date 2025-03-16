using DavxeShop.Library.Services;
using DavxeShop.Library.Services.Interfaces;
using DavxeShop.Persistance;
using DavxeShop.Persistance.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins("http://localhost:4200", "http://localhost:44355")
              .AllowAnyMethod()
              .AllowAnyHeader());
});

builder.Services.AddTransient<ISeleniumService, SeleniumService>();
builder.Services.AddTransient<ICSVProcessorService, CSVProcessorService>();
builder.Services.AddTransient<ITrenDboHelper, TrenDboHelper>();
builder.Services.AddTransient<ITrenService, TrenService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddDbContextFactory<TrenScannerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TrenScanner API v1");
        c.RoutePrefix = string.Empty;
    });
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseCors("AllowFrontend");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();