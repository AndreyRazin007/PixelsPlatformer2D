using System.Collections;
using UnityEngine;

public class MovingObstacles : MonoBehaviour
{
    [Range(0, 5)]
    public float _speed;

    [Range(0, 2)]
    public float _waitDuration;
    
    private Vector3 _targetPosition;

    public GameObject _ways;
    public Transform[] _wayPoints;

    private int _pointIndex;
    private int _pointCount;
    private int _direction = 1;
    private int _speedMultiplier = 1;

    private void Awake()
    {
        _wayPoints = new Transform[_ways.transform.childCount];

        for (int i = 0; i < _ways.gameObject.transform.childCount; ++i)
        {
            _wayPoints[i] = _ways.transform.GetChild(i).gameObject.transform;
        }
    }

    private void Start()
    {
        _pointCount = _wayPoints.Length;
        _pointIndex = 1;
        _targetPosition = _wayPoints[_pointIndex].transform.position;
    }

    private void Update()
    {
        var step = _speedMultiplier * _speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, step);

        if (transform.position == _targetPosition)
        {
            NextPoint();
        }
    }

    private void NextPoint()
    {
        if (_pointIndex == _pointCount - 1)
        {
            _direction = -1;
        }

        if (_pointIndex == 0)
        {
            _direction = 1;
        }

        _pointIndex += _direction;
        _targetPosition = _wayPoints[_pointIndex].transform.position;
        StartCoroutine(WaitNextPoint());
    }

    IEnumerator WaitNextPoint()
    {
        _speedMultiplier = 0;
        yield return new WaitForSeconds(_waitDuration);
        _speedMultiplier = 1;
    }
}
