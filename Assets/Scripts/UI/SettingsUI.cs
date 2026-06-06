using System;

using TMPro;

using UnityEngine;
using UnityEngine.UI;
public class SettingsUI : MonoBehaviour
{
    public static SettingsUI Instance { get; private set; }
    [SerializeField] private GameObject _uiParent;
    [SerializeField] private Button _soundButton;
    [SerializeField] private TextMeshProUGUI _soundButtonText;
    [SerializeField] private Button _musicButton;
    [SerializeField] private TextMeshProUGUI _musicButtonText;
    [SerializeField] private Button _closeButton;

    //上下左右按钮
    [SerializeField] private Button _upButton;
    [SerializeField] private Button _downButton;
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;
    [SerializeField] private TextMeshProUGUI _upButtonText;
    [SerializeField] private TextMeshProUGUI _downButtonText;
    [SerializeField] private TextMeshProUGUI _leftButtonText;
    [SerializeField] private TextMeshProUGUI _rightButtonText;

    //交互和操作按钮 和 文本
    [SerializeField] private Button _interactButton;
    [SerializeField] private Button _operateButton;
    [SerializeField] private TextMeshProUGUI _interactButtonText;
    [SerializeField] private TextMeshProUGUI _operateButtonText;

    //暂停按钮 和 文本
    [SerializeField] private Button _pauseButton;
    [SerializeField] private TextMeshProUGUI _pauseButtonText;

    [SerializeField] private GameObject _reBindingHint;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Hide();
        _soundButton.onClick.AddListener(() => SoundManager.Instance.ChangeVolume());
        _musicButton.onClick.AddListener(() => MusicManager.Instance.ChangeVolume());
        _closeButton.onClick.AddListener(() => Hide());
        //按键绑定按钮监听

        _upButton.onClick.AddListener(() => ReBinding(GameInput.BindingType.Up));
        _downButton.onClick.AddListener(() => ReBinding(GameInput.BindingType.Down));
        _leftButton.onClick.AddListener(() => ReBinding(GameInput.BindingType.Left));
        _rightButton.onClick.AddListener(() => ReBinding(GameInput.BindingType.Right));
        _interactButton.onClick.AddListener(() => ReBinding(GameInput.BindingType.Interact));
        _operateButton.onClick.AddListener(() => ReBinding(GameInput.BindingType.Operate));
        _pauseButton.onClick.AddListener(() => GameInput.Instance.ReBinding(GameInput.BindingType.Pause));
    }

    public void Show()
    {
        _uiParent.SetActive(true);
    }

    private void Hide()
    {
        _uiParent.SetActive(false);
    }

    void Update()
    {
        _soundButtonText.text = "音效大小: " + SoundManager.Instance.GetVolume();
        _musicButtonText.text = "音乐大小: " + MusicManager.Instance.GetVolume();
        //更新按键绑定显示
        _upButtonText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Up);
        _downButtonText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Down);
        _leftButtonText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Left);
        _rightButtonText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Right);
        _interactButtonText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Interact);
        _operateButtonText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Operate);
        _pauseButtonText.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Pause);
    }

    private void ReBinding(GameInput.BindingType bindingType)
    {
        _reBindingHint.SetActive(true);
        GameInput.Instance.ReBinding(bindingType, () => { Update(); _reBindingHint.SetActive(false); });
    }
}
