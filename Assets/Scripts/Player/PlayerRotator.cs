using UnityEngine;

public class PlayerRotator
{
    private Transform _transform;
    private PlayerInputProvider _inputProvider;
    public PlayerRotator(Transform transform, PlayerInputProvider inputProvider)
    {
        _transform = transform;
        _inputProvider = inputProvider;
    }

    public void Rotate(float sensitivity)
    {
        Vector2 playerLook = _inputProvider.GetMouseLook() * sensitivity;

        _transform.Rotate(Vector3.up * playerLook.x * Time.deltaTime);
    }
}
