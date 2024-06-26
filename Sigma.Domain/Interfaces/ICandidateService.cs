using Sigma.Domain.DTOs;

namespace Sigma.Domain.Interfaces;

public interface ICandidateService
{
    public Task AddOrUpdateCandidateAsync(CandidateDTO candidate);
}

