using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Azure.Core;

namespace multitenancy.Application.Models;

public class Tenant
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    public Tenant(string name)
    {
        Id = new Guid();
        Name = name;
    }
}