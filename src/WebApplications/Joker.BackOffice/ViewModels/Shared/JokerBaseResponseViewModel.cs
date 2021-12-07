namespace Joker.BackOffice.ViewModels.Shared;

public class JokerBaseResponseViewModel<T>
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public T Payload { get; set; }
}