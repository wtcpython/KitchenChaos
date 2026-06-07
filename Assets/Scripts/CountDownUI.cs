using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class CountDownUI : MonoBehaviour
{
    private const string IS_SHAKE = "IsShake";
    [SerializeField] private TextMeshProUGUI _numberText;
    private Animator _anim;
    private int _preNumber = -1;
    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        _anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (GameManager.Instance.IsCountDownState())
        {
            int nowNumber = Mathf.CeilToInt(GameManager.Instance.GetCountDownTimer());
            _numberText.text = nowNumber.ToString();
            if (_preNumber != nowNumber)
            {
                _preNumber = nowNumber;
                _anim.SetTrigger(IS_SHAKE);
                SoundManager.Instance.PlayCountDownSound();
            }
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
