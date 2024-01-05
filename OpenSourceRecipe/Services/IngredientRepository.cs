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
    public async Task<GetIngredientDto> CreateIngredient(CreateIngredientDto ingredient)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetConnectionString(_connectionString!));

        var parameters = new DynamicParameters();
        parameters.Add("IngredientName ", ingredient.IngredientName);
        parameters.Add("Nutrition", ingredient.Nutrition);
        
        var sql = "INSERT INTO \"Ingredient\" " +
                  "(\"IngredientName\", \"Nutrition\") " +
                  "VALUES (@IngredientName, @Nutrition) RETURNING *";
        var newIngredient = await connection.QuerySingleOrDefaultAsync<GetRecipeDto>(sql, parameters);

        if (newIngredient == null)
        {
            throw new Exception("Ingredient not created due to missing parameters");
        }

        return newIngredient;
    }

}