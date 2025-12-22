using Device.Api.Models;
using Device.Application.Dtos;
using Device.Application.Services;
using Device.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Device.Api.Controllers;

[ApiController]
[Route("devices")]
public class DevicesController : ControllerBase
{
    private readonly DeviceService _service;

    public DevicesController(DeviceService service)
    {
        _service = service;
    }

    [HttpPost]
    [ProducesResponseType(typeof(DeviceResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] CreateDeviceRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _service.CreateAsync(request, cancellationToken);

        return CreatedAtAction(
            nameof(GetById), 
            new { id = result.Id },
            result
        );
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(DeviceResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await _service.GetByIdAsync(id, cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResponse<DeviceResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(
        [FromQuery] string? brand,
        [FromQuery] DeviceState? state,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        if (page < 1 || pageSize < 1)
            return BadRequest("Page and pageSize must be greater than zero.");

        var (items, totalCount) =
            await _service.GetAsync(brand, state, page, pageSize, cancellationToken);

        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        var response = new PagedResponse<DeviceResponse>(
            items,
            new PaginationMetadata(
                page,
                pageSize,
                totalCount,
                totalPages));

        return Ok(response);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(DeviceResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateDeviceRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _service.UpdateAsync(id, request, cancellationToken);
        return Ok(result);
    }

    [HttpPatch("{id:int}")]
    [ProducesResponseType(typeof(DeviceResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Patch(
        int id,
        [FromBody] PatchDeviceRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _service.PatchAsync(id, request, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Delete(
        int id,
        CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
