using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private GameControl gameControl;

    private void Awake()
    {
        gameControl = new GameControl();
        gameControl.Player.Enable();
    }
    public Vector3 GetMovementDirectionNormalized()
    {
        Vector2 inputVector2 = gameControl.Player.Move.ReadValue<Vector2>();



        Vector3 movement = new Vector3(inputVector2.x, 0f, inputVector2.y);

        movement = movement.normalized;

        return movement;
    }
}