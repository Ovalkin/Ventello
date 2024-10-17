using Microsoft.EntityFrameworkCore.Migrations;
using Vintello.Common.EntityModel.PostgreSql;

#nullable disable

namespace Vintello.Common.EntityModel.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class SeedPermissionRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            List<Permission> permissions = new();
            using (VintelloContext db = new())
            {
                permissions = db.Permissions.ToList();
            }

            foreach (var permission in permissions)
            {
                migrationBuilder.InsertData(
                    table: "roles_permissions",
                    columns: new[] { "role_id", "permission_id" },
                    values: new object[] {1, permission.Id});
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM permissions;");
        }
    }
}
