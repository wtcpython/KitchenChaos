using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSO;

    [SerializeField] private ContainerCounterVisual _containerCounterVisual;

    public override void Interact(Player player)
    {
        if (player.KitchenObject != null) return;

        CreateKitchenObject(_kitchenObjectSO.Prefab);
        TransferKitchenObject(this, player);

        _containerCounterVisual.PlayOpen();
    }
}
