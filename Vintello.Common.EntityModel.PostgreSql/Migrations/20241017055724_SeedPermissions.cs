using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vintello.Common.EntityModel.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class SeedPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            foreach (var action in Enum.GetNames(typeof(Actions)))
            {
                foreach (var entity in Enum.GetNames(typeof(Entities)))
                {
                    migrationBuilder.InsertData(
                        table: "permissions",
                        columns: new[] { "action", "entity" },
                        values: new object[] {Enum.Parse<Actions>(action), Enum.Parse<Entities>(entity)});
                }
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM permissions;");
        }
    }
}