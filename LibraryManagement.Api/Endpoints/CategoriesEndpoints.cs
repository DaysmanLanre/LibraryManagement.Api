using LibraryManagement.Api.Entities;
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
            .Produces<Category>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
        
        group.MapGet("/", GetAll);
    }

    private static async Task<IResult> GetbyId(int id)
    {
        var category = _categories.FirstOrDefault(
            category => category.id  == id);

        if (category == null) 
        {
            return Results.NotFound();
        }

        return Results.Ok(category);
    }


    private static async Task<IResult> GetAll() 
    {
        var categories = _categories;
        return Results.Ok(categories);
    }
}
