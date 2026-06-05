using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class CountDownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _numberText;
    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }
    private void Update()
    {
        if (GameManager.Instance.IsCountDownState())
        {

            _numberText.text = Mathf.CeilToInt(GameManager.Instance.GetCountDownTimer()).ToString();
        }
    }
    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountDownState())
        {
            _numberText.gameObject.SetActive(true);
        }
        else
        {
            _numberText.gameObject.SetActive(false);
        }
    }
}
