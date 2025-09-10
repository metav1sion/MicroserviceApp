using System.Reflection;
using MicroserviceApp.Catalog.Api.Features.Categories;
using MicroserviceApp.Catalog.Api.Features.Courses;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;

namespace MicroserviceApp.Catalog.Api.Repositories;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Category> Categories { get; set; }
    
    //static create method
    public static AppDbContext Create(IMongoDatabase database)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>().UseMongoDB(database.Client,database.DatabaseNamespace.DatabaseName);
        return new AppDbContext(optionsBuilder.Options);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}