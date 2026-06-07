using TMPro;

using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private GameObject _uiParent;
    [SerializeField] private TextMeshProUGUI _upKeytext;
    [SerializeField] private TextMeshProUGUI _downKeytext;
    [SerializeField] private TextMeshProUGUI _leftKeytext;
    [SerializeField] private TextMeshProUGUI _rightKeytext;
    [SerializeField] private TextMeshProUGUI _interactKeytext;
    [SerializeField] private TextMeshProUGUI _operatetext;
    [SerializeField] private TextMeshProUGUI _pauseKeytext;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        Show();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsWaitingToStartState())
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
        UpdateVisual();
        _uiParent.SetActive(true);
    }
    private void Hide()
    {
        _uiParent.SetActive(false);
    }
    private void UpdateVisual()
    {
        _upKeytext.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Up);
        _downKeytext.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Down);
        _leftKeytext.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Left);
        _rightKeytext.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Right);
        _interactKeytext.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Interact);
        _operatetext.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Operate);
        _pauseKeytext.text = GameInput.Instance.GetBindingDisplayString(GameInput.BindingType.Pause);
    }
}
