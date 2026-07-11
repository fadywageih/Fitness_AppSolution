using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessEngine.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalculatedMetrics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Bmr = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Tdee = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    CalorieTarget = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CalculatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculatedMetrics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FitnessPlanConfigs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Goal = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PlanName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DurationWeeks = table.Column<int>(type: "int", nullable: false),
                    WorkoutsPerWeek = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FitnessPlanConfigs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserFitnessStats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(5,1)", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(5,1)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Goal = table.Column<int>(type: "int", nullable: false),
                    ActivityLevel = table.Column<int>(type: "int", nullable: false),
                    RecordedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFitnessStats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAssignedPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlanConfigId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeactivatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAssignedPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAssignedPlans_FitnessPlanConfigs_PlanConfigId",
                        column: x => x.PlanConfigId,
                        principalTable: "FitnessPlanConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalculatedMetrics_UserId",
                table: "CalculatedMetrics",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAssignedPlans_PlanConfigId",
                table: "UserAssignedPlans",
                column: "PlanConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAssignedPlans_UserId_IsActive",
                table: "UserAssignedPlans",
                columns: new[] { "UserId", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_UserFitnessStats_UserId",
                table: "UserFitnessStats",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalculatedMetrics");

            migrationBuilder.DropTable(
                name: "UserAssignedPlans");

            migrationBuilder.DropTable(
                name: "UserFitnessStats");

            migrationBuilder.DropTable(
                name: "FitnessPlanConfigs");
        }
    }
}
