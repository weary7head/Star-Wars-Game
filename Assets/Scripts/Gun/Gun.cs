using UnityEngine;
using UnityEngine.PlayerLoop;

public class Gun : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float range = 100f;
    private PlayerInputProvider _inputProvider;

    private void Awake()
    {
        _inputProvider = new PlayerInputProvider();
    }
    private void Update()
    {
        if (_inputProvider.IsShootPressed())
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(_damage);
            }
            Debug.Log(hit.transform.name);
        }
    }
}
