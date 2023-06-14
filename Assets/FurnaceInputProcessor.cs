using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceInputProcessor : MonoBehaviour
{
    #region singleton
    private static FurnaceInputProcessor _instance;
    public static FurnaceInputProcessor I { get { return _instance; } }

    public Furnace linkedFurnace;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
    #endregion

    public FurnaceRecipeBookSO recipeBook;

    public void AddItemToFurance(InventoryItem _item)
    {
        if(recipeBook.RecipeMatch(_item.itemRef))
        {
            if(linkedFurnace.TryAddSmeltItem(_item))
            {
                CharacterController.I.inventory.RemoveItem(_item);
            }
            return;
        }
    }
}
