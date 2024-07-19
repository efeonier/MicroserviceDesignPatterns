using System.ComponentModel.DataAnnotations;

namespace Shared;

public class StockNotReservedEvent
{
    public int OrderId { get; set; }

    [MaxLength(500)]
    public string Message { get; set; } = string.Empty;
}