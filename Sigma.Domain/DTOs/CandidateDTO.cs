using Sigma.Database.Models;

namespace Sigma.Domain.DTOs;

public sealed record CandidateDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public TimeIntervalToCall? TimeIntervalToCall { get; set; }
    public string LinkedInProfileUrl { get; set; }
    public string GitHubProfileUrl { get; set; }
    public required string Comment { get; set; }
}
