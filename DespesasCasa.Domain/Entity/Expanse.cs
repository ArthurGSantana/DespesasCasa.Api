namespace DespesasCasa.Domain.Entity;

public class Expense : Base
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal Value { get; set; }
    public DateTime PaymentDate { get; set; }
    public string? Observation { get; set; }
    public Guid? CollectionId { get; set; }
    public Collection? Collection { get; set; }
}
