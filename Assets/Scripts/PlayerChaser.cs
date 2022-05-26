using UnityEngine;

public class PlayerChaser : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _movingSpeed;

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
        _target.x = _playerTransform.position.x;
        _target.y = _playerTransform.position.y;

        Vector3 position = Vector3.Lerp
        (
            transform.position,
            _target,
            _movingSpeed * Time.deltaTime
        );

        transform.position = position;
    }
}
