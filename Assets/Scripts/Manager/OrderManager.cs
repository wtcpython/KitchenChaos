using System;
using System.Collections.Generic;

using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance { get; private set; }
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeSuccessed;
    public event EventHandler OnRecipeFailed;

    [SerializeField] private RecipeListSO _recipeSOList;
    [SerializeField] private int _orderMaxCount = 5;
    [SerializeField] private float _orderRate = 2;

    private List<RecipeSO> _orderRecipeSOList = new List<RecipeSO>();

    private float _orderTimer = 0;
    private bool _isStartOrder = false;
    private int _orderCount = 0;
    private int _successDeliveryCount = 0;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _isStartOrder = true;
    }
    private void Update()
    {
        if (_isStartOrder)
        {
            OrderUpdate();
        }
    }

    private void OrderUpdate()
    {
        _orderTimer += Time.deltaTime;
        if (_orderTimer >= _orderRate)
        {
            _orderTimer = 0;
            OrderANewRecipe();
        }
    }

    // 在倒计时结束时生成一个新的订单，如果订单数量已经达到最大值，则不生成新的订单。
    private void OrderANewRecipe()
    {
        if (_orderCount >= _orderMaxCount)
        {
            return;
        }
        _orderCount++;
        int index = UnityEngine.Random.Range(0, _recipeSOList.RecipeSOList.Count);
        _orderRecipeSOList.Add(_recipeSOList.RecipeSOList[index]);
        OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
    }
    public void DeliveryRecipe(PlateKitchenObject plateKitchenObject)
    {
        RecipeSO correctRecipe = null;
        foreach (RecipeSO recipe in _orderRecipeSOList)
        {
            if (IsCorrect(recipe, plateKitchenObject))
            {
                correctRecipe = recipe;
                break;
            }
        }

        if (correctRecipe == null)
        {
            OnRecipeFailed?.Invoke(this, EventArgs.Empty);
            print("上菜失败");
        }
        else
        {
            _ = _orderRecipeSOList.Remove(correctRecipe);
            OnRecipeSuccessed?.Invoke(this, EventArgs.Empty);
            _successDeliveryCount++;
            print("上菜成功");
        }
    }
    private bool IsCorrect(RecipeSO recipe, PlateKitchenObject plateKitchenObject)
    {
        List<KitchenObjectSO> list1 = recipe.KitchenObjectSOList;
        List<KitchenObjectSO> list2 = plateKitchenObject.GetKitchenObjectSOList();

        if (list1.Count != list2.Count) return false;

        foreach (KitchenObjectSO kitchenObjectSO in list1)
        {
            if (list2.Contains(kitchenObjectSO) == false)
            {
                return false;
            }
        }

        return true;
    }
    public List<RecipeSO> GetOrderRecipeSOList()
    {
        return _orderRecipeSOList;
    }

    public int GetSuccessDeliveryCount()
    {
        return _successDeliveryCount;
    }
}
