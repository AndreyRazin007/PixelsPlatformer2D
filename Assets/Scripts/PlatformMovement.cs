using System.Collections;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float _speed;
    private Vector3 _targetPosition;

    private PlayerMovement _playerMovement;
    private Rigidbody2D _rigidbody;
    private Vector3 _moveDirection;

    private Rigidbody2D _playerRigidbody;

    public GameObject _ways;
    public Transform[] _wayPoints;
    private int _pointIndex;
    private int _pointCount;
    private int _directon = 1;

    public float _waitDuration;

    private void Awake()
    {
        _playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

        _wayPoints = new Transform[_ways.transform.childCount];

        for (int i = 0; i < _ways.gameObject.transform.childCount; ++i)
        {
            _wayPoints[i] = _ways.transform.GetChild(i).gameObject.transform;
        }
    }

    private void Start()
    {
        _pointIndex = 1;
        _pointCount = _wayPoints.Length;
        _targetPosition = _wayPoints[1].transform.position;

        DirectionCalculate();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _moveDirection * _speed;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, _targetPosition) < 0.1f)
        {
            NextPoint();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            _playerMovement._isOnPlatform = true;
            _playerMovement._platformRigidbody = _rigidbody;
            _playerRigidbody.gravityScale = _playerRigidbody.gravityScale * 15;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            _playerMovement._isOnPlatform = false;
            _playerRigidbody.gravityScale = _playerRigidbody.gravityScale / 15;
        }
    }

    private void NextPoint()
    {
        transform.position = _targetPosition;
        _moveDirection = Vector3.zero;

        if (_pointIndex == _pointCount - 1)
        {
            _directon = -1;
        }

        if (_pointIndex == 0)
        {
            _directon = 1;
        }

        _pointIndex += _directon;
        _targetPosition = _wayPoints[_pointIndex].transform.position;
        StartCoroutine(WaitNextPoint());
    }

    IEnumerator WaitNextPoint()
    {
        yield return new WaitForSeconds(_waitDuration);
        DirectionCalculate();
    }

    private void DirectionCalculate()
    {
        _moveDirection = (_targetPosition - transform.position).normalized;
    }
}
