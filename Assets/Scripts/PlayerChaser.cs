using UnityEngine;

public class PlayerChaser : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _movingSpeed;
    [SerializeField] private Vector2 _minCameraPosition;
    [SerializeField] private Vector2 _maxCameraPosition;
    
    private Transform _playerTransform;
    private Vector3 _target;

    private void Awake()
    {
        _playerTransform = _player.transform;

        _target = new Vector3()
        {
            x = _playerTransform.position.x,
            y = _playerTransform.position.y,
            z = _playerTransform.position.z - 10,
        };
        transform.position = _target;
    }

    private void Update()
    {
        _target = GetBoundedCameraPosition();

        Vector3 position = Vector3.Lerp
        (
            transform.position,
            _target,
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

        Vector3 target = Vector3.zero;
        target.x = Mathf.Max(Mathf.Min(x, maxX), minX);
        target.y = Mathf.Max(Mathf.Min(y, maxY), minY);
        target.z = -10;
        return target;
    }
}
