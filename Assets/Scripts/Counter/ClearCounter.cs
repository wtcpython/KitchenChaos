using UnityEngine;
using UnityEngine.InputSystem;

public class ClearCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (player.KitchenObject != null)
        {
            if (KitchenObject == null)
            {
                TransferKitchenObject(player, this);
            }
        }
        else
        {
            if (KitchenObject != null)
            {
                TransferKitchenObject(this, player);
            }
        }
    }
}
