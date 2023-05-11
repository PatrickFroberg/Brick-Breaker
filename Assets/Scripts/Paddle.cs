using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] private float _speed;

    private float _speedMultiplier;
    private float _maxMovement;
    
    void Start()
    {
        _speed = 4.0f;
        _speedMultiplier = 1.10f;
        _maxMovement = 2.0f;
    }

    void Update()
    {
        float input = Input.GetAxis("Horizontal");

        Vector3 pos = transform.position;
        pos.x += input * _speed * Time.deltaTime;

        if (pos.x > _maxMovement)
            pos.x = _maxMovement;
        else if (pos.x < -_maxMovement)
            pos.x = -_maxMovement;

        transform.position = pos;
    }

    public void IncreaseMaxSpeed()
    {
        _speed *= _speedMultiplier;
    }
}
