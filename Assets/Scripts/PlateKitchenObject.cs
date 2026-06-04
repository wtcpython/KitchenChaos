using System.Collections.Generic;

using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectSO> _validKitchenObjectSOList;
    [SerializeField] private PlateCompleteVisual _plateCompleteVisual;
    [SerializeField] private KitchenObjectGridUI _kitchenObjectGridUI;

    private List<KitchenObjectSO> _kitchenObjectSOList = new List<KitchenObjectSO>();

    public bool AddKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
    {
        if (_kitchenObjectSOList.Contains(kitchenObjectSO))
            return false;

        if (_validKitchenObjectSOList.Contains(kitchenObjectSO) == false)
            return false;

        _plateCompleteVisual.ShowKitchenObject(kitchenObjectSO);
        _kitchenObjectGridUI.ShowKitchenObject(kitchenObjectSO);
        _kitchenObjectSOList.Add(kitchenObjectSO);
        return true;
    }
}
