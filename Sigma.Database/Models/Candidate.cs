using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sigma.Database.Models;

[Index(nameof(Email), IsUnique = true)]
public sealed class Candidate : BaseModel
{
    [Required]
    public required string FirstName { get; set; }
    
    [Required]
    public required string LastName { get; set; }

    [Phone]
    public string? PhoneNumber { get; set; }
   
    [Required]
    public required string Email { get; set; }
    
    [ForeignKey(nameof(TimeIntervalToCallId))]
    public TimeIntervalToCall? TimeIntervalToCall { get; set; }

    public int TimeIntervalToCallId { get; set; }

    public string? LinkedInProfileUrl { get; set; }

    public string? GitHubProfileUrl { get; set; }

    [Required]
    public required string Comment { get; set; }
}
