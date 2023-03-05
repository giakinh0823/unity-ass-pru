using UnityEngine;
using UnityEngine.InputSystem;

public class JoystickController : MonoBehaviour
{
    public Vector2 joystickValue;
    private MyPlayerActions playerControls;

    private void Awake()
    {
        playerControls = new MyPlayerActions();
        playerControls.Enable();
        playerControls.Player.Move.performed += OnJoystickPerformed;
        playerControls.Player.Move.canceled += OnJoystickCanceled;
    }

    private void OnDestroy()
    {
        playerControls.Disable();
    }

    private void OnJoystickPerformed(InputAction.CallbackContext ctx)
    {
        joystickValue = ctx.ReadValue<Vector2>();
    }

    private void OnJoystickCanceled(InputAction.CallbackContext ctx)
    {
        joystickValue = Vector2.zero;
    }

    public float GetHorizontalValue()
    {
        return joystickValue.x;
    }

    public float GetVerticalValue()
    {
        return joystickValue.y;
    }

    public Vector3 GetDirection()
    {
        return new Vector3(joystickValue.x, 0, joystickValue.y).normalized;
    }
}