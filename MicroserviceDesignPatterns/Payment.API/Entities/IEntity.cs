using System.ComponentModel.DataAnnotations;

namespace Payment.API.Entities;

public interface IEntity
{
    [Key]
    int Id { get; set; }
}