using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private int _damage = 1;

    private void Update()
    {
        transform.Translate(0,0, _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
