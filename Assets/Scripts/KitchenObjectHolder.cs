using System;

using UnityEngine;

public class KitchenObjectHolder : MonoBehaviour
{
    public static event EventHandler OnDrop;
    public static event EventHandler OnPickUp;
    [field: SerializeField]
    public Transform HoldPoint { get; private set; }

    private KitchenObject _kitchenObject;
    public KitchenObject KitchenObject
    {
        get => _kitchenObject;
        set
        {
            if (_kitchenObject != value && value != null && this is BaseCounter)
            {
                OnDrop?.Invoke(this, EventArgs.Empty);
            }
            else if (_kitchenObject != value && value != null && this is Player)
            {
                OnPickUp?.Invoke(this, EventArgs.Empty);
            }
            _kitchenObject = value;

            // Set the parent of the kitchen object to the hold point and reset its local position
            if (value != null)
            {
                value.transform.localPosition = Vector3.zero;
            }
        }
    }

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return KitchenObject.GetKitchenObjectSO();
    }

    public void TransferKitchenObject(KitchenObjectHolder sourceHolder, KitchenObjectHolder targetHolder)
    {
        if (sourceHolder.KitchenObject == null)
        {
            Debug.LogWarning("Source holder has no kitchen object to transfer!");
            return;
        }

        if (targetHolder.KitchenObject != null)
        {
            Debug.LogWarning("Target holder already has a kitchen object!");
            return;
        }

        targetHolder.AddKitchenObject(sourceHolder.KitchenObject);
        sourceHolder.KitchenObject = null;
    }

    public void AddKitchenObject(KitchenObject kitchenObject)
    {
        kitchenObject.transform.parent = HoldPoint;
        kitchenObject.transform.localPosition = Vector3.zero;
        KitchenObject = kitchenObject;
    }

    public void CreateKitchenObject(GameObject kitchenObjectPrefab)
    {
        GameObject obj = GameObject.Instantiate(kitchenObjectPrefab, HoldPoint);
        KitchenObject kitchenObject = obj.GetComponent<KitchenObject>();
        KitchenObject = kitchenObject;
    }

    public void DestroyKitchenObject()
    {
        if (KitchenObject != null)
        {
            Destroy(KitchenObject.gameObject);
            KitchenObject = null;
        }
    }

    public static void ClearStaticData()
    {
        OnDrop = null;
        OnPickUp = null;
    }
}
