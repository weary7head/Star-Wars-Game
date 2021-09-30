using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Player _player;
    private Slider _slider;
    

    private void OnEnable()
    {
        _player.OnHealthChanged += SetHealth;
    }

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnDisable()
    {
        _player.OnHealthChanged -= SetHealth;
    }

    private void SetHealth(int health)
    {
        _slider.value = health;
    }
}
