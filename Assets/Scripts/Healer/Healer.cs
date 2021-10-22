using Assets.Scripts.Player;
using UnityEngine;

public class Healer : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float _healCount = 50f;
    private Player _player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out _player))
        {
            _player.GetHealth(_healCount);
            Destroy(gameObject);
        }
    }
}
