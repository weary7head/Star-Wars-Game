using UnityEngine;

namespace Assets.Scripts.Player
{
    class PlayerMover
    {
        private CharacterController _characterController;
        private Transform _transform;
        private PlayerInputProvider _inputProvider;
        private Vector3 _velocity;

        public PlayerMover(CharacterController characterController, Transform transform, PlayerInputProvider inputProvider)
        {
            _inputProvider = inputProvider;
            _characterController = characterController;
            _transform = transform;
            _velocity = Vector3.zero;
        }

        public void Move(float speed, float jumpHeight, float deltaTime)
        {
            if(_characterController.isGrounded)
            {
                _velocity.y = Physics.gravity.y;
            }

            Vector2 direction = _inputProvider.GetMovement();
            Vector3 movement = _transform.right * direction.x + _transform.forward * direction.y;
            movement = Vector3.ClampMagnitude(movement, speed);
            _characterController.Move(movement * speed * Time.deltaTime);

            if (_inputProvider.IsJumpPressed() && _characterController.isGrounded)
            {
                _velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y * deltaTime);
            }
            _velocity.y += Physics.gravity.y * deltaTime;
         
            _characterController.Move(_velocity);
        }
    }
}
