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
        if (_input.Player.Jump.ReadValue<float>() > 0)
        {
            return true;
        }
        return false;
    }

    public bool IsShootPressed()
    {
        if (_input.Player.Shoot.ReadValue<float>() > 0)
        {
            return true;
        }
        return false;
    }

    public bool IsPausePressed()
    {
        if (_input.Player.Pause.ReadValue<float>() > 0)
        {
            return true;
        }
        return false;
    }
}
