using MediatR;
using MicroserviceApp.Catalog.Api.Features.Categories;
using MicroserviceApp.Catalog.Api.Features.Categories.Create;
using MicroserviceApp.Catalog.Api.Options;
using MicroserviceApp.Catalog.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Storage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptionExt();
builder.Services.AddDbServiceExt();

var app = builder.Build();

app.AddCategoryGroupEndpointExt();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();