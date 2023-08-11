var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// Registers the routing middleware in the application pipeline
// This middleware is responsible for matching incoming requests to endpoints
app.UseRouting();

// Registers the endpoints execution middleware in the application pipeline
// This middleware is reponsible for executing the endpoints that are matched by the routing middleware
app.UseEndpoints(endpoints =>
{
    // Use Mpa, MapGet, MapPOst, etc to define endpoints.
    // They take a url, path and a delegate/lambda as input
    // The lambda/delegate will be executed when a request is rec3eived for that endpoint

    endpoints.Map("api", async (context) =>
    {
        await context.Response.WriteAsync("Common method for all");
    });

    endpoints.MapGet("api", async (context) =>
    {
        await context.Response.WriteAsync("only Get request");
    });

    endpoints.MapPost("api", async (context) =>
    {
        await context.Response.WriteAsync("only Post request");
    });


});

app.Run(async context => {
    // Default middleware
    await context.Response.WriteAsync("Default middleware, endpoint not found");
});

app.Run();
