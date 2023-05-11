using Assets.Scripts.Managers;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Vector3 _velocity;

    private float _maxSpeed;
    private float _speedMultiplier;

    private Rigidbody _rb;

    void Start()
    {
        _maxSpeed = 3.0f;
        _speedMultiplier = 1.1f;

        _rb = GetComponent<Rigidbody>();
    }

    public void IncreaseMaxSpeed()
    {
        _maxSpeed *= _speedMultiplier;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "DeathZone")
            AudioManager.Instance.PlayBounceSound();
    }

    private void OnCollisionExit(Collision other)
    {
        _velocity = _rb.velocity;

        //after a collision we accelerate a bit
        _velocity += _velocity.normalized * 0.01f;
        
        //check if we are not going totally vertically as this would lead to being stuck, we add a little vertical force
        if (Vector3.Dot(_velocity.normalized, Vector3.up) < 0.1f)
        {
            _velocity += _velocity.y > 0 ? Vector3.up * 0.5f : Vector3.down * 0.5f;
        }

        //max velocity
        if (_velocity.magnitude > _maxSpeed)
        {
            _velocity = _velocity.normalized * _maxSpeed;
        }

        _rb.velocity = _velocity;
    }
}
