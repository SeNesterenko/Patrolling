using UnityEngine;

public class PatrolBehaviour : MonoBehaviour
{
    [SerializeField] private Transform [] _destinations;
    [SerializeField] private float _speed;
    [SerializeField] private float _detention;


    private float _currentTime;
    
    private CircularBuffer<Transform> _circleDestinations;
    
    private bool _isWaiting;
    private float _currentTimeDetention;
    private float _currentDetention;

    private void Start()
    {
        _circleDestinations = new CircularBuffer<Transform>(_destinations.Length);
        foreach (var destination in _destinations)
        {
            _circleDestinations.Add(destination);
        }
    }

    private void Update()
    {
        if (_isWaiting)
        {
            _currentDetention += Time.deltaTime;

            if (!(_currentDetention >= _detention)) return;
            
            _currentDetention = 0f;
            _isWaiting = !_isWaiting;
            return;
        }
        
        _currentTime += Time.deltaTime;

        var progress = _currentTime / _speed;
        var newPosition = Vector3.Lerp(_circleDestinations[0].position, _circleDestinations[1].position, progress);
        transform.position = newPosition;

        if (!(_currentTime >= _speed)) return;
        _currentTime = 0f;
        _circleDestinations.Add(_circleDestinations[0]);
        _isWaiting = !_isWaiting;
    }
}
