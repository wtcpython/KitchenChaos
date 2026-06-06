using System;

using UnityEngine;

public class CuttingCounter : BaseCounter
{
    public static event EventHandler OnCut;

    [SerializeField] private CuttingRecipeListSO _cuttingRecipeListSO;

    [SerializeField] private ProgressBarUI _progressBarUI;

    [SerializeField] private CuttingCounterVisual _cuttingCounterVisual;

    private int _cuttingCount = 0;

    public override void Interact(Player player)
    {
        if (player.KitchenObject != null)
        {
            if (KitchenObject == null)
            {
                _cuttingCount = 0;
                TransferKitchenObject(player, this);
            }
        }
        else
        {
            if (KitchenObject != null)
            {
                TransferKitchenObject(this, player);
                _progressBarUI.Hide();
            }
        }
    }

    public override void Operate(Player player)
    {
        if (KitchenObject != null)
        {
            if (_cuttingRecipeListSO.TryGetCuttingRecipeSO(KitchenObject.GetKitchenObjectSO(), out CuttingRecipeSO cuttingRecipeSO))
            {
                Cut();

                _progressBarUI.UpdateProgress((float)_cuttingCount / cuttingRecipeSO.MaxCuttingCount);

                if (_cuttingCount == cuttingRecipeSO.MaxCuttingCount)
                {
                    DestroyKitchenObject();
                    CreateKitchenObject(cuttingRecipeSO.Output.Prefab);
                }
            }
        }
    }

    private void Cut()
    {
        OnCut?.Invoke(this, EventArgs.Empty);
        _cuttingCount++;
        _cuttingCounterVisual.PlayCut();
    }

    public static new void ClearStaticData()
    {
        OnCut = null;
    }
}
