using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    class PlayerMover
    {
        private PlayerInput _input;
        private CharacterController _characterController;
        private Transform _transform;

        public PlayerMover(CharacterController characterController, Transform transform)
        {
            _input = new PlayerInput();
            _characterController = characterController;
            _transform = transform;
        }

        public void EnableInput()
        {
            _input.Enable();
        }

        public void DisableInput()
        {
            _input.Disable();
        }
        public void Move(float speed, float gravity)
        {
            Vector2 temporaryDirection = _input.Player.Move.ReadValue<Vector2>();
            Vector3 movement = new Vector3(temporaryDirection.x, gravity, temporaryDirection.y);
            movement = Vector3.ClampMagnitude(movement, speed);
            movement *= Time.deltaTime;
            movement = _transform.TransformDirection(movement);
            
            _characterController.Move(movement);
        }
    }
}
