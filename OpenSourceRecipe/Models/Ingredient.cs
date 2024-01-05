using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace OpenSourceRecipes.Models;

public class Ingredient
{
    // Primary Key
    public int IngredientId { get; set; }

    // Foreign Key to the Recipe table
    public string? IngredientName { get; set; }

    // Nutrition information for the ingredient
    public string? Nutrition { get; set; }
}

public class CreateIngredientDto
{
    public string? IngredientName { get; set; }
    public string? Nutrition { get; set; }
}

public class GetIngredientDto
{
    public int IngredientId { get; set; }
    public string? IngredientName { get; set; }
    public string? Nutrition { get; set; }
}
