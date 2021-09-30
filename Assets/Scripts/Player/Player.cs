﻿using System;
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
        private PlayerInputProvider _inputProvider;
        private PlayerMover _mover;
        private PlayerRotator _rotator;
        private Transform _transform;

        public event Action<int> OnHealthChanged;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _inputProvider = new PlayerInputProvider();
            _mover = new PlayerMover(GetComponent<CharacterController>(), _transform, _inputProvider);
            _rotator = new PlayerRotator(_transform, _inputProvider);
        }

        private void OnEnable()
        {
            _inputProvider.EnableInput();
        }

        private void Start()
        {
            OnHealthChanged?.Invoke(Convert.ToInt32(_health));
        }

        private void Update()
        {
            _mover.Move(_speed, _jumpHeight, Time.deltaTime);
            if (transform.hasChanged)
            {
                _audioSource.Play();
            }
            _rotator.Rotate(_minimumVertical, _maximumVertical, _horizontalSensitivity, _verticalSensitivity);
        }

        private void OnDisable()
        {
            _inputProvider.DisableInput();    
        }

        public void GetHealth(float count)
        {
            _health += count;
            if (_health > 100)
            {
                _health = 100;
            }
            OnHealthChanged?.Invoke(Convert.ToInt32(_health));
        }

        public void GetDamage(float damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Die();
            }
            OnHealthChanged?.Invoke(Convert.ToInt32(_health));
        }

        public void Die()
        {

        }
    }
}
