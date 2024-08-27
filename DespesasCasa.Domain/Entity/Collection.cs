namespace DespesasCasa.Domain.Entity;

public class Collection : Base
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public List<Expense>? Expanses { get; set; }
}
