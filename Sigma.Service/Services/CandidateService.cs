using Sigma.Database.Models;
using Sigma.Domain.Interfaces;

namespace Sigma.Service.Services;

public sealed class CandidateService : ICandidateService
{
    private readonly ICandidateRepository _repository;

    public CandidateService(ICandidateRepository repository)
    {
        _repository = repository;
    }

    public async Task AddOrUpdateCandidateAsync(Candidate candidate)
    {
        var existingCandidate = await _repository.GetByEmailAsync(candidate.Email);

        if (existingCandidate is not null)
        {
            existingCandidate.FirstName = candidate.FirstName;
            existingCandidate.LastName = candidate.LastName;
            existingCandidate.PhoneNumber = candidate.PhoneNumber;
            existingCandidate.PreferredCallTime = candidate.PreferredCallTime;
            existingCandidate.LinkedInProfileUrl = candidate.LinkedInProfileUrl;
            existingCandidate.GitHubProfileUrl = candidate.GitHubProfileUrl;
            existingCandidate.Comment = candidate.Comment;

            await _repository.UpdateAsync(existingCandidate);
        }
        else
        {
            await _repository.AddAsync(candidate);
        }
    }
}

