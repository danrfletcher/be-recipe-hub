using FluentMigrator;

namespace OpenSourceRecipes.Migrations;

[Migration(2024010102)]
public class CreateCuisineTable : Migration
{
    public override void Up()
    {
        Execute.Sql("CREATE TABLE \"Cuisine\"" +
                    "(" +
                    "\"CuisineId\" INT NOT NULL PRIMARY KEY," +
                    "\"CuisineName\" VARCHAR(255) NOT NULL," +
                    "\"Description\" TEXT NOT NULL," +
                    "\"CuisineImg\" VARCHAR(255) NOT NULL" +
                    ")");
    }

    public override void Down()
    {
        Delete.Table("Cuisine");
    }
}
