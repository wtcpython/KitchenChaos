using System;

using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnObjectTrashed;
    public override void Interact(Player player)
    {
        if (player.KitchenObject != null)
        {
            player.DestroyKitchenObject();
            OnObjectTrashed?.Invoke(this, EventArgs.Empty);
        }
    }
    public static new void ClearStaticData()
    {
        OnObjectTrashed = null;
    }
}
