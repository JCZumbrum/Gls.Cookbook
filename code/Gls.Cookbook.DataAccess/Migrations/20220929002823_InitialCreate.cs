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
            // measurments

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
                new object[] { "28.5 Ounce Can", "28.5 oz. can", (int)MeasurementType.Each, (int)MeasurementSystem.Universal });

            // ingredients

            migrationBuilder.InsertData(
                "Ingredients",
                new string[] { "Name" },
                new object[] { "Active Dry Yeast" });

            migrationBuilder.InsertData(
                "Ingredients",
                new string[] { "Name" },
                new object[] { "All Purpose Flour" });

            migrationBuilder.InsertData(
                "Ingredients",
                new string[] { "Name" },
                new object[] { "Almond Extract" });

            migrationBuilder.InsertData(
                "Ingredients",
                new string[] { "Name" },
                new object[] { "Almonds" });

            migrationBuilder.InsertData(
                "Ingredients",
                new string[] { "Name" },
                new object[] { "Anchovy Paste" });

            migrationBuilder.InsertData(
                "Ingredients",
                new string[] { "Name" },
                new object[] { "Apple" });

            migrationBuilder.InsertData(
                "Ingredients",
                new string[] { "Name" },
                new object[] { "Apple Cider" });

            migrationBuilder.InsertData(
                "Ingredients",
                new string[] { "Name" },
                new object[] { "Bacon" });

            migrationBuilder.InsertData(
                "Ingredients",
                new string[] { "Name" },
                new object[] { "Baking Powder" });

            migrationBuilder.InsertData(
                "Ingredients",
                new string[] { "Name" },
                new object[] { "Baking Soda" });

            migrationBuilder.InsertData(
                "Ingredients",
                new string[] { "Name" },
                new object[] { "Balsamic Vinegar" });

            migrationBuilder.InsertData(
                "Ingredients",
                new string[] { "Name" },
                new object[] { "Bay Leaves" });

            migrationBuilder.InsertData(
                "Ingredients",
                new string[] { "Name" },
                new object[] { "Beef Stock" });

            migrationBuilder.InsertData(
                "Ingredients",
                new string[] { "Name" },
                new object[] { "Black Pepper" });

            migrationBuilder.InsertData(
                "Ingredients",
                new string[] { "Name" },
                new object[] { "Black Peppercorns" });

            migrationBuilder.InsertData(
                "Ingredients",
                new string[] { "Name" },
                new object[] { "Brown Sugar" });

            migrationBuilder.InsertData(
                "Ingredients",
                new string[] { "Name" },
                new object[] { "Butter" });

            migrationBuilder.InsertData(
                "Ingredients",
                new string[] { "Name" },
                new object[] { "Canned Chiles" });

            migrationBuilder.InsertData(
                "Ingredients",
                new string[] { "Name" },
                new object[] { "Canned Tomatoes" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Capers" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Carrots" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Cayenne Pepper" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Celery" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Chicken" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Chicken Breasts" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Chicken Stock" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Chili Powder" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Ground Cinnamon" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Cinnamon Sticks" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Coarse Salt" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Coconut Milk" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Cornstarch" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Couscous" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Crushed Tomatoes" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Cumin" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Curry Powder" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Diced Tomatoes" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Dijon Mustard" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Dried Basil" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Dried Bread Crumbs" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Dried Oregano" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Dried Parsley" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Dried Rosemary" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Dried Sage" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Dried Taragon" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Dried Thyme" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Dry Mustard" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Eggs" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Fish Sauce" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Flour" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Fresh Basil" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Fresh Chiles" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Fresh Ginger" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Fresh Oregano" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Fresh Parsley" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Fresh Rosemary" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Fresh Sage" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Fresh Spinach" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Fresh Taragon" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Fresh Thyme" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Frozen Spinach" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Garlic Cloves" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Garlic Powder" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Ginger" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Gold Potatoes" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Granulated Sugar" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Ground Beef" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Ground Ginger" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Ground Nutmeg" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Heavy Cream" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Honey" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Hot Sauce" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Instant Yeast" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Italian Seasoning" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Ketchup" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Kidney Beans" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Kosher Salt" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Lemon Juice" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Lemons" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Lentils" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Lime Juice" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Limes" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Maple Syrup" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Marjoram" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Mayonnaise" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Milk" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Mushrooms" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Mustard" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Nutmeg" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Olive Oil" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Olives" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Onion" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Onion Powder" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Orange" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Orange Juice" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Panko Bread Crumbs" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Paprika" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Parmesan" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Pecorino" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Pepper" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Pork" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Potatoes" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Powdered Sugar" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Prepared Horseradish" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Red Onion" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Red Pepper Flakes" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Red Wine" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Rice" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Rice Wine Vinegar" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Russet Potatoes" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Salt" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Salted Butter" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "San Marzano Tomatoes" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Scallions" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Shallots" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Shrimp" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Soy Sauce" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Sugar" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Sweet Cream Butter" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Sweet Onion" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Tomato Paste" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Tomato Sauce" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Turmeric" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Unsalted Butter" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Vanilla Extract" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Vegetable Oil" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Vinegar" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "White Sugar" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "White Wine" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Whole Milk" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Whole Nutmeg" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Worcestershire Sauce" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Yellow Mustard" });

            migrationBuilder.InsertData(
            "Ingredients",
            new string[] { "Name" },
            new object[] { "Yellow Onion" });
        }
    }
}
