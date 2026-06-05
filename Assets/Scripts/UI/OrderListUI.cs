using System;
using System.Collections.Generic;

using UnityEngine;

public class OrderRecipeListUI : MonoBehaviour
{
    [SerializeField] private Transform _recipeParent;
    [SerializeField] private RecipeUI _recipeUITemplate;
    private void Start()
    {
        _recipeUITemplate.gameObject.SetActive(false);
        OrderManager.Instance.OnRecipeSpawned += OrderManager_OnRecipeSpawned;
        OrderManager.Instance.OnRecipeSuccessed += OrderManager_OnRecipeSuccessed;
    }

    private void OrderManager_OnRecipeSuccessed(object sender, EventArgs e)
    {
        UpdateUI();
    }
    private void OrderManager_OnRecipeSpawned(object sender, EventArgs e)
    {
        UpdateUI();
    }
    private void UpdateUI()
    {
        foreach (Transform child in _recipeParent)
        {
            if (child != _recipeUITemplate.transform)
            {
                Destroy(child.gameObject);
            }
        }
        List<RecipeSO> recipesSOList = OrderManager.Instance.GetOrderRecipeSOList();
        foreach (RecipeSO recipeSO in recipesSOList)
        {
            RecipeUI recipeUI = Instantiate(_recipeUITemplate);
            recipeUI.transform.SetParent(_recipeParent);
            recipeUI.gameObject.SetActive(true);
            recipeUI.UpdateUI(recipeSO);
        }
    }
}
