using Ahura.Web;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .InjectControllers()
    .InjectDbContext(builder.Configuration)
    .InjectAddSwaggerGen()
    .InjectServices();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
