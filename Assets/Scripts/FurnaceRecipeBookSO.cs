using UnityEngine;
using System;

[CreateAssetMenuAttribute(fileName = "RecipeBook", menuName = "Assets/Recipes/Furnace")]
public class FurnaceRecipeBookSO : ScriptableObject
{
    public FurnaceRecipe[] recipes;

    public bool RecipeMatch(ItemSO itemToMatch)
    {
        foreach(FurnaceRecipe recipe in recipes)
        {
            if(recipe.input == itemToMatch)
            {
                return true;
            }
        }
        return false;
    }

    public InventoryItem GetOutputItem(ItemSO itemToMatch)
    {
        foreach(FurnaceRecipe recipe in recipes)
        {
            if(recipe.input == itemToMatch)
            {
                return new InventoryItem(recipe.output, recipe.outputAmount);
            }
        }
        return null;
    }

    [Serializable]
    public class FurnaceRecipe
    {
        public ItemSO input;
        public int inputAmount;

        public ItemSO output;
        public int outputAmount;
    }
}
