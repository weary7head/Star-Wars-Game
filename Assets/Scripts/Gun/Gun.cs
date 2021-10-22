using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _laser;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private float _distance = 100f;
    [SerializeField] private float _fireRate = 1.5f;
    private PlayerInputProvider _inputProvider;
    private RayShooter _rayShooter;
    private float _nextTimeToFire = 0f;
    private AudioSource _fireAudioSource;

    private void OnEnable()
    {
        _inputProvider.EnableInput();
    }

    private void Awake()
    {
        _inputProvider = new PlayerInputProvider();
        _rayShooter = new RayShooter(_camera, _distance);
        _fireAudioSource = _spawnPosition.GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (_inputProvider.IsShootPressed() && Time.time >= _nextTimeToFire)
        {
            _nextTimeToFire = Time.time + 1f / _fireRate;
            Shoot();
            _fireAudioSource.Play();
        }
    }

    private void OnDisable()
    {
        _inputProvider.DisableInput();
    }

    private void Shoot()
    {
        Vector3 direction;
        Vector3 targetPosition = _rayShooter.GetTargetPoint();
        
        if (targetPosition != Vector3.zero)
        {
            direction = targetPosition - _spawnPosition.position;
        }
        else
        {
            direction = transform.forward;
        }

        GameObject laserObject = Instantiate(_laser, _spawnPosition.position, transform.rotation);
        Laser laser = laserObject.GetComponent<Laser>();
        laser.SetDirection(direction);
    }
}
