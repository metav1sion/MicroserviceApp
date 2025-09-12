using MicroserviceApp.Catalog.Api.Features.Categories.Create;

namespace MicroserviceApp.Catalog.Api.Features.Categories;

public static class CategoryEndpointExt
{
    public static void AddCategoryGroupEndpointExt(this WebApplication app)
    {
        app.MapGroup("/api/v1/categories").CreateCategoryGroupItemEndpoint();
    }
}