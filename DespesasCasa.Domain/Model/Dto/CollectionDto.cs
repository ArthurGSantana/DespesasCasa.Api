using System;

namespace DespesasCasa.Domain.Model.Dto;

public class CollectionDto
{
    public Guid? Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public List<ExpanseDto>? Expanses { get; set; }
}
