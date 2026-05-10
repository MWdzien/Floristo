namespace Floristo.Infrastructure.EF.Models;

internal class CustomerReadModel
{
    public Guid Id { get; set; }
    public int Version { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeactivatedAt { get; set; }
    
}