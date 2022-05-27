using UnityEngine;

public class PlayerChaser : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _movingSpeed;
    [SerializeField] private Vector2 _minCameraPosition;
    [SerializeField] private Vector2 _maxCameraPosition;
    
    private void OnValidate()
    {
        if (_playerTransform.TryGetComponent(out Player _))
        {
            return;
        }
        string playerName = _playerTransform.name;
        _playerTransform = null;
        throw new System.Exception($"{playerName} "
            + $"need contain a {nameof(Player)} Component");
    }

    private void Awake()
    {
        Vector3 temp = _playerTransform.position;
        temp.z -= 10;
        transform.position = temp;
    }

    private void Update()
    {
        Vector3 target = GetBoundedCameraPosition();

        Vector3 position = Vector3.Lerp
        (
            transform.position,
            target,
            _movingSpeed * Time.deltaTime
        );

        transform.position = position;
    }

    private Vector3 GetBoundedCameraPosition()
    {
        float x = _playerTransform.position.x;
        float y = _playerTransform.position.y;

        float minX = _minCameraPosition.x;
        float minY = _minCameraPosition.y;
        float maxX = _maxCameraPosition.x;
        float maxY = _maxCameraPosition.y;

        return new()
        {
            x = Bounded(x, minX, maxX),
            y = Bounded(y, minY, maxY),
            z = -10
        };
    }

    private float Bounded(float number, float min, float max)
    {
        return Mathf.Max(Mathf.Min(number, max), min);
    }
}
