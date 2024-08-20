namespace DespesasCasa.Domain.Entity;

public abstract class Base
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool Active { get; set; } = true;
}
