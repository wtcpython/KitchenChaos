using TMPro;

using UnityEngine;
public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject _uiParent;
    [SerializeField] private TextMeshProUGUI _numberText;
    // Start is calle
    // d once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Hide();
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOverState())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    // Update is called once per frame
    // void Update()
    // {

    // }
    private void Show()
    {
        _numberText.text = OrderManager.Instance.GetSuccessDeliveryCount().ToString();
        _uiParent.SetActive(true);
    }

    private void Hide()
    {
        _uiParent.SetActive(false);
    }
}
