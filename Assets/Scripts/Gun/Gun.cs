using UnityEngine;
using UnityEngine.PlayerLoop;

public class Gun : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _laser;
    [SerializeField] private Transform _spawnPosition;
    private PlayerInputProvider _inputProvider;

    private void OnEnable()
    {
        _inputProvider.EnableInput();
    }

    private void Awake()
    {
        _inputProvider = new PlayerInputProvider();
    }
    private void Update()
    {
        if (_inputProvider.IsShootPressed())
        {
            Shoot();
        }
    }

    private void OnDisable()
    {
        _inputProvider.DisableInput();
    }

    private void Shoot()
    {

            Instantiate(_laser, _spawnPosition.position, transform.rotation);

    }
}
