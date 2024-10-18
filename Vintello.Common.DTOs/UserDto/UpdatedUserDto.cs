using System.ComponentModel.DataAnnotations;
using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Common.DTOs;

public class UpdatedUserDto
{
    public RolesEnum? Role { get; set; }

    [RegularExpression(@"^[А-ЯЁ][а-яё]*$", ErrorMessage = "Имя должно начинатся с заглавной буквы!")]
    public string? FirstName { get; set; }

    [RegularExpression(@"^[А-ЯЁ][а-яё]*$", ErrorMessage = "Фамилия должна начинатся с заглавной буквы!")]
    public string? LastName { get; set; }

    [EmailAddress] public string? Email { get; set; }
    public string? Password { get; set; }

    [RegularExpression(@"^(([0-9])|(\+[0-9]))[0-9]{10}$", ErrorMessage = "Номер телефона не валиден!")]
    public string? Phone { get; set; }

    public string? Location { get; set; }
    public string? ProfilePic { get; set; }
    public string? Bio { get; set; }

    public static User CreateUser(UpdatedUserDto updatedUserDto, User user)
    {
        if (updatedUserDto.Role != null)
            user.Role = updatedUserDto.Role.Value;
        if (updatedUserDto.FirstName != null)
            user.FirstName = updatedUserDto.FirstName;
        if (updatedUserDto.LastName != null)
            user.LastName = updatedUserDto.LastName;
        if (updatedUserDto.Email != null)
            user.Email = updatedUserDto.Email;
        if (updatedUserDto.Password != null)
            user.Password = updatedUserDto.Password;
        if (updatedUserDto.Phone != null)
            user.Phone = updatedUserDto.Phone;
        if (updatedUserDto.Location != null)
            user.Location = updatedUserDto.Location;
        if (updatedUserDto.ProfilePic != null)
            user.ProfilePic = updatedUserDto.ProfilePic;
        if (updatedUserDto.Bio != null)
            user.Bio = updatedUserDto.Bio;
        user.UpdatedAt = DateTime.Now;
        return user;
    }
}