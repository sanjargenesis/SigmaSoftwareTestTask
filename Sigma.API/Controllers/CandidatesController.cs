using Microsoft.AspNetCore.Mvc;
using Sigma.Domain.DTOs;
using Sigma.Domain.Interfaces;

namespace Sigma.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class CandidatesController : ControllerBase
{
    private readonly ICandidateService _service;

    public CandidatesController(ICandidateService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> AddOrUpdateCandidate([FromBody] CandidateDTO candidate)
    {
        await _service.AddOrUpdateCandidateAsync(candidate);
        return Ok();
    }
}
