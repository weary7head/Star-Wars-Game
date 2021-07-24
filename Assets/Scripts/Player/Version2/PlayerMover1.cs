using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    class PlayerMover1 : MonoBehaviour
    {
        [SerializeField] private float _speed = 6.0f;
        [SerializeField] private float _gravity = -9.81f;

        private CharacterController _characterController;
        private Vector3 _moveDirection;
        private PlayerInput _input;

        private void Awake()
        {
            _input = new PlayerInput();
        }

        private void OnEnable()
        {
            _input.Enable();
        }

        private void Update()
        {
            Vector2 temporaryDirection = _input.Player.Move.ReadValue<Vector2>();
            _moveDirection = new Vector3(temporaryDirection.x, _gravity, temporaryDirection.y);
            Move(_speed, _moveDirection);
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        private void Move(float speed, Vector3 direction)
        {
            Vector3 movement = direction;
            movement = Vector3.ClampMagnitude(movement, speed);
            movement *= Time.deltaTime;
            movement = transform.TransformDirection(movement);
            _characterController.Move(movement);
        }
    }
}
