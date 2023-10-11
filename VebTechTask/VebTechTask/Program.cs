using VebTechTask.BLL.GlobalExceptionHandler;
using VebTechTask.UI.ServiceCollection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServices(builder);

var app = builder.Build();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty;
});

app.UseMiddleware(typeof(ExceptionHandlingMiddleware));
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
