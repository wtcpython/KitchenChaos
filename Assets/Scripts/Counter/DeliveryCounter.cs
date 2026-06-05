using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (player.KitchenObject != null
        && player.KitchenObject.TryGetComponent<PlateKitchenObject>(out PlateKitchenObject plateKitchenObject))
        {
            OrderManager.Instance.DeliveryRecipe(plateKitchenObject);
            player.DestroyKitchenObject();
        }
    }
}
