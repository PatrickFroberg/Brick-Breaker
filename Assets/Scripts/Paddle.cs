using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Paddle : MonoBehaviour
{
    [SerializeField] private float _speed;

    private float _speedMultiplier;
    private float _maxMovement;
    private Vector2 _moveInput;
    
    void Start()
    {
        _speed = 4.0f;
        _speedMultiplier = 1.10f;
        _maxMovement = 2.0f;
    }

    public void Move(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();

        Debug.Log($"Move Input: {_moveInput}");
    }

    void Update()
    {
        //float input = Input.GetAxisRaw("Horizontal");

        Vector3 pos = transform.position;
        //pos.x += input * _speed * Time.deltaTime;
        pos.x += _moveInput.x * _speed * Time.deltaTime;


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
