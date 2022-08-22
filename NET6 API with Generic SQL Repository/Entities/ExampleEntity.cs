namespace NET6_API_with_Generic_SQL_Repository.Entities;

public class ExampleEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Exists { get; set; }
    
    public int UserId { get; set; }
}