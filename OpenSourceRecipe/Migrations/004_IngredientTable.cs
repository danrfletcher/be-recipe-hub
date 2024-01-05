using FluentMigrator;

namespace OpenSourceRecipes.Migrations;

[Migration(2024010104)]
public class CreateIngredientTable : Migration
{
    public override void Up()
    {
        Execute.Sql("CREATE TABLE \"Ingredient\"" +
                    "(" +
                    "\"IngredientId\" SERIAL PRIMARY KEY," +
                    "\"IngredientName\" VARCHAR(255) NOT NULL," +
                    "\"Nutrition\" TEXT NOT NULL," +

                    "PRIMARY KEY (\"IngredientId\", \"IngredientName\")" +
                    ");");
    }

    public override void Down()
    {
        Delete.Table("Ingredient");
    }
}
