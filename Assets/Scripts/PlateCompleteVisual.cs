
using System;
using System.Collections.Generic;

using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{

    [Serializable]
    public class KitchenObjectSOModel
    {
        public KitchenObjectSO KitchenObjectSO;
        public GameObject Model;
    }
    [SerializeField] private List<KitchenObjectSOModel> _modelMap;

    public void ShowKitchenObject(KitchenObjectSO kitchenObjectSO)
    {
        foreach (KitchenObjectSOModel item in _modelMap)
        {
            if (item.KitchenObjectSO == kitchenObjectSO)
            {
                item.Model.SetActive(true);
                return;
            }
        }
    }
}
