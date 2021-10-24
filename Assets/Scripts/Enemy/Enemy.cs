using Assets.Scripts.Player;
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;
using Random = UnityEngine.Random;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator), typeof(AudioSource))]
public class Enemy : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float _health = 100f;
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private Transform[] _positions;
    [SerializeField] private float _attackRange = 10;
    [SerializeField] private float _sightRange = 20;
    [SerializeField] private Transform _raysCaster;
    [SerializeField] private Rig _rigLayerBlaster;
    [SerializeField] private Rig _rigLayerFire;
    [SerializeField] private Rig _rigLayerDied;

    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private float _timer = 0;
    private int _waitingTime = 5;
    private bool _isTargetInSightRange;
    private bool _isTargetInAttackRange;
    private Transform _transform;
    private float _offsetY = 3;
    private bool _isAlive = true;
    private AudioSource _deathAudioSource;

    public event Action DeadCountChanged;
    public event Action<Vector3> Shooted;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _transform = transform;
        _deathAudioSource = GetComponent<AudioSource>();
    }
    
    private void Update()
    {
        if (_isAlive)
        {
            _isTargetInSightRange = Physics.CheckSphere(_transform.position, _sightRange, _playerMask);
            _isTargetInAttackRange = Physics.CheckSphere(_transform.position, _attackRange, _playerMask);

            if (_isTargetInSightRange == false && _isTargetInAttackRange == false)
            {
                if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                {
                    WaitForPosition();
                }
            }
            if (_isTargetInSightRange == true && _isTargetInAttackRange == false)
            {
                FindEnemy(State.WalkingFire, false);
            }
            if (_isTargetInSightRange == true && _isTargetInAttackRange == true)
            {
                _navMeshAgent.SetDestination(_transform.position);
                FindEnemy(State.Fire, true);
            } 
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
                _rigLayerFire.weight = 0f;
                _rigLayerBlaster.weight = 1f;
                break;
            case State.Walk:
                _animator.SetFloat("FiringSpeed", -1f);
                _animator.SetFloat("Speed", 0.5f);
                _rigLayerFire.weight = 0f;
                _rigLayerBlaster.weight = 1f;
                break;
            case State.Run:
                _animator.SetFloat("FiringSpeed", -1f);
                _animator.SetFloat("Speed", 1f);
                _rigLayerFire.weight = 0f;
                _rigLayerBlaster.weight = 0f;
                break;
            case State.Fire:
                _animator.SetFloat("Speed", -1f);
                _animator.SetFloat("FiringSpeed", 0.5f);
                _rigLayerFire.weight = 1f;
                _rigLayerBlaster.weight = 0f;
                break;
            case State.WalkingFire:
                _animator.SetFloat("Speed", -1f);
                _animator.SetFloat("FiringSpeed", 1f);
                _rigLayerFire.weight = 1f;
                _rigLayerBlaster.weight = 0f;
                break;
            case State.Die:
                _animator.SetFloat("FiringSpeed", -1f);
                _animator.SetFloat("Speed", -1f);
                _animator.SetFloat("Died", 1f);
                _rigLayerFire.weight = 0f;
                _rigLayerBlaster.weight = 0f;
                _rigLayerDied.weight = 1f;
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

    private void FindEnemy(State state, bool isLookAt)
    {
        Collider[] hitColliders = Physics.OverlapSphere(_raysCaster.position, _sightRange, _playerMask);

        if (hitColliders.Length != 0)
        {
            foreach (Collider hitCollider in hitColliders)
            {
                Player player = hitCollider.GetComponent<Player>();
                Vector3 playerPosition = player.transform.position;
                playerPosition.y += _offsetY;

                if (isLookAt)
                {
                    _transform.LookAt(playerPosition);
                }
                else
                {
                    _navMeshAgent.SetDestination(playerPosition);
                }

                Vector3 target = playerPosition - _raysCaster.transform.position;
                Debug.DrawRay(_raysCaster.position, target, Color.green);
                if (Physics.Raycast(_raysCaster.position, target, _sightRange, _playerMask))
                {
                    
                    Shooted?.Invoke(playerPosition);
                }

                SetState(state);
            }
        }
        else
        {
            _navMeshAgent.SetDestination(_transform.position);
            SetState(State.Idle);
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

    public void GetDamage(float count)
    {
        if (_health > 0)
        {
            _health -= count;
            if (_health <= 0)
            {
                Die();
                Destroy(gameObject, 3);
            }
        }
    }

    private void Die()
    {
        SetState(State.Die);
        _isAlive = false;
        _deathAudioSource.Play();
        DeadCountChanged?.Invoke();
    }
}
