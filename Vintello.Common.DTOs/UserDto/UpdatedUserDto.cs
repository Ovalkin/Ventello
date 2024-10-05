using System.ComponentModel.DataAnnotations;
using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Common.DTOs;

public class UpdatedUserDto
{
    public int? RoleId { get; set; }

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

    public static User CreateUser(UpdatedUserDto retrievedUserDto, User user)
    {
        if (retrievedUserDto.RoleId != null)
            user.RoleId = (int)retrievedUserDto.RoleId;
        if (retrievedUserDto.FirstName != null)
            user.FirstName = retrievedUserDto.FirstName;
        if (retrievedUserDto.LastName != null)
            user.LastName = retrievedUserDto.LastName;
        if (retrievedUserDto.Email != null)
            user.Email = retrievedUserDto.Email;
        if (retrievedUserDto.Password != null)
            user.Password = retrievedUserDto.Password;
        if (retrievedUserDto.Phone != null)
            user.Phone = retrievedUserDto.Phone;
        if (retrievedUserDto.Location != null)
            user.Location = retrievedUserDto.Location;
        if (retrievedUserDto.ProfilePic != null)
            user.ProfilePic = retrievedUserDto.ProfilePic;
        if (retrievedUserDto.Bio != null)
            user.Bio = retrievedUserDto.Bio;
        user.UpdatedAt = DateTime.Now;
        return user;
    }
}