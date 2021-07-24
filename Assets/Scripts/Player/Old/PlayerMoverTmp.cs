using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMoverTmp : MonoBehaviour
{
    [SerializeField] private float _speed = 6.0f;
    [SerializeField] private float _gravity = -9.81f;

    private PlayerTmp _player;
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

    private void Start()
    {
        _player = new PlayerTmp(GetComponent<CharacterController>(), transform);   
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Update()
    {
        Vector2 temporaryDirection = _input.Player.Move.ReadValue<Vector2>();
        _moveDirection = new Vector3(temporaryDirection.x, _gravity, temporaryDirection.y);
        _player.Move(_speed, _moveDirection);
    }
}
