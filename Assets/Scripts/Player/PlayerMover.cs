using UnityEngine;

namespace Assets.Scripts.Player
{
    class PlayerMover
    {
        
        private CharacterController _characterController;
        private Transform _transform;
        private PlayerInputProvider _inputProvider;

        public PlayerMover(CharacterController characterController, Transform transform, PlayerInputProvider inputProvider)
        {
            _inputProvider = inputProvider;
            _characterController = characterController;
            _transform = transform;
        }

        public void Move(float speed, float gravity)
        {
            Vector2 temporaryDirection = _inputProvider.GetMovement();
            Vector3 movement = new Vector3(temporaryDirection.x, gravity, temporaryDirection.y);
            movement = Vector3.ClampMagnitude(movement, speed);
            movement *= Time.deltaTime;
            movement = _transform.TransformDirection(movement);
            
            _characterController.Move(movement);
        }
    }
}
