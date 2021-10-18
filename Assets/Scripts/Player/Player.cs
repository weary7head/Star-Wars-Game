using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(CharacterController))]
    class Player : MonoBehaviour
    {
        [Header("Steps Audio Source")]
        [SerializeField] private AudioSource _audioSource;
        [Header("Movement")]
        [SerializeField] private float _speed = 6.0f;
        [SerializeField] private float _jumpHeight = 3f;
        [Header("Camera")]
        [SerializeField] private float _minimumVertical = -45.0f;
        [SerializeField] private float _maximumVertical = 45.0f;
        [SerializeField] private float _horizontalSensitivity = 1f;
        [SerializeField] private float _verticalSensitivity = 1f;
        [Header("Stats")]
        [SerializeField, Range(0f, 100f)] private float _health = 100f;
        [SerializeField] private PauseMenu _pause;

        private PlayerInputProvider _inputProvider;
        private PlayerMover _mover;
        private PlayerRotator _rotator;
        private Transform _transform;

        public event Action<int> HealthChanged;
        public event Action PausePressed;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _inputProvider = new PlayerInputProvider();
            _mover = new PlayerMover(GetComponent<CharacterController>(), _transform, _inputProvider);
            _rotator = new PlayerRotator(_transform, _inputProvider);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnEnable()
        {
            _inputProvider.EnableInput();
            _pause.HorizontalSensitivityChanged += SetHorizontalSensitivity;
            _pause.VerticalSensitivityChanged += SetVerticalSensitivity;
        }

        private void Start()
        {
            HealthChanged?.Invoke(Convert.ToInt32(_health));
        }

        private void Update()
        {
            _mover.Move(_speed, _jumpHeight, Time.deltaTime);
            if (transform.hasChanged)
            {
                _audioSource.Play();
            }
            _rotator.Rotate(_minimumVertical, _maximumVertical, _horizontalSensitivity, _verticalSensitivity);

            if (_inputProvider.IsPausePressed())
            {
                PausePressed?.Invoke();
            }
        }

        private void OnDisable()
        {
            _inputProvider.DisableInput();
            _pause.HorizontalSensitivityChanged -= SetHorizontalSensitivity;
            _pause.VerticalSensitivityChanged -= SetVerticalSensitivity;
        }

        public void GetHealth(float count)
        {
            _health += count;
            if (_health > 100)
            {
                _health = 100;
            }
            HealthChanged?.Invoke(Convert.ToInt32(_health));
        }

        public void GetDamage(float damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Die();
            }
            HealthChanged?.Invoke(Convert.ToInt32(_health));
        }

        public void Die()
        {
            //TODO
        }

        private void SetVerticalSensitivity(float value)
        {
            _verticalSensitivity = value;
        }

        private void SetHorizontalSensitivity(float value)
        {
            _horizontalSensitivity = value;
        }

        public float GetVerticalSensitivity()
        {
            return _verticalSensitivity;
        }

        public float GetHorizontalSensitivity()
        {
            return _horizontalSensitivity;
        }
    }
}
