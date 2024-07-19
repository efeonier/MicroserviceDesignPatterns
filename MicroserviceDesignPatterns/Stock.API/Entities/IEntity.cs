using System.ComponentModel.DataAnnotations;

namespace Stock.API.Entities;

public interface IEntity
{
    [Key]
    int Id { get; set; }
}