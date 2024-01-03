using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using OpenSourceRecipes.Models;

namespace OpenSourceRecipes.Services;
public class RecipeRepository
{

	private readonly IConfiguration _configuration;
	private readonly string? _connectionString;

    public UserRepository(IConfiguration configuration)
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

    public async Task<CreateRecipeDto?> CreateRecipe(string recipe, User user)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetConnectionString(_connectionString!));

        var parameters = new DynamicParameters();
        parameters.Add("RecipeTitle", recipe.RecipeTitle);
        parameters.Add("TagLine", recipe.TagLine);
        parameters.Add("Difficulty", recipe.Difficulty);
        parameters.Add("TimeToPrepare", recipe.TimeToPrepare);
        parameters.Add("RecipeMethod", recipe.RecipeMethod);
        parameters.Add("Cuisine", recipe.Cuisine);
        parameters.Add("RecipeImg", recipe.RecipeImg);
        parameters.Add("ForkedFrom", recipe.ForkedFrom);
        parameters.Add("OriginalRecipeId", recipe.OriginalRecipeId);
        parameters.Add("UserId", user.UserId);
        parameters.Add("CuisineId", recipe.CuisineId); //Cuisine Id is an integer in cuisine table - won't recipes have multiple cuisines?

        var sql = "INSERT INTO Recipe " +
                  "(\"RecipeTitle\", \"TagLine\", \"Difficulty\", \"TimeToPrepare\", \"RecipeMethod\", \"Cuisine\", \"RecipeImg\", \"ForkedFrom\", \"OriginalRecipeId\", \"UserId\", \"CuisineId\") " +
                  "VALUES (@RecipeTitle, @TagLine, @Difficulty, @TimeToPrepare, @RecipeMethod, @Cuisine, @RecipeImg, @ForkedFrom, @OriginalRecipeId, @UserId, @CuisineId) RETURNING *";
        
        var newRecipe = await connection.QueryAsync<Recipe>(sql, parameters);

    }

}