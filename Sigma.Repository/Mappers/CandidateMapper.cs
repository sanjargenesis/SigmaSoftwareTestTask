using AutoMapper;
using Sigma.Database.Models;
using Sigma.Domain.DTOs;

namespace Sigma.Repository.Mappers;

public sealed class CandidateMapper : Profile
{
    public CandidateMapper()
    {
        CreateMap<CandidateDTO, Candidate>().ReverseMap();
    }
}
