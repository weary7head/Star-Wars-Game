using System;
using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private int _damage = 1;
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

    private void OnTriggerEnter(Collider other)
    {
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
