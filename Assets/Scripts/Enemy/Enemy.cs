using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private Transform[] _positions;
    [SerializeField] private float _attackRange = 10;
    [SerializeField] private float _sightRange = 20;
    [SerializeField] private Transform _raysCaster;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private float _timer = 0;
    private int _waitingTime = 5;
    private bool _isTargetInSightRange;
    private bool _isTargetInAttackRange;
    private Transform _transform;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _transform = transform;
    }
    
    private void Update()
    {
        _isTargetInSightRange = Physics.CheckSphere(_transform.position, _sightRange, _playerMask);
        _isTargetInAttackRange = Physics.CheckSphere(_transform.position, _attackRange, _playerMask);
       
        //Debug.DrawRay(_raysCaster.position, _raysCaster.transform.forward * _sightRange, Color.red);
        Debug.DrawRay(_raysCaster.position, (_raysCaster.transform.forward + _raysCaster.transform.right) * _sightRange, Color.green);
        Debug.DrawRay(_raysCaster.position, (_raysCaster.transform.forward - _raysCaster.transform.right) * _sightRange, Color.blue);
        Debug.DrawRay(_raysCaster.position, (_raysCaster.transform.forward - _raysCaster.transform.up) * _sightRange, Color.magenta);
        Debug.DrawRay(_raysCaster.position, (_raysCaster.transform.forward + _raysCaster.transform.up) * _sightRange, Color.yellow);
        Debug.DrawRay(_raysCaster.position, ((_raysCaster.transform.forward - _raysCaster.transform.right) + _raysCaster.transform.up) * _sightRange, Color.white);
        Debug.DrawRay(_raysCaster.position, ((_raysCaster.transform.forward + _raysCaster.transform.right) + _raysCaster.transform.up) * _sightRange, Color.cyan);

        if (_isTargetInSightRange == false && _isTargetInAttackRange == false)
        {
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                WaitForPosition();
            }
        }
        if (_isTargetInSightRange == true && _isTargetInAttackRange == false)
        {
            Collider[] hitColliders = Physics.OverlapSphere(_raysCaster.position, _sightRange, _playerMask);
          
            if (hitColliders.Length != 0)
            {
                foreach (Collider hitCollider in hitColliders)
                {
                    Player player = hitCollider.GetComponent<Player>();
                    Vector3 playerPosition = player.transform.position;
                    Debug.DrawRay(_raysCaster.position, playerPosition - _raysCaster.transform.position, Color.red);
                    _navMeshAgent.SetDestination(playerPosition);
                    SetState(State.WalkingFire);
                }
            }
            else
            {
                _navMeshAgent.SetDestination(_transform.position);
                SetState(State.Idle);
            }

           


            //Vector3 playerPosition = FindEnemy();

            //if (playerPosition != Vector3.zero)
            //{
            //    _navMeshAgent.SetDestination(playerPosition);
            //    SetState(State.WalkingFire);
            //}
        }
        else if (_isTargetInSightRange == true && _isTargetInAttackRange == true)
        {
            _navMeshAgent.SetDestination(_transform.position);
            Collider[] hitColliders = Physics.OverlapSphere(_raysCaster.position, _sightRange, _playerMask);
            
            if (hitColliders.Length != 0)
            {
                foreach (Collider hitCollider in hitColliders)
                {
                    Player player = hitCollider.GetComponent<Player>();
                    Vector3 playerPosition = player.transform.position;
                    Debug.DrawRay(_raysCaster.position, playerPosition - _raysCaster.transform.position, Color.green);
                    _transform.LookAt(playerPosition);
                    SetState(State.Fire);
                }
            }
            else
            {
                _navMeshAgent.SetDestination(_transform.position);
                SetState(State.Idle);
            }



            //Vector3 playerPosition = FindEnemy();

            //if (playerPosition != Vector3.zero)
            //{
            //    _navMeshAgent.SetDestination(_transform.position);
            //    _transform.LookAt(playerPosition);
            //    SetState(State.Fire);
            //}
        }
    }

    private Vector3 ChangePosition()
    {
        Random.InitState(DateTime.Now.Millisecond);
        return _positions[Random.Range(0, _positions.Length)].position;
    }

    private void SetState(Enum state)
    {
        switch (state)
        {
            case State.Idle:
                _animator.SetFloat("FiringSpeed", -1f);
                _animator.SetFloat("Speed", -1f);
                break;
            case State.Walk:
                _animator.SetFloat("Speed", 0.5f);
                break;
            case State.Run:
                _animator.SetFloat("Speed", 1f);
                break;
            case State.Fire:
                _animator.SetFloat("FiringSpeed", 0.5f);
                break;
            case State.WalkingFire:
                _animator.SetFloat("FiringSpeed", 1f);
                break;
            case State.Die:
                break;
        }
    }

    private void WaitForPosition()
    {
        SetState(State.Idle);
        _timer += Time.deltaTime;
        if (_timer > _waitingTime)
        {
            SetState(State.Walk);
            _navMeshAgent.SetDestination(ChangePosition());
            _timer = 0;
        }
    }

    private enum State
    {
        Idle,
        Walk,
        Run,
        Fire,
        WalkingFire,
        Die
    }

}
