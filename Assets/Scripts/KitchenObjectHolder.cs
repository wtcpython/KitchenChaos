using UnityEngine;

public class KitchenObjectHolder : MonoBehaviour
{
    [field: SerializeField]
    public Transform HoldPoint { get; private set; }

    private KitchenObject _kitchenObject;
    public KitchenObject KitchenObject
    {
        get => _kitchenObject;
        set
        {
            _kitchenObject = value;
            if (_kitchenObject != null)
            {
                _kitchenObject.transform.localPosition = Vector3.zero;
            }
        }
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
}
