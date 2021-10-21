using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour
{
    [SerializeField] private GameObject _laser;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private float _fireRate = 1.5f;
    [SerializeField] private Enemy _enemy;
    private float _nextTimeToFire = 0f;
    private AudioSource _fireAudioSource;

    private void OnEnable()
    {
        _enemy.Shooted += Shoot;
    }

    private void Awake()
    {
        _fireAudioSource = _spawnPosition.GetComponent<AudioSource>();
    }

    private void OnDisable()
    {
        _enemy.Shooted -= Shoot;
    }

    private void Shoot(Vector3 direction)
    {
        if (Time.time >= _nextTimeToFire)
        {
            _nextTimeToFire = Time.time + 1f / _fireRate;

            GameObject laserObject = Instantiate(_laser, _spawnPosition.position, transform.rotation);
            Laser laser = laserObject.GetComponent<Laser>();
            laser.SetDirection(direction - _spawnPosition.position);
            _fireAudioSource.Play();
        }
    }
}
