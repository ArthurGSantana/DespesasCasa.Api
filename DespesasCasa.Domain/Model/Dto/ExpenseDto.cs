namespace DespesasCasa.Domain.Model.Dto;

public class ExpenseDto
{
    public Guid? Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal? Value { get; set; }
    public DateTime? PaymentDate { get; set; }
    public string? Observation { get; set; }
}
