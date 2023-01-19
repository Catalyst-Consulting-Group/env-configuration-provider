using System.ComponentModel.DataAnnotations;

namespace WebSample.Options;

public record GreetingOptions
{
    [Required]
    public required string Format { get; set; }

    [Required]
    public required string Name { get; set; }
}
