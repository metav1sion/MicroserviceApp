using MicroserviceApp.Catalog.Api.Features.Courses;
using MicroserviceApp.Catalog.Api.Repositories;

namespace MicroserviceApp.Catalog.Api.Features.Categories;

public class Category : BaseEntity
{
    public string Name { get; set; } = default!;
    public List<Course>? Courses { get; set; } //navigation property //her kategorinin birden fazla kursu olabilir.
}