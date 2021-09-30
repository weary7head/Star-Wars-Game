using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private float _inspectionRadius = 0.4f;
    [SerializeField] private LayerMask _groundMask;
    private bool _isGrounded;

    public bool IsGrounded()
    {
        _isGrounded = Physics.CheckSphere(transform.position, _inspectionRadius, _groundMask);
        Debug.Log(_isGrounded);
        return _isGrounded;
    }
}
