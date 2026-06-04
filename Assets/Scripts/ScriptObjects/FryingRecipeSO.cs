using System;
using System.Collections.Generic;

using UnityEngine;

[Serializable]
public class FryingRecipeSO
{
    public KitchenObjectSO Input;
    public KitchenObjectSO Output;
    public float FryingTime;
}

[CreateAssetMenu]
public class FryingRecipeListSO : ScriptableObject
{
    public List<FryingRecipeSO> List;

    public bool TryGetFryingRecipeSO(KitchenObjectSO inputKitchenObjectSO, out FryingRecipeSO fryingRecipeSO)
    {
        foreach (FryingRecipeSO recipe in List)
        {
            if (recipe.Input == inputKitchenObjectSO)
            {
                fryingRecipeSO = recipe;
                return true;
            }
        }

        fryingRecipeSO = null;
        return false;
    }
}
