var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// Registers the routing middleware in the application pipeline
// This middleware is responsible for matching incoming requests to endpoints
app.UseRouting();

// GetEndpoint() extension method to get endpoint called in current request
// 1. Logging
// 2. What middleware will be executede for a request
// 3. Inspect the route's path or method

app.Use(async (context, next) =>
{
    var endpoint = context.GetEndpoint();

    if (endpoint != null)
    {
        await context.Response.WriteAsync($"GetEndpoint() method: {endpoint}\n");
    }

    await next(context);
});

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

    endpoints.Map("api/downloads/{filename}.{extension}", async (context) =>
    {
        var fileName = context.Request.RouteValues["filename"].ToString();
        var extension = context.Request.RouteValues["extension"].ToString();
        await context.Response.WriteAsync($"Filename to download: {fileName}.{extension}");
    });

    endpoints.Map("api/user/{id=guest}", async (context) =>
    {
        var id = context.Request.RouteValues["id"].ToString();
        await context.Response.WriteAsync($"User's ID: {id}");
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
