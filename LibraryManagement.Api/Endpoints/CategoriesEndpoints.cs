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

        group.MapGet("/", GetAll);
    }

    private static async Task<IResult> GetAll() 
    {
        var categories = _categories;
        return Results.Ok(categories);
    }
}
