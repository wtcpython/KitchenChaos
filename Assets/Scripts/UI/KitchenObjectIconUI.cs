using UnityEngine;
using UnityEngine.UI;

public class KitchenObjectIconUI : MonoBehaviour
{
    [SerializeField] private Image _iconImage;

    public void Show(Sprite sprite)
    {
        gameObject.SetActive(true);
        _iconImage.sprite = sprite;
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
