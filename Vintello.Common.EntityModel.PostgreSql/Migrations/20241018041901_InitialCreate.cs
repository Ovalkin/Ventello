using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vintello.Common.EntityModel.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "categories_id_seq");

            migrationBuilder.CreateSequence<int>(
                name: "items_id_seq");

            migrationBuilder.CreateSequence<int>(
                name: "roles_id_seq");

            migrationBuilder.CreateSequence<int>(
                name: "users_id_seq");

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('categories_id_seq'::regclass)"),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("categories_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('roles_id_seq'::regclass)"),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("roles_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('users_id_seq'::regclass)"),
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    first_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    last_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    location = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    profile_pic = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    bio = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("users_pkey", x => x.id);
                    table.ForeignKey(
                        name: "users_role_id_fkey",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "items",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('items_id_seq'::regclass)"),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    category_id = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "character varying", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false, defaultValueSql: "'created'::character varying"),
                    images = table.Column<List<string>>(type: "text[]", nullable: false),
                    price = table.Column<decimal>(type: "money", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("items_pkey", x => x.id);
                    table.ForeignKey(
                        name: "items_category_id_fkey",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "items_user_id_fkey",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_items_category_id",
                table: "items",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_items_user_id",
                table: "items",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "roles_name_key",
                table: "roles",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_role_id",
                table: "users",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "users_email_key",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "users_phone_key",
                table: "users",
                column: "phone",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "items");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropSequence(
                name: "categories_id_seq");

            migrationBuilder.DropSequence(
                name: "items_id_seq");

            migrationBuilder.DropSequence(
                name: "roles_id_seq");

            migrationBuilder.DropSequence(
                name: "users_id_seq");
        }
    }
}
