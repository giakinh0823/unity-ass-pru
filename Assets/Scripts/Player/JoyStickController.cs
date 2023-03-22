using Common;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoystickController : MonoBehaviour
{
    public  Vector2      JoystickValue => this.inputManager.JoystickDirection;
    private InputManager inputManager;

    private void Start()
    {
        this.inputManager = FindObjectOfType<InputManager>();
    }

    public float GetHorizontalValue()
    {
        return this.JoystickValue.x;
    }

    public float GetVerticalValue()
    {
        return this.JoystickValue.y;
    }

    public Vector3 GetDirection()
    {
        return new Vector3(this.JoystickValue.x, 0, this.JoystickValue.y).normalized;
    }
}