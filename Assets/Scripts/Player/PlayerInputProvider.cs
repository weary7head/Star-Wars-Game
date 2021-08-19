using UnityEngine;

public class PlayerInputProvider
{
    private PlayerInput _input;

    public PlayerInputProvider()
    {
        _input = new PlayerInput();
    }

    public void EnableInput()
    {
        _input.Enable();
    }

    public void DisableInput()
    {
        _input.Disable();
    }

    public Vector2 GetMovement()
    {
        return _input.Player.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetMouseLook()
    {
        return _input.Player.Look.ReadValue<Vector2>();
    }

    public bool IsJumpPressed()
    {
        return _input.Player.Jump.ReadValue<bool>();
    }

    public bool IsShootPressed()
    {
        return _input.Player.Shoot.ReadValue<bool>();
    }
}
