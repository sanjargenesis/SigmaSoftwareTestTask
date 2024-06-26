using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sigma.Database.Models;
using Sigma.Domain.DTOs;
using Sigma.Domain.Exceptions;
using Sigma.Domain.Interfaces;
using System.Text.RegularExpressions;

namespace Sigma.Service.Services;

public sealed class CandidateService : ICandidateService
{
    private readonly ICandidateRepository _repository;
    private readonly IMapper _mapper;

    public CandidateService(ICandidateRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <summary>
    /// Adds or updates if candidate exists
    /// </summary>
    /// <param name="candidate"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task AddOrUpdateCandidateAsync(CandidateDTO candidate)
    {
        try
        {
            var existingCandidate = await _repository.GetByEmailAsync(candidate.Email);
            
            if (!IsValidEmail(candidate.Email))
            {
                throw new EmailValidationException("Invalid email format.");
            }

            if (existingCandidate is not null)
            {
                existingCandidate.FirstName = candidate.FirstName;
                existingCandidate.LastName = candidate.LastName;
                existingCandidate.PhoneNumber = candidate.PhoneNumber;
                existingCandidate.PreferredCallTime = candidate.PreferredCallTime;
                existingCandidate.LinkedInProfileUrl = candidate.LinkedInProfileUrl;
                existingCandidate.GitHubProfileUrl = candidate.GitHubProfileUrl;
                existingCandidate.Comment = candidate.Comment;
                existingCandidate.UpdatedAt = DateTime.UtcNow;

                await _repository.UpdateAsync(existingCandidate);
            }
            else
            {
                await _repository.AddAsync(_mapper.Map<Candidate>(candidate));
            }
        }
        catch (DbUpdateException dbEx)
        {
            // Handle database update exceptions
            throw new Exception("An error occurred while updating the database.", dbEx);
        }
        catch (ArgumentNullException argNullEx)
        {
            // Handle argument null exceptions
            throw new Exception("A required argument was null.", argNullEx);
        }
        catch (InvalidOperationException invalidOpEx)
        {
            // Handle invalid operations
            throw new Exception("An invalid operation occurred.", invalidOpEx);
        }
        catch (Exception ex)
        {
            // Handle all other exceptions
            throw new Exception("An unknown error occurred.", ex);
        }
    }

    private static bool IsValidEmail(string email)
    {
        var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        if (string.IsNullOrEmpty(email))
            return false;

        var regex = new Regex(emailPattern);
        return regex.IsMatch(email);
    }
}

