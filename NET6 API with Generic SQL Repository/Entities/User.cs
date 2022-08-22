namespace NET6_API_with_Generic_SQL_Repository.Entities;

public class User 
{
    public int Id { get; set; }
    public string? GoogleUserId { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
}