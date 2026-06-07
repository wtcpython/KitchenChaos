using UnityEngine;

public class WarningControl : MonoBehaviour
{
    private const string IS_FLICKER = "IsFlicker";
    [SerializeField] private GameObject _warningUI;
    [SerializeField] private Animator _progressBarAnimator;

    private bool _isWarning = false;

    private float _warningSoundRate = .2f;
    private float _warningSoundTimer = 0f;

    private void Update()
    {
        if (_isWarning)
        {
            _warningSoundTimer += Time.deltaTime;
            if (_warningSoundTimer > _warningSoundRate)
            {
                _warningSoundTimer = 0f;
                SoundManager.Instance.PlayWarningSound();
            }
        }
    }
    public void ShowWarning()
    {
        if (_isWarning == false)
        {
            _isWarning = true;
            _warningUI.SetActive(true);
            _progressBarAnimator.SetBool(IS_FLICKER, true);
        }
    }

    public void StopWarning()
    {
        _isWarning = false;
        _warningUI.SetActive(false);
        _progressBarAnimator.SetBool(IS_FLICKER, false);
    }
}
