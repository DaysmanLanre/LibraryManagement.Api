using LibraryManagement.Api.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Reflection.Metadata.Ecma335;

namespace LibraryManagement.Api.Endpoints;

public static class CategoriesEndpoints
{
    private static List<Category> _categories = 
        [
            new Category {id =1, Name = "Biography"},
            new Category {id =2, Name = "Novel"}
        ];

    public static void MapCategoryEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("/api/categories")
            .WithTags("Categories");

        group.MapGet("/{id:int}", GetbyId)
            .WithName("GetCategoryById")
            .ProducesProblem(StatusCodes.Status404NotFound);
        
        group.MapGet("/", GetAll);

        group.MapPost("/", Create)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status409Conflict);
    }

    private static async Task<Results<Ok<Category>, ProblemHttpResult>> GetbyId(int id)
    {
        var category = _categories.FirstOrDefault(
            category => category.id  == id);

        if (category == null) 
        {
            return TypedResults.Problem(
                detail: $"A category with Id {id} has not been found.",
                statusCode: StatusCodes.Status404NotFound);
        }

        return TypedResults.Ok(category);
    }


    private static async Task<Ok<List<Category>>> GetAll() 
    {
        var categories = _categories;
        return TypedResults.Ok(categories);
    }

    private static async Task<Results<CreatedAtRoute<Category>, ProblemHttpResult>> Create(
        Category category)
    {
        if (string.IsNullOrWhiteSpace(category.Name)) 
        {
            return TypedResults.Problem(
                detail: $"A Category Name cannot be empty.",
                statusCode: StatusCodes.Status400BadRequest);
        }

        if (_categories.Any(existingCategory => existingCategory.id == category.id)) 
        {
            return TypedResults.Problem(
                detail: $"A Category with Id {category.id} already exists.",
                statusCode: StatusCodes.Status409Conflict);
        }

        if (_categories.Any(existingCategory => existingCategory.Name == category.Name))
        {
            return TypedResults.Problem(
                detail: $"A Category with Name {category.Name} already exists.",
                statusCode: StatusCodes.Status409Conflict);
        } 

        _categories.Add(category);

        return TypedResults.CreatedAtRoute(
            category,
            "GetCategoryById",
            new { id = category.id });
    }
}
