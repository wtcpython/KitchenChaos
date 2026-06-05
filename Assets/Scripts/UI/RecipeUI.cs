using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class RecipeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _recipleNameText;
    [SerializeField] private Transform _kitchenObjectParent;
    [SerializeField] private Image _iconUITemplate;

    private void Start()
    {
        _iconUITemplate.gameObject.SetActive(false);
    }

    public void UpdateUI(RecipeSO recipeSO)
    {
        _recipleNameText.text = recipeSO.RecipeName;
        foreach (KitchenObjectSO kitchenObjectSO in recipeSO.KitchenObjectSOList)
        {
            Image newIcon = Instantiate(_iconUITemplate);
            newIcon.transform.SetParent(_kitchenObjectParent, false);
            newIcon.sprite = kitchenObjectSO.Sprite;
            newIcon.gameObject.SetActive(true);
        }
    }
}
