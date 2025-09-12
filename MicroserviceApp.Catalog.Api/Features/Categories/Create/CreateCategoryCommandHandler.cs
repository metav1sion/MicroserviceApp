using System.Net;
using MassTransit;
using MediatR;
using MicroserviceApp.Catalog.Api.Repositories;
using MicroserviceApp.Shared;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceApp.Catalog.Api.Features.Categories.Create;

public class CreateCategoryCommandHandler(AppDbContext context) : IRequestHandler<CreateCategoryCommand, ServiceResult<CreateCategoryResponse>>
{
    public async Task<ServiceResult<CreateCategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var existingCategory = await context.Categories.AnyAsync(x=>x.Name == request.Name, cancellationToken);
        
        if (existingCategory)
        {
            ServiceResult<CreateCategoryResponse>.Error("Category with the same name already exists",$"The category name '{request.Name}' is already in use.",HttpStatusCode.BadRequest);
        }
        var category = new Category
        {
            Name = request.Name,
            Id = NewId.NextSequentialGuid()
        };
        
        await context.AddAsync(category, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        
        return ServiceResult<CreateCategoryResponse>.SuccessAsCreated(new CreateCategoryResponse(category.Id),"<empty>");

    }
}