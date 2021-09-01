using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float minimumVert = -45.0f;
    [SerializeField] private float maximumVert = 45.0f;
    [SerializeField] private float _horizontalSensitivity = 100f;
    [SerializeField] private float _verticalSensitivity = 1f;
    private PlayerInputProvider _inputProvider;
    private PlayerRotator _rotator;
    private float _rotationX = 0;

    private void Awake()
    {
        _inputProvider = new PlayerInputProvider();
        _rotator = new PlayerRotator(_player.transform, _inputProvider);
    }

    private void OnEnable()
    {
        _inputProvider.EnableInput();
    }

    private void LateUpdate()
    {
        _rotationX -= _inputProvider.GetMouseLook().y * _verticalSensitivity;
        _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

        float rotationY = transform.localEulerAngles.y;

        transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        _rotator.Rotate(_horizontalSensitivity);
    }

    private void OnDisable()
    {
        _inputProvider.DisableInput();
    }
}
