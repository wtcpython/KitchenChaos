using UnityEngine;

public class StoveCounter : BaseCounter
{
    [SerializeField] private FryingRecipeListSO _fryingRecipeListSO;
    [SerializeField] private FryingRecipeListSO _burningRecipeListSO;
    [SerializeField] private StoveCounterVisual _stoveCounterVisual;
    [SerializeField] private ProgressBarUI _progressBarUI;
    [SerializeField] private AudioSource _sound;
    public enum StoveState
    {
        Idle,
        Frying,
        Burning
    }

    private FryingRecipeSO _fryingRecipeSO;
    private float _fryingTimer = 0;
    private StoveState _stoveState = StoveState.Idle;

    public override void Interact(Player player)
    {
        if (player.KitchenObject != null)
        {
            if (KitchenObject == null)
            {
                if (_fryingRecipeListSO.TryGetFryingRecipeSO(player.KitchenObject.GetKitchenObjectSO(), out FryingRecipeSO fryingRecipeSO))
                {
                    TransferKitchenObject(player, this);
                    StartFrying(fryingRecipeSO);
                }
                else if (_burningRecipeListSO.TryGetFryingRecipeSO(player.KitchenObject.GetKitchenObjectSO(), out FryingRecipeSO burningRecipeSO))
                {
                    TransferKitchenObject(player, this);
                    StartBurning(burningRecipeSO);
                }
                else
                {
                    Debug.LogWarning("No frying or burning recipe found for " + player.KitchenObject.GetKitchenObjectSO().name);
                }
            }
        }
        else
        {
            if (KitchenObject != null)
            {
                TurnToIdle();
                TransferKitchenObject(this, player);
            }
        }
    }

    void Update()
    {
        switch (_stoveState)
        {
            case StoveState.Idle:
                break;
            case StoveState.Frying:
                _fryingTimer += Time.deltaTime;
                _progressBarUI.UpdateProgress(_fryingTimer / _fryingRecipeSO.FryingTime);
                if (_fryingTimer >= _fryingRecipeSO.FryingTime)
                {
                    DestroyKitchenObject();
                    CreateKitchenObject(_fryingRecipeSO.Output.Prefab);
                    _stoveState = StoveState.Burning;

                    if (_burningRecipeListSO.TryGetFryingRecipeSO(KitchenObject.GetKitchenObjectSO(), out FryingRecipeSO burningRecipeSO))
                    {
                        StartBurning(burningRecipeSO);
                    }
                    else
                    {
                        Debug.LogWarning("Burning recipe not found for " + KitchenObject.GetKitchenObjectSO().name);
                        TurnToIdle();
                    }
                }
                break;
            case StoveState.Burning:
                _fryingTimer += Time.deltaTime;
                _progressBarUI.UpdateProgress(_fryingTimer / _fryingRecipeSO.FryingTime);
                if (_fryingTimer >= _fryingRecipeSO.FryingTime)
                {
                    DestroyKitchenObject();
                    CreateKitchenObject(_fryingRecipeSO.Output.Prefab);
                    TurnToIdle();
                }
                break;
            default:
                break;
        }
    }

    private void StartFrying(FryingRecipeSO fryingRecipeSO)
    {
        _fryingTimer = 0;
        _fryingRecipeSO = fryingRecipeSO;
        _stoveState = StoveState.Frying;
        _stoveCounterVisual.ShowStoveEffect();
        _sound.Play();
    }

    private void StartBurning(FryingRecipeSO fryingRecipeSO)
    {
        _fryingTimer = 0;
        _fryingRecipeSO = fryingRecipeSO;
        _stoveState = StoveState.Burning;
        _stoveCounterVisual.ShowStoveEffect();
        _sound.Play();
    }

    private void TurnToIdle()
    {
        _stoveState = StoveState.Idle;
        _stoveCounterVisual.HideStoveEffect();
        _progressBarUI.Hide();
        _sound.Pause();
    }
}
