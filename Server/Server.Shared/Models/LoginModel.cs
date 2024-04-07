using System.ComponentModel.DataAnnotations;

namespace Server.Shared.Models;

public class LoginModel
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}

public record TokenMessage(string Token, DateTime Expiration, string Email);