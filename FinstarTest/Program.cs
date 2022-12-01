using Business_Logic.Services;
using BusinessLogic.Implementations;
using BusinessLogic.Implementations.Mapping;
using Data_Access;
using Data_Access.Mapping;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    ConfigureSwagger(options);

    static void ConfigureSwagger(SwaggerGenOptions cfg)
    {
        var xmlFiles = Directory.EnumerateFiles(AppContext.BaseDirectory)
            .Where(f => Path.GetExtension(f) == ".xml" && File.Exists(Path.ChangeExtension(f, ".dll")));
        foreach (string path2 in xmlFiles)
        {
            string str = Path.Combine(AppContext.BaseDirectory, path2);
            if (File.Exists(str))
                cfg.IncludeXmlComments(str);
        }
    }
});
builder.Services.AddDbContext<FinstarContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddScoped<ITodosRepository, TodosRepository>();
builder.Services.AddAutoMapper(typeof(BusinessLogicProfile),typeof(DataAccessProfile));
builder.Logging.ClearProviders();
builder.Host.UseNLog();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<FinstarContext>();
    await db.Database.EnsureDeletedAsync();
    await db.Database.EnsureCreatedAsync();
}
app.Run();
