using Sigma.Database.Models;

namespace Sigma.Domain.Interfaces;

public interface ICandidateService
{
    public Task AddOrUpdateCandidateAsync(Candidate candidate);
}

