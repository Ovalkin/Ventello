using System.ComponentModel.DataAnnotations;

namespace Vintello.Common.DTOs;

public class UpdatedUserDto
{
    public int? RoleId { get; set; }
    [RegularExpression(@"^[А-ЯЁ][а-яё]*$", ErrorMessage = "Имя должно начинатся с заглавной буквы!")]
    public string? FirstName { get; set; }
    [RegularExpression(@"^[А-ЯЁ][а-яё]*$", ErrorMessage = "Фамилия должна начинатся с заглавной буквы!")]
    public string? LastName { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    public string? Password { get; set; }
    [RegularExpression(@"^(([0-9])|(\+[0-9]))[0-9]{10}$", ErrorMessage = "Номер телефона не валиден!")]
    public string? Phone { get; set; }
    public string? Location { get; set; }
    public string? ProfilePic { get; set; }
    public string? Bio { get; set; }
}