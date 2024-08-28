using DespesasCasa.Domain.Enum;

namespace DespesasCasa.Domain.Model.Response;

public class ErrorViewModel
{
    public List<Error> Errors { get; private set; }

    public ErrorViewModel()
    {
        Errors = new List<Error>();
    }

    public ErrorViewModel(List<Error> errors)
    {
        Errors = errors;
    }

    public ErrorViewModel(Error error)
    {
        Errors = new List<Error> { error };
    }

     public ErrorViewModel(string code, string? message = "")
    {

        this.Errors = new List<Error>()
        {
            new Error()
            {
                Code = code,
                Message = message
            }
        };

    }

    public ErrorViewModel(IEnumerable<string> errors)
    {
        this.Errors = errors.Select(e => new Error() { Code = System.Enum.GetName(ErrorCodeEnum.ValidationError)?.ToSnakeCase()?.ToLower() ?? "", Message = e });
    }
}
