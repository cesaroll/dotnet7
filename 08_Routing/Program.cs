var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.Map("api", async (context) =>
    {
        await context.Response.WriteAsync("Common method get");
    });
});

app.Run();
