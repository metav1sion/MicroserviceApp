using MicroserviceApp.Catalog.Api.Options;
using MongoDB.Driver;

namespace MicroserviceApp.Catalog.Api.Repositories;

public static class RepositoryExt
{
    public static IServiceCollection AddDbServiceExt(this IServiceCollection services)
    {
        services.AddSingleton<IMongoClient, MongoClient>(sp =>
        {
            var option = sp.GetRequiredService<MongoOption>();
            return new MongoClient(option.ConnectionString);
        });
        services.AddScoped(sp =>
        {
            var option = sp.GetRequiredService<MongoOption>();
            var client = sp.GetRequiredService<IMongoClient>();
            var database = client.GetDatabase(option.DatabaseName);
            return AppDbContext.Create(client.GetDatabase(option.DatabaseName));
        });
        return services;
    }
}