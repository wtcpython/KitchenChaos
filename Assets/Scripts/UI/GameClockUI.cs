using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class GameClockUI : MonoBehaviour
{
    [SerializeField] private GameObject _uiParent;
    [SerializeField] private Image _progressImage;
    [SerializeField] private TextMeshProUGUI _timeText;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        Hide();
    }

    private void Update()
    {
        if (GameManager.Instance.IsGamePlayingState())
        {
            _progressImage.fillAmount = GameManager.Instance.GetGamePlayingTimerNormalized();
            _timeText.text = Mathf.CeilToInt(GameManager.Instance.GetGamePlayingTimer()).ToString();
        }
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGamePlayingState())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        _uiParent.SetActive(true);
    }
    private void Hide()
    {
        _uiParent.SetActive(false);
    }
}
