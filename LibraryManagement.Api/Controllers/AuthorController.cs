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

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Authors), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Authors>> GetById(int id)
    {
        var author = _authors.FirstOrDefault(author => author.Id == id);

        if (author is null)
        {
            return Problem(
                detail: $"An author with ID {id} was not found.",
                statusCode: StatusCodes.Status404NotFound);
        }

        return Ok(author);
    }


    [HttpGet]
    [ProducesResponseType(typeof(List<Authors>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<Authors>>> GetAll()
    {
        return Ok(_authors);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Authors), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<Authors>> Create (Authors authors)
    {
        if(string.IsNullOrWhiteSpace(authors.FirstName) || string.IsNullOrWhiteSpace(authors.LastName))
        {
            return Problem(
                detail: $"Author's first name and last name cannot be empty.",
                statusCode: StatusCodes.Status400BadRequest);
        }

        if (_authors.Any(exisitingAuthor => exisitingAuthor.Id == authors.Id))
        {
            return Problem(
                detail: $"Author with ID {authors.Id} already exists.",
                statusCode: StatusCodes.Status409Conflict);
        }

        _authors.Add(authors);
        return CreatedAtAction(
            nameof(GetById), 
            new { id = authors.Id }, 
            authors);
    }



    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(Authors), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Authors>> Update(int id, Authors authors)
    {
        if (string.IsNullOrWhiteSpace(authors.FirstName) || string.IsNullOrWhiteSpace(authors.LastName))
        {
            return Problem(
                detail: "Author's first name and last name cannot be empty.",
                statusCode: StatusCodes.Status400BadRequest);
        }

        var existingAuthor = _authors.FirstOrDefault(author => author.Id == id);

        if (existingAuthor is null)
        {
            return Problem(
                detail: $"An author with ID {id} was not found.",
                statusCode: StatusCodes.Status404NotFound);
        }

        existingAuthor.FirstName = authors.FirstName;
        existingAuthor.LastName = authors.LastName;
        existingAuthor.YearofBirth = authors.YearofBirth;

        return Ok(existingAuthor);
    }


    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var authorToBeDeleted = _authors.FirstOrDefault(author => author.Id == id);

        if (authorToBeDeleted is null)
        {
            return Problem(
                detail: $"An author with ID {id} was not found.",
                statusCode: StatusCodes.Status404NotFound);
        }

        _authors.Remove(authorToBeDeleted);

        return NoContent();
    }


}
