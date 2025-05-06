namespace TestTask.Application.Models;

public class Envelope
{
    public Envelope(object? result, Error[]? errors)
    {
        if (result is not null && errors is not null)
        {
            throw new ArgumentException("Successful operation can not have errors");
        }

        Result = result;
        Errors = errors;
        GeneratedAt = DateTime.UtcNow;
    }

    public object? Result { get; set; }
    
    public Error[]? Errors { get; set; }

    public DateTime GeneratedAt { get; set; }
}