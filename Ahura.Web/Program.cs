using Ahura.Web;
using Radenoor.Middleware;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .InjectLogger()
    .InjectControllers()
    .InjectMapster()
    .InjectDbContext(builder.Configuration)
    .InjectAddSwaggerGen()
    .InjectUnitOfWork()
    .InjectServices();

var app = builder.Build();

app.UseMiddleware<ExceptionHandler>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
