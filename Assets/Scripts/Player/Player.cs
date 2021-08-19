using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(CharacterController))]
    class Player : MonoBehaviour
    {
        [SerializeField] private float _speed = 6.0f;
        [SerializeField] private float _gravity = -9.81f;
        private PlayerInputProvider _inputProvider;
        private PlayerMover _mover;

        private void Awake()
        {
            _inputProvider = new PlayerInputProvider();
            _mover = new PlayerMover(GetComponent<CharacterController>(), transform, _inputProvider);
        }

        private void OnEnable()
        {
            _inputProvider.EnableInput();
        }

        private void Update()
        {
            _mover.Move(_speed, _gravity);
        }

        private void OnDisable()
        {
            _inputProvider.DisableInput();    
        }
    }
}
