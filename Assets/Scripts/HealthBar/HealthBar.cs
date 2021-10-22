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
        _player.HealthChanged += OnSetHealth;
    }

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnSetHealth;
    }

    private void OnSetHealth(int health)
    {
        _slider.value = health;
    }
}
