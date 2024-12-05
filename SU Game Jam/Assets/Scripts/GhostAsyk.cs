using UnityEngine;

public class GhostAsyk : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    private bool _isGhost;
    public void Init(Vector3 velocity, bool isGhost)
    {
        _isGhost = isGhost;
        _rb.AddForce(velocity, ForceMode.Impulse);
    }
}
