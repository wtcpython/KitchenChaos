using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image _progressImage;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void UpdateProgress(float progress)
    {
        Show();
        _progressImage.fillAmount = progress;

        if (_progressImage.fillAmount >= 1f)
        {
            Invoke(nameof(Hide), 0.5f);
        }
    }
}
