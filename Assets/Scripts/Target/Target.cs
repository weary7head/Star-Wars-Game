using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float _health = 100f;
    public event Action OnDead;

    public void GetDamage(float count)
    {
        _health -= count;
        if (_health <= 0)
        {
            Die();
            Destroy(gameObject);
        }
    }

    private void Die()
    {
        OnDead?.Invoke();
    }
}
