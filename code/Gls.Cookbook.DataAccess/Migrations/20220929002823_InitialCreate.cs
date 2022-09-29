using Gls.Cookbook.Domain;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gls.Cookbook.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Note = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Measurements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Abbreviation = table.Column<string>(type: "TEXT", nullable: true),
                    MeasurementType = table.Column<int>(type: "INTEGER", nullable: false),
                    MeasurementSystem = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Tags = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecipeNotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RecipeId = table.Column<int>(type: "INTEGER", nullable: true),
                    Index = table.Column<int>(type: "INTEGER", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeNotes_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RecipeSections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RecipeId = table.Column<int>(type: "INTEGER", nullable: true),
                    Index = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeSections_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RecipeIngredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RecipeSectionId = table.Column<int>(type: "INTEGER", nullable: true),
                    Index = table.Column<int>(type: "INTEGER", nullable: false),
                    IngredientId = table.Column<int>(type: "INTEGER", nullable: false),
                    QuantityText = table.Column<string>(type: "TEXT", nullable: true),
                    Quantity = table.Column<double>(type: "REAL", nullable: false),
                    MeasurementId = table.Column<int>(type: "INTEGER", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeIngredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Measurements_MeasurementId",
                        column: x => x.MeasurementId,
                        principalTable: "Measurements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_RecipeSections_RecipeSectionId",
                        column: x => x.RecipeSectionId,
                        principalTable: "RecipeSections",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RecipeInstructions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RecipeSectionId = table.Column<int>(type: "INTEGER", nullable: true),
                    Index = table.Column<int>(type: "INTEGER", nullable: false),
                    Direction = table.Column<string>(type: "TEXT", nullable: true),
                    Note = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeInstructions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeInstructions_RecipeSections_RecipeSectionId",
                        column: x => x.RecipeSectionId,
                        principalTable: "RecipeSections",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_IngredientId",
                table: "RecipeIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_MeasurementId",
                table: "RecipeIngredients",
                column: "MeasurementId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_RecipeSectionId",
                table: "RecipeIngredients",
                column: "RecipeSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeInstructions_RecipeSectionId",
                table: "RecipeInstructions",
                column: "RecipeSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeNotes_RecipeId",
                table: "RecipeNotes",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeSections_RecipeId",
                table: "RecipeSections",
                column: "RecipeId");

            SeedData(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeIngredients");

            migrationBuilder.DropTable(
                name: "RecipeInstructions");

            migrationBuilder.DropTable(
                name: "RecipeNotes");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Measurements");

            migrationBuilder.DropTable(
                name: "RecipeSections");

            migrationBuilder.DropTable(
                name: "Recipes");
        }

        private void SeedData(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                "Measurements",
                new string[] { "Name", "Abbreviation", "MeasurementType", "MeasurementSystem" },
                new object[] { "Teaspoon", "tsp.", (int)MeasurementType.Volume, (int)MeasurementSystem.UsCustomary });

            migrationBuilder.InsertData(
                "Measurements",
                new string[] { "Name", "Abbreviation", "MeasurementType", "MeasurementSystem" },
                new object[] { "Tablespoon", "tbsp.", (int)MeasurementType.Volume, (int)MeasurementSystem.UsCustomary });

            migrationBuilder.InsertData(
                "Measurements",
                new string[] { "Name", "Abbreviation", "MeasurementType", "MeasurementSystem" },
                new object[] { "Cup", "c.", (int)MeasurementType.Volume, (int)MeasurementSystem.UsCustomary });

            migrationBuilder.InsertData(
                "Measurements",
                new string[] { "Name", "Abbreviation", "MeasurementType", "MeasurementSystem" },
                new object[] { "Pint", "pt.", (int)MeasurementType.Volume, (int)MeasurementSystem.UsCustomary });

            migrationBuilder.InsertData(
                "Measurements",
                new string[] { "Name", "Abbreviation", "MeasurementType", "MeasurementSystem" },
                new object[] { "Quart", "qt.", (int)MeasurementType.Volume, (int)MeasurementSystem.UsCustomary });

            migrationBuilder.InsertData(
                "Measurements",
                new string[] { "Name", "Abbreviation", "MeasurementType", "MeasurementSystem" },
                new object[] { "Fluid Ounce", "fl. oz.", (int)MeasurementType.Volume, (int)MeasurementSystem.UsCustomary });

            migrationBuilder.InsertData(
                "Measurements",
                new string[] { "Name", "Abbreviation", "MeasurementType", "MeasurementSystem" },
                new object[] { "Milliliter", "ml", (int)MeasurementType.Volume, (int)MeasurementSystem.Metric });

            migrationBuilder.InsertData(
                "Measurements",
                new string[] { "Name", "Abbreviation", "MeasurementType", "MeasurementSystem" },
                new object[] { "Liter", "l", (int)MeasurementType.Volume, (int)MeasurementSystem.Metric });

            migrationBuilder.InsertData(
                "Measurements",
                new string[] { "Name", "Abbreviation", "MeasurementType", "MeasurementSystem" },
                new object[] { "Ounce", "oz.", (int)MeasurementType.Weight, (int)MeasurementSystem.UsCustomary });

            migrationBuilder.InsertData(
                "Measurements",
                new string[] { "Name", "Abbreviation", "MeasurementType", "MeasurementSystem" },
                new object[] { "Pound", "lb.", (int)MeasurementType.Weight, (int)MeasurementSystem.UsCustomary });

            migrationBuilder.InsertData(
                "Measurements",
                new string[] { "Name", "Abbreviation", "MeasurementType", "MeasurementSystem" },
                new object[] { "Milligram", "mg", (int)MeasurementType.Weight, (int)MeasurementSystem.Metric });

            migrationBuilder.InsertData(
                "Measurements",
                new string[] { "Name", "Abbreviation", "MeasurementType", "MeasurementSystem" },
                new object[] { "Gram", "g", (int)MeasurementType.Weight, (int)MeasurementSystem.Metric });

            migrationBuilder.InsertData(
                "Measurements",
                new string[] { "Name", "Abbreviation", "MeasurementType", "MeasurementSystem" },
                new object[] { "Each", "each", (int)MeasurementType.Each, (int)MeasurementSystem.Agnostic });
        }
    }
}
