using System.ComponentModel.DataAnnotations;

namespace PlantManagerServer.Models;

public class RegistrationRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    [Required]
    public string UserName { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}