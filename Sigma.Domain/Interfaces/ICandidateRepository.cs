using Sigma.Database.Models;

namespace Sigma.Domain.Interfaces;

public interface ICandidateRepository
{
    public Task<Candidate> GetByEmailAsync(string email);
    public Task AddAsync(Candidate candidate);
    public Task UpdateAsync(Candidate candidate);
}
