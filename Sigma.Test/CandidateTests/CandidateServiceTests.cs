using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Sigma.Database.Models;
using Sigma.Domain.DTOs;
using Sigma.Domain.Exceptions;
using Sigma.Domain.Interfaces;
using Sigma.Service.Services;

namespace Sigma.Test.CandidateTests;

public class CandidateServiceTests
{
    private readonly Mock<ICandidateRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CandidateService _candidateService;

    public CandidateServiceTests()
    {
        _repositoryMock = new Mock<ICandidateRepository>();
        _mapperMock = new Mock<IMapper>();
        _candidateService = new CandidateService(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task AddOrUpdateCandidateAsync_ValidEmail_AddsCandidate()
    {
        // Arrange
        var candidateDTO = new CandidateDTO
        {
            FirstName = "Sanjar",
            LastName = "Shavkatkhujaev",
            Email = "Sanjar@example.com",
            Comment = "A comment"
        };

        var candidate = new Candidate
        {
            FirstName = "Sanjar",
            LastName = "Shavkatkhujaev",
            Email = "Sanjar@example.com",
            Comment = "A comment"
        };

        _repositoryMock.Setup(repo => repo.GetByEmailAsync(candidateDTO.Email)).ReturnsAsync((Candidate)null);
        _mapperMock.Setup(mapper => mapper.Map<Candidate>(candidateDTO)).Returns(candidate);

        // Act
        await _candidateService.AddOrUpdateCandidateAsync(candidateDTO);

        // Assert
        _repositoryMock.Verify(repo => repo.AddAsync(candidate), Times.Once);
        _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Candidate>()), Times.Never);
    }

    [Fact]
    public async Task AddOrUpdateCandidateAsync_ExistingCandidate_UpdatesCandidate()
    {
        // Arrange
        var candidateDTO = new CandidateDTO
        {
            FirstName = "Sanjar",
            LastName = "Shavkatkhujaev",
            Email = "Sanjar@example.com",
            Comment = "A comment"
        };

        var existingCandidate = new Candidate
        {
            Id = 1,
            FirstName = "Sanjar",
            LastName = "Shavkatkhujaev",
            Email = "Sanjar@example.com",
            Comment = "Another comment"
        };

        _repositoryMock.Setup(repo => repo.GetByEmailAsync(candidateDTO.Email)).ReturnsAsync(existingCandidate);

        // Act
        await _candidateService.AddOrUpdateCandidateAsync(candidateDTO);

        // Assert
        _repositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Candidate>()), Times.Never);
        _repositoryMock.Verify(repo => repo.UpdateAsync(existingCandidate), Times.Once);
        Assert.Equal(candidateDTO.FirstName, existingCandidate.FirstName);
        Assert.Equal(candidateDTO.LastName, existingCandidate.LastName);
        Assert.Equal(candidateDTO.Comment, existingCandidate.Comment);
    }

    [Fact]
    public async Task AddOrUpdateCandidateAsync_InvalidEmail_ThrowsValidationException()
    {
        // Arrange
        var candidateDTO = new CandidateDTO
        {
            FirstName = "Sanjar",
            LastName = "Shavkatkhujaev",
            Email = "invalid-email",
            Comment = "A comment"
        };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<EmailValidationException>(() => _candidateService.AddOrUpdateCandidateAsync(candidateDTO));
        Assert.Equal("Invalid email format.", exception.Message);
    }

    [Fact]
    public async Task AddOrUpdateCandidateAsync_RepositoryThrowsDbUpdateException_ThrowsException()
    {
        // Arrange
        var candidateDTO = new CandidateDTO
        {
            FirstName = "Sanjar",
            LastName = "Shavkatkhujaev",
            Email = "Sanjar@example.com",
            Comment = "A comment"
        };

        _repositoryMock.Setup(repo => repo.GetByEmailAsync(candidateDTO.Email)).ThrowsAsync(new DbUpdateException());

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => _candidateService.AddOrUpdateCandidateAsync(candidateDTO));
        Assert.Equal("An error occurred while updating the database.", exception.Message);
    }

    [Fact]
    public async Task AddOrUpdateCandidateAsync_RepositoryThrowsArgumentNullException_ThrowsException()
    {
        // Arrange
        var candidateDTO = new CandidateDTO
        {
            FirstName = "Sanjar",
            LastName = "Shavkatkhujaev",
            Email = "Sanjar@example.com",
            Comment = "A comment"
        };

        _repositoryMock.Setup(repo => repo.GetByEmailAsync(candidateDTO.Email)).ThrowsAsync(new ArgumentNullException());

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => _candidateService.AddOrUpdateCandidateAsync(candidateDTO));
        Assert.Equal("A required argument was null.", exception.Message);
    }

    [Fact]
    public async Task AddOrUpdateCandidateAsync_RepositoryThrowsInvalidOperationException_ThrowsException()
    {
        // Arrange
        var candidateDTO = new CandidateDTO
        {
            FirstName = "Sanjar",
            LastName = "Shavkatkhujaev",
            Email = "Sanjar@example.com",
            Comment = "A comment"
        };

        _repositoryMock.Setup(repo => repo.GetByEmailAsync(candidateDTO.Email)).ThrowsAsync(new InvalidOperationException());

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => _candidateService.AddOrUpdateCandidateAsync(candidateDTO));
        Assert.Equal("An invalid operation occurred.", exception.Message);
    }
}
