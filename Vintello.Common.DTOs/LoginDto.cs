using System.ComponentModel.DataAnnotations;

namespace Vintello.Common.DTOs;

public class LoginDto
{
    [Required(ErrorMessage = "Введите почту!")]
    [EmailAddress(ErrorMessage = "Введеная почта не является почтой:)!")]
    public string Email { get; set; } = null!;
    [Required(ErrorMessage = "Введите пароль!")]
    public string Password { get; set; } = null!;
}