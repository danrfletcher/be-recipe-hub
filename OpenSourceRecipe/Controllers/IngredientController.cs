using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenSourceRecipes.Models;
using OpenSourceRecipes.Services;

namespace OpenSourceRecipe.Controllers;

[ApiController]
public class IngredientController(IngredientRepository ingredientRepository) : ControllerBase
{
    [HttpPost("api/ingredients")]
    public async Task<ActionResult<GetIngredientDto>> CreateIngredient(CreateIngredientDto ingredient)
    {
        try
        {
            return await ingredientRepository.CreateIngredient(ingredient);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
