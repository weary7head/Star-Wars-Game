using UnityEngine;

public class PlayerRotator
{
    private Transform _transform;
    private PlayerInputProvider _inputProvider;
    private float _rotationX = 0;
    public PlayerRotator(Transform transform, PlayerInputProvider inputProvider)
    {
        _transform = transform;
        _inputProvider = inputProvider;
    }

    public void Rotate(float minimumVertical, float maximumVertical, float horizontalSensitivity, float verticalSensitivity)
    {
        _rotationX -= _inputProvider.GetMouseLook().y * verticalSensitivity;
        _rotationX = Mathf.Clamp(_rotationX, minimumVertical, maximumVertical);
        float delta = _inputProvider.GetMouseLook().x * horizontalSensitivity;
        float rotationY = _transform.localEulerAngles.y + delta;
        _transform.localEulerAngles = new Vector3(_rotationX, rotationY);
    }
}
