using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] private float _speed;
    [SerializeField] private Transform[] _points;

    private int _currentPoint;

    private void Awake()
    {
        _points = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
        {
            _points[i] = _path.GetChild(i);
        }
    }

    private void Update()
    {
        Transform target = _points[_currentPoint];
        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

        if (transform.position == target.position)
        {
            _currentPoint = (_currentPoint + 1) % _points.Length;
        }
    }
}