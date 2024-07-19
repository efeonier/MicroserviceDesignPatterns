using System.ComponentModel.DataAnnotations;

namespace Order.API.Entities;

public interface IEntity
{
    [Key]
    int Id { get; set; }
}