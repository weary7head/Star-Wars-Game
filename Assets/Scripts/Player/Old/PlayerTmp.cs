using UnityEngine;

public class PlayerTmp : IMoveable
{
    private CharacterController _characterController;
    private Transform _transform;
    public PlayerTmp(CharacterController characterController, Transform transform)
    {
        _characterController = characterController;
        _transform = transform;
    }

    public void Move(float speed, Vector3 direction)
    {
        Vector3 movement = direction;
        movement = Vector3.ClampMagnitude(movement, speed);
        movement *= Time.deltaTime;
        movement = _transform.TransformDirection(movement);
        _characterController.Move(movement);
    }
}