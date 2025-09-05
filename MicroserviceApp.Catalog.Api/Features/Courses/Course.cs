using MicroserviceApp.Catalog.Api.Features.Categories;
using MicroserviceApp.Catalog.Api.Repositories;

namespace MicroserviceApp.Catalog.Api.Features.Courses;

public class Course : BaseEntity
{
    public string Name { get; set; } = default!; //null olamaz
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public Guid UserId { get; set; }
    public string? Picture { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = default!; //navigation property her kursun bir kategorisi olacak
}