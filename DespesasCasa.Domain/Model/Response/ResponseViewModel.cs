namespace DespesasCasa.Domain.Model;

public class ResponseViewModel<T>
{
    public T? Data { get; set; }
    public long? Count { get; set; }

    public ResponseViewModel(T data, long? count = null)
    {
        Data = data;
        Count = count;
    }
}
