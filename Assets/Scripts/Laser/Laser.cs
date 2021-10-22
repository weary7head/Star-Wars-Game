using System.Collections;
using Assets.Scripts.Player;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private float _damage = 10f;
    private Target _target;
    private Enemy _enemy;
    private Player _player;
    private Vector3 _direction;
    public Laser(Vector3 direction)
    {
        _direction = direction;
    }

    private void Start()
    {
        StartCoroutine(Destroy());
    }

    private void Update()
    {
        _direction *= _speed;
        _direction = Vector3.ClampMagnitude(_direction, _speed);
        transform.Translate(Time.deltaTime * _direction, Space.World);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out _target))
        {
            _target.GetDamage(_damage);
        }
        else if (other.gameObject.TryGetComponent(out _enemy))
        {
            _enemy.GetDamage(_damage);
        }
        else if (other.gameObject.TryGetComponent(out _player))
        {
            _player.GetDamage(_damage);
        }
        Destroy(this.gameObject);
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSecondsRealtime(5f);
        Destroy(this.gameObject);
    }
}
