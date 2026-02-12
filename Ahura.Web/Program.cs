using Ahura.Web;
using Radenoor.Middleware;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .InjectControllers()
    .InjectDbContext(builder.Configuration)
    .InjectAddSwaggerGen()
    .InjectServices();

var app = builder.Build();

app.UseMiddleware<ExceptionHandler>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
