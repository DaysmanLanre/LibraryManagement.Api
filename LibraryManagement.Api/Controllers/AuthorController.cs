using LibraryManagement.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace LibraryManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorController : ControllerBase
{
    private static List<Authors> _authors = 
        [
            new Authors { Id = 1, FirstName = "John", LastName = "Dad", YearofBirth = 1981 },
            new Authors { Id = 2, FirstName = "Jane", LastName = "Mom", YearofBirth = 1982 },
            new Authors { Id = 3, FirstName = "Groffery", LastName = "Kid", YearofBirth = 1983 }

        ];

    [HttpGet]
    public async Task<ActionResult<List<Authors>>> GetAll()
    {
        return Ok(_authors);
    }


}
