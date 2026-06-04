using UnityEngine;

public class KitchenObjectGridUI : MonoBehaviour
{
    [SerializeField] private KitchenObjectIconUI _iconTemplateUI;

    private void Start()
    {
        _iconTemplateUI.Hide();
    }

    public void ShowKitchenObject(KitchenObjectSO kitchenObjectSO)
    {
        KitchenObjectIconUI newIconUI = GameObject.Instantiate(_iconTemplateUI, transform);
        newIconUI.Show(kitchenObjectSO.Sprite);
    }
}
