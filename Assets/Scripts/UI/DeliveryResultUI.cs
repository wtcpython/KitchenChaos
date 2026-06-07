using UnityEngine;

public class DeliveryResultUI : MonoBehaviour
{
    private const string IS_SHOW = "IsShow";
    [SerializeField] private Animator _deliverySuccessUIAnimatior;
    [SerializeField] private Animator _deliveryFailedUIAnimatior;
    private void Start()
    {
        OrderManager.Instance.OnRecipeSuccessed += OrderManager_OnRecipeSucceeded;
        OrderManager.Instance.OnRecipeFailed += OrderManager_OnRecipeFailed;
    }
    private void OrderManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        _deliveryFailedUIAnimatior.gameObject.SetActive(true);
        _deliveryFailedUIAnimatior.SetTrigger(IS_SHOW);
    }
    private void OrderManager_OnRecipeSucceeded(object sender, System.EventArgs e)
    {
        _deliverySuccessUIAnimatior.gameObject.SetActive(true);
        _deliverySuccessUIAnimatior.SetTrigger(IS_SHOW);
    }
}
