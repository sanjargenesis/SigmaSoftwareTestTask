using Microsoft.AspNetCore.Mvc;
using Sigma.Database.Models;
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
    public async Task<IActionResult> AddOrUpdateCandidate([FromBody] Candidate candidate)
    {
        await _service.AddOrUpdateCandidateAsync(candidate);
        return Ok();
    }
}
