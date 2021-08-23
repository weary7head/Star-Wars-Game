using UnityEngine;

namespace Assets.Scripts.Player
{
    class PlayerMover
    {
        private CharacterController _characterController;
        private Transform _transform;
        private PlayerInputProvider _inputProvider;
        private GroundChecker _groundChecker;
        private Vector3 _velocity;

        public PlayerMover(CharacterController characterController, Transform transform, PlayerInputProvider inputProvider, GroundChecker groundChecker)
        {
            _inputProvider = inputProvider;
            _characterController = characterController;
            _transform = transform;
            _velocity = Vector3.zero;
            _groundChecker = groundChecker;
        }

        public void Move(float speed, float gravity, float jumpHeight)
        {
            if(_groundChecker.IsGrounded() && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }

            Vector2 direction = _inputProvider.GetMovement();
            Vector3 movement = _transform.right * direction.x + _transform.forward * direction.y;
            movement = Vector3.ClampMagnitude(movement, speed);
            _characterController.Move(movement * speed * Time.deltaTime);

            if (_inputProvider.IsJumpPressed() && _groundChecker.IsGrounded())
            {
                _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            _velocity.y += gravity * Time.deltaTime * Time.deltaTime;
            _characterController.Move(_velocity);
        }
    }
}
