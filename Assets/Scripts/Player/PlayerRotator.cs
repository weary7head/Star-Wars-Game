using UnityEngine;

public class PlayerRotator
{
    private Transform _cameraTransform;
    private Transform _bodyTransform;
    private PlayerInputProvider _inputProvider;
    private float _rotationX = 0;
    public PlayerRotator(Transform bodyTransform, Transform cameraTransform, PlayerInputProvider inputProvider)
    {
        _bodyTransform = bodyTransform;
        _cameraTransform = cameraTransform;
        _inputProvider = inputProvider;
    }

    public void Rotate(float minimumVertical, float maximumVertical, float horizontalSensitivity, float verticalSensitivity)
    {
        _rotationX -= _inputProvider.GetMouseLook().y * verticalSensitivity;
        _rotationX = Mathf.Clamp(_rotationX, minimumVertical, maximumVertical);
        float delta = _inputProvider.GetMouseLook().x * horizontalSensitivity;
        float rotationY = _bodyTransform.localEulerAngles.y + delta;
        _bodyTransform.localEulerAngles = new Vector3(0, rotationY);
        _cameraTransform.localEulerAngles = new Vector3(_rotationX, 0);
    }
}
