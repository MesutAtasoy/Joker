using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Management.Infrastructure.Migrations
{
    public partial class DbInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Badges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValue: new Guid("1313de76-39d8-4e03-ac41-aa2e92afcb49")),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Code = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    FontColor = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    BackgroundColor = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Badges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessDirectories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValue: new Guid("93501ebb-0fe2-449a-b96d-f2159e49ef50")),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Icon = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    ImageUrl = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessDirectories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValue: new Guid("46312bd4-c023-4475-88d9-49e35acf1dc1")),
                    Code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    ImageUrl = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValue: new Guid("e9da72ac-154d-4e1e-867a-b613d722ff5a")),
                    Code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    IsoCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    ImageUrl = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValue: new Guid("61a24fa2-2a91-4acb-bc6d-81dc05b6da6e")),
                    Code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    ImageUrl = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SocialProviders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValue: new Guid("079ca133-fabc-424f-954d-6687b9a41e5f")),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Icon = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    ImageUrl = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialProviders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PricingPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValue: new Guid("40f04675-9cf0-4bda-9ef8-ec618e8ad0ca")),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Slug = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    CurrencyId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricingPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PricingPlan_Currency",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PricingPlans_CurrencyId",
                table: "PricingPlans",
                column: "CurrencyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Badges");

            migrationBuilder.DropTable(
                name: "BusinessDirectories");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "PricingPlans");

            migrationBuilder.DropTable(
                name: "SocialProviders");

            migrationBuilder.DropTable(
                name: "Currencies");
        }
    }
}
