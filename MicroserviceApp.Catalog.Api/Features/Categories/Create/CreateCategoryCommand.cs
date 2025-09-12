using MediatR;
using MicroserviceApp.Shared;

namespace MicroserviceApp.Catalog.Api.Features.Categories.Create;

public record CreateCategoryCommand(string Name): IRequest<ServiceResult<CreateCategoryResponse>>;