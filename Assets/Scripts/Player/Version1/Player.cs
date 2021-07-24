using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    class Player : MonoBehaviour
    {
        [SerializeField] private float _speed = 6.0f;
        [SerializeField] private float _gravity = -9.81f;

        private PlayerMover _playerMover;

        private void Awake()
        {
            _playerMover = new PlayerMover(GetComponent<CharacterController>(), transform);
        }

        private void OnEnable()
        {
            _playerMover.EnableInput();
        }

        private void Update()
        {
            _playerMover.Move(_speed, _gravity);
        }

        private void OnDisable()
        {
            _playerMover.DisableInput();    
        }
    }
}
