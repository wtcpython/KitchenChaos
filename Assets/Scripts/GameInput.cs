using System;

using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    private const string GAME_INPUT_BINDINGS = "GameInputBindings";
    public event EventHandler OnInteractAction;
    public event EventHandler OnOperateAction;

    public event EventHandler OnPauseAction;

    private GameControl _gameControl;

    public enum BindingType
    {
        Up,
        Down,
        Left,
        Right,
        Interact,
        Operate,
        Pause
    }

    private void Awake()
    {
        Instance = this;
        _gameControl = new GameControl();
        if (PlayerPrefs.HasKey(GAME_INPUT_BINDINGS))
        {
            _gameControl.LoadBindingOverridesFromJson(PlayerPrefs.GetString(GAME_INPUT_BINDINGS));
        }
        _gameControl.Player.Enable();

        _gameControl.Player.Interact.performed += Interact_Performed;
        _gameControl.Player.Operate.performed += Operate_Performed;
        _gameControl.Player.Pause.performed += Pause_Performed;
    }

    public void ReBinding(BindingType bindingType, Action onComplete = null)
    {
        _gameControl.Player.Disable();
        InputAction inputAction = bindingType switch
        {
            BindingType.Up => _gameControl.Player.Move,
            BindingType.Down => _gameControl.Player.Move,
            BindingType.Left => _gameControl.Player.Move,
            BindingType.Right => _gameControl.Player.Move,
            BindingType.Interact => _gameControl.Player.Interact,
            BindingType.Operate => _gameControl.Player.Operate,
            BindingType.Pause => _gameControl.Player.Pause,
            _ => null
        };
        int index = bindingType switch
        {
            BindingType.Up => 1,
            BindingType.Down => 2,
            BindingType.Left => 3,
            BindingType.Right => 4,
            BindingType.Interact => 0,
            BindingType.Operate => 0,
            BindingType.Pause => 0,
            _ => -1
        };
        Debug.Log($"ReBinding: {bindingType}, index: {index}");
        if (inputAction == null) return;
        _ = inputAction.PerformInteractiveRebinding(index).OnComplete(operation =>
        {
            operation.Dispose();
            _gameControl.Player.Enable();
            onComplete?.Invoke();
            PlayerPrefs.SetString(GAME_INPUT_BINDINGS, _gameControl.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();
        }).Start();
    }
    public string GetBindingDisplayString(BindingType bindingType)
    {
        return bindingType switch
        {
            BindingType.Up => _gameControl.Player.Move.bindings[1].ToDisplayString(),
            BindingType.Down => _gameControl.Player.Move.bindings[2].ToDisplayString(),
            BindingType.Left => _gameControl.Player.Move.bindings[3].ToDisplayString(),
            BindingType.Right => _gameControl.Player.Move.bindings[4].ToDisplayString(),
            BindingType.Interact => _gameControl.Player.Interact.bindings[0].ToDisplayString(),
            BindingType.Operate => _gameControl.Player.Operate.bindings[0].ToDisplayString(),
            BindingType.Pause => _gameControl.Player.Pause.bindings[0].ToDisplayString(),
            _ => string.Empty,
        };
    }

    private void OnDestroy()
    {
        // if (_gameControl == null) return;

        _gameControl.Player.Interact.performed -= Interact_Performed;
        _gameControl.Player.Operate.performed -= Operate_Performed;
        _gameControl.Player.Pause.performed -= Pause_Performed;
        // _gameControl.Player.Disable();
        _gameControl.Dispose();
    }

    // 事件触发方法
    private void Pause_Performed(InputAction.CallbackContext context)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void Operate_Performed(InputAction.CallbackContext context)
    {
        OnOperateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_Performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }
    public Vector3 GetMovementDirectionNormalized()
    {
        Vector2 inputVector2 = _gameControl.Player.Move.ReadValue<Vector2>();

        Vector3 direction = new Vector3(inputVector2.x, 0f, inputVector2.y);

        direction = direction.normalized;

        return direction;
    }
}
