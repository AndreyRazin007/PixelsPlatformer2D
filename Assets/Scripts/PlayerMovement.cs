using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D _rigidbody;
    public Transform _groundCheck;
    public LayerMask _groundLayer;

    private float _horizontal;
    private float _speed = 6.5f;
    private float _jumpPower = 5f;
    private bool _isFacingRight = true;

    public bool _isOnPlatform;
    public Rigidbody2D _platformRigidbody;

    public ManageParticles _manageParticles;

    private void FixedUpdate()
    {
        if (_isOnPlatform)
        {
            _rigidbody.velocity = new Vector2(_horizontal * _speed + _platformRigidbody.velocity.x, _rigidbody.velocity.y);
        }
        else
        {
            _rigidbody.velocity = new Vector2(_horizontal * _speed, _rigidbody.velocity.y);
        }
    }

    private void Update()
    {
        _rigidbody.velocity = new Vector2(_horizontal * _speed, _rigidbody.velocity.y);

        if (!_isFacingRight && _horizontal > 0f)
        {
            Flip();
        }
        else if (_isFacingRight && _horizontal < 0f)
        {
            Flip();
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpPower);
        }

        if (context.canceled && _rigidbody.velocity.y > 0f)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * 0.5f);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _groundLayer);
    }

    private void Flip()
    {
        if (IsGrounded())
        {
            _manageParticles.PlayTouchParticle();
        }

        _isFacingRight = !_isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void Move(InputAction.CallbackContext context)
    {
        _horizontal = context.ReadValue<Vector2>().x;
    }
}
