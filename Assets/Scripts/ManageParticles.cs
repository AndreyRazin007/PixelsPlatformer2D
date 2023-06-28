using UnityEngine;

public class ManageParticles : MonoBehaviour
{
    [Header("MovementParticle")]
    [SerializeField] private ParticleSystem _movementParticle;

    [Header("")]
    [SerializeField] private ParticleSystem _fallParticle;
    [SerializeField] private ParticleSystem _touchParticle;

    [Range(0, 10)]
    [SerializeField] private int _occurAfterVelocity;

    [Range(0, 0.2f)]
    [SerializeField] private float _dustFormationPeriod;

    [SerializeField] private Rigidbody2D _playerRigidbody;

    private float _counter;
    private bool _isOnGround;

    private void Update()
    {
        _counter += Time.deltaTime;

        if (_isOnGround && Mathf.Abs(_playerRigidbody.velocity.x) > _occurAfterVelocity)
        {
            if (_counter > _dustFormationPeriod)
            {
                _movementParticle.Play();
                _counter = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Ground"))
        {
            _fallParticle.Play();
            _isOnGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Ground"))
        {
            _isOnGround = false;
        }
    }
    public void PlayTouchParticle()
    {
        _touchParticle.Play();
    }
}
