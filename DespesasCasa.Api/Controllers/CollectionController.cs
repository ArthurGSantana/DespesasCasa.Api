

using DespesasCasa.Domain.Interface.Service;
using DespesasCasa.Domain.Model;
using DespesasCasa.Domain.Model.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DespesasCasa.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CollectionController(ICollectionService _collectionService) : ControllerBase
{
    /// <summary>
    /// Get Collection
    /// </summary>
    /// <param name="collectionId"></param>
    /// <returns></returns>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpGet("{collectionId}")]
    public async Task<IActionResult> GetCollectionAsync(Guid collectionId)
    {
        var collection = await _collectionService.GetCollectionAsync(collectionId);
        return Ok(new ResponseViewModel<CollectionDto>(collection));
    }

    /// <summary>
    /// Get Collections
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var collections = await _collectionService.GetAllAsync();
        return Ok(new ResponseViewModel<List<CollectionDto>>(collections));
    }

    /// <summary>
    /// Create Collection
    /// </summary>
    /// <param name="collection"></param>
    /// <returns></returns>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CollectionDto collection)
    {
        var collectionId = await _collectionService.CreateAsync(collection);
        return Ok(new ResponseViewModel<Guid>(collectionId));
    }

    /// <summary>
    /// Delete Collection
    /// </summary>
    /// <param name="collectionId"></param>
    /// <returns></returns>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpDelete("{collectionId}")]
    public async Task<IActionResult> DeleteAsync(Guid collectionId)
    {
        var result = await _collectionService.DeleteAsync(collectionId);
        return Ok(new ResponseViewModel<bool>(result));
    }
}
