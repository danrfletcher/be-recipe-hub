using System.Security.Claims;
using System.Text;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Npgsql;
using OpenSourceRecipes.Models;

namespace OpenSourceRecipes.Services;
public class IngredientRepository
{
    private readonly IConfiguration _configuration;
    private readonly string? _connectionString;
        public IngredientRepository(IConfiguration configuration)
    {
        this._configuration = configuration;

        string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

        if (env == "Testing")
        {
            _connectionString = "TestConnection";
        }
        else if (env == "Production")
        {
            _connectionString = "ProductionConnection";
        }
        else
        {
            _connectionString = "DefaultConnection";
        }
    }
    public async Task<CreateIngredientDto> CreateIngredient(CreateIngredientDto Ingredient)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetConnectionString(_connectionString!));

        var parameters = new DynamicParameters();
        parameters.Add("IngredientTitle", Ingredient.IngredientTitle);
        parameters.Add("TagLine", Ingredient.TagLine);
        parameters.Add("Difficulty", Ingredient.Difficulty);
        parameters.Add("TimeToPrepare", Ingredient.TimeToPrepare);
        parameters.Add("IngredientMethod", Ingredient.IngredientMethod);
        parameters.Add("IngredientImg", Ingredient.IngredientImg);
        parameters.Add("Cuisine", Ingredient.Cuisine);
        parameters.Add("ForkedFromId", Ingredient.ForkedFromId);
        parameters.Add("OriginalIngredientId", Ingredient.OriginalIngredientId);
        parameters.Add("UserId", Ingredient.UserId);
        parameters.Add("CuisineId", Ingredient.CuisineId);
        
        var sql;
        var newRecipe = await connection.QuerySingleOrDefaultAsync<GetRecipeDto>(sql, parameters);

        if (newIngredient == null)
        {
            throw new Exception("Ingredient not created due to missing parameters");
        }

        return newIngredient;
    }

}