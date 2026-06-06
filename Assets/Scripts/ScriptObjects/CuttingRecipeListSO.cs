using System;
using System.Collections.Generic;

using UnityEngine;

[Serializable]
public class CuttingRecipeSO
{
    public KitchenObjectSO Input;
    public KitchenObjectSO Output;
    public int MaxCuttingCount;
}

[CreateAssetMenu]
public class CuttingRecipeListSO : ScriptableObject
{
    public List<CuttingRecipeSO> List;

    public bool TryGetCuttingRecipeSO(KitchenObjectSO input, out CuttingRecipeSO cuttingRecipeSO)
    {
        foreach (CuttingRecipeSO recipe in List)
        {
            if (recipe.Input == input)
            {
                cuttingRecipeSO = recipe;
                return true;
            }
        }

        cuttingRecipeSO = null;
        return false;
    }
}
