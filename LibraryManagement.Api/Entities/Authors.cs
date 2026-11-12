namespace LibraryManagement.Api.Entities;

public class Authors
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public int? YearofBirth { get; set; }
}
