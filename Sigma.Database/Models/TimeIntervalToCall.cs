using System.ComponentModel.DataAnnotations;

namespace Sigma.Database.Models;

public sealed class TimeIntervalToCall
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Range(0, 23)]
    public int StartHour { get; set; }

    [Required]
    [Range(0, 23)]
    public int EndHour { get; set; }

    public override string ToString()
    {
        return $"{StartHour}:00 - {EndHour}:00";
    }
}
