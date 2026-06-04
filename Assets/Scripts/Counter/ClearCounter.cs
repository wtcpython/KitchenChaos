using UnityEngine;

public class ClearCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        // 玩家手上有物品，且是盘子
        if (player.KitchenObject != null &&
            player.KitchenObject.TryGetComponent<PlateKitchenObject>(out PlateKitchenObject plateKitchenObject))
        {
            // 柜台为空 → 把盘子放上去
            if (KitchenObject == null)
            {
                TransferKitchenObject(player, this);
            }
            else
            {
                // 柜台上有物品 → 尝试往盘子里添加该物品的 SO
                bool isSuccessful = plateKitchenObject.AddKitchenObjectSO(GetKitchenObjectSO());
                if (isSuccessful)
                {
                    DestroyKitchenObject(); // 销毁柜台上的物品
                }
            }
        }
        else
        {
            // 玩家手上不是盘子（可能是其他食材，也可能是空手）
            if (player.KitchenObject != null)
            {
                // 玩家手上有非盘子食材
                if (KitchenObject == null)
                {
                    TransferKitchenObject(player, this);
                }
                else
                {
                    // 柜台上有物品，检查它是不是盘子
                    if (KitchenObject.TryGetComponent<PlateKitchenObject>(out PlateKitchenObject plateOnCounter))
                    {
                        // 柜台上的盘子尝试接收玩家手中的食材 SO
                        if (plateOnCounter.AddKitchenObjectSO(player.KitchenObject.GetKitchenObjectSO()))
                        {
                            player.DestroyKitchenObject(); // 销毁玩家手中的食材
                        }
                    }
                }
            }
            else
            {
                // 玩家空手
                if (KitchenObject != null)
                {
                    TransferKitchenObject(this, player);
                }
            }
        }
    }
}
