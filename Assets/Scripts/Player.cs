using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameInput gameInput;
    private float moveSpeed = 7f;
    private float turnspeed = 10f;

    private bool isWalking = false;

    private void FixedUpdate()
    {
        Vector3 movement = gameInput.GetMovementDirectionNormalized();

        isWalking = movement != Vector3.zero;

        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);

        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnspeed * Time.deltaTime);
        }
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}