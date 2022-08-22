using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using NET6_API_with_Generic_SQL_Repository.Interfaces;
using NET6_API_with_Generic_SQL_Repository.ViewModel;

namespace NET6_API_with_Generic_SQL_Repository.Controllers;

[ApiController]
[Route("api/example")]
public class ExampleEntityController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IExampleMapper _exampleMapper;
    // GET

    public ExampleEntityController(IUnitOfWork unitOfWork, IExampleMapper exampleMapper)
    {
        _unitOfWork = unitOfWork;
        _exampleMapper = exampleMapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetExamples()
    {
        var result = await _unitOfWork.Examples.GetAllAsync();
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("{exampleId}")]
    public async Task<IActionResult> GetExampleByExampleId(int exampleId)
    {
        var example = await _unitOfWork.Examples.GetByIdAsync(exampleId);
        if (example == null) return NotFound();
        return Ok(example);

    }

    [HttpPost]
    public async Task<IActionResult> AddExampleAsync([FromBody]ExampleViewModel model)
    {
        var example = _exampleMapper.Map(model);
        await _unitOfWork.Examples.AddAsync(example);
        if (await _unitOfWork.Complete()) return StatusCode(201);
        return StatusCode(409, "The Example was not added");
    }

    [HttpPut("{exampleId}")]
    public async Task<IActionResult> UpdateExampleAsync(int exampleId, [FromBody]ExampleViewModel model)
    {
        var example = await _unitOfWork.Examples.GetByIdAsync(exampleId);
        if (example == null) return NotFound();
        example = _exampleMapper.Map(model, example);
        _unitOfWork.Examples.Update(example);
        var result = _unitOfWork.Complete();
        return Ok(result);

    }

    [HttpDelete("{exampleId}")]
    public async Task<IActionResult> DeleteExampleAsync(int exampleId)
    {
        var example = await _unitOfWork.Examples.GetByIdAsync(exampleId);
        if (example == null) return NotFound();
        _unitOfWork.Examples.Remove(example);
        var result = _unitOfWork.Complete();
        return Ok(result);

    }
}

