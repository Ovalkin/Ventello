using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Test.Integration.Helpers;

public class Users
{
    public static readonly User AdminUser = new()
    {
        Id = 1,
        FirstName = "Админ",
        Role = RolesEnum.Admin,
        Email = "admin@example.com",
        Password = "пароль"
    };

    public static readonly User SuperAdminUser = new()
    {
        Id = 2,
        FirstName = "Супер",
        LastName = "Админ",
        Role = RolesEnum.SuperAdmin,
        Email = "supadmin@example.com",
        Password = "пароль"
    };

    public static readonly User ClientUser = new()
    {
        Id = 3,
        FirstName = "Клиент",
        Role = RolesEnum.Client,
        Email = "client@example.com",
        Password = "пароль"
    };
}