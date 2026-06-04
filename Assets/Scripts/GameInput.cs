using System;

using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnOperateAction;

    private GameControl _gameControl;

    /// <summary>
    /// 懒加载属性：无论 Awake 是否已执行，首次访问时一定完成初始化。
    /// 消除了对 Unity 不同 GameObject 之间 Awake 执行顺序的依赖。
    /// </summary>
    private GameControl GameControl
    {
        get
        {
            if (_gameControl == null)
            {
                _gameControl = new GameControl();
                _gameControl.Player.Enable();
                _gameControl.Player.Interact.performed += Interact_Performed;
                _gameControl.Player.Operate.performed += Operate_Performed;
            }
            return _gameControl;
        }
    }

    private void Awake()
    {
        // 尽早触发初始化（如果此时还未被访问），确保 Input System 尽早就绪
        _ = GameControl;
    }

    private void OnDestroy()
    {
        if (_gameControl == null) return;

        _gameControl.Player.Interact.performed -= Interact_Performed;
        _gameControl.Player.Operate.performed -= Operate_Performed;
        _gameControl.Player.Disable();
        _gameControl.Dispose();
    }

    private void Operate_Performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        OnOperateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_Performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector3 GetMovementDirectionNormalized()
    {
        Vector2 inputVector2 = GameControl.Player.Move.ReadValue<Vector2>();

        Vector3 direction = new(inputVector2.x, 0f, inputVector2.y);

        direction = direction.normalized;

        return direction;
    }
}
