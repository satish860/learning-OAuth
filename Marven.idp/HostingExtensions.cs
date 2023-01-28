using Marven.idp.Entities;
using Marven.idp.Services;
using MongoDB.Driver;
using Serilog;

namespace Marven.idp;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        // uncomment if you want to add a UI
        builder.Services.AddRazorPages();
        ConfigureMongoDB(builder);
        builder.Services.AddTransient<SeedDatabase>();
        builder.Services.AddScoped<ILocalStore,LocalStore>();

        builder.Services.AddIdentityServer(options =>
            {
                // https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/api_scopes#authorization-based-on-scopes
                options.EmitStaticAudienceClaim = true;
            })
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiResources(Config.ApiResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients)
            .AddTestUsers(TestUsers.Users);

        return builder.Build();
    }
    
    public static WebApplication ConfigurePipeline(this WebApplication app)
    { 
        app.UseSerilogRequestLogging();
    
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        // uncomment if you want to add a UI
        app.UseStaticFiles();
        app.UseRouting();
            
        app.UseIdentityServer();

        // uncomment if you want to add a UI
        app.UseAuthorization();
        app.MapRazorPages().RequireAuthorization();

        return app;
    }


    private static void ConfigureMongoDB(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IMongoClient, MongoClient>(s =>
        {
            var uri = s.GetRequiredService<IConfiguration>()["DBHOST"];
            if(string.IsNullOrEmpty(uri))
            {
                return new MongoClient();
            }
            return new MongoClient(uri);
        });
        builder.Services.AddSingleton<IMongoCollection<User>>(s =>
        {
            var mongoClient = s.GetRequiredService<IMongoClient>();
            var DBName = s.GetRequiredService<IConfiguration>()["DBNAME"];
            var database = mongoClient.GetDatabase(DBName);
            var collection = database.GetCollection<User>("User");
            return collection;
        });
    }
}
