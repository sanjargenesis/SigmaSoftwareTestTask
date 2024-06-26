using Microsoft.EntityFrameworkCore;
using Sigma.Database.Contexts;
using Sigma.Database.Models;
using Sigma.Domain.Interfaces;

namespace Sigma.Repository.Repositories;

public sealed class CandidateRepository : ICandidateRepository
{
    private readonly Context _context;

    public CandidateRepository(Context context)
    {
        _context = context;
    }

    public async Task<Candidate> GetByEmailAsync(string email)
    {
        return await _context.Candidates.FirstOrDefaultAsync(c => c.Email == email);
    }

    public async Task AddAsync(Candidate candidate)
    {
        await _context.Candidates.AddAsync(candidate);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Candidate candidate)
    {
        _context.Candidates.Update(candidate);
        await _context.SaveChangesAsync();
    }
}

