using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CharacterController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Sprite[] _sprite;

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Vector2 direction = Vector2.zero;
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");

        Vector2 position = transform.position;
        position += _speed * Time.deltaTime * direction;
        transform.position = position;

        SetSpriteByDirection(direction);
    }

    private void SetSpriteByDirection(Vector2 direction)
    {
        if (direction.x < 0)
        {
            _spriteRenderer.sprite = _sprite[0];
        }
        else if (direction.x > 0)
        {
            _spriteRenderer.sprite = _sprite[3];
        }
        else if (direction.y > 0)
        {
            _spriteRenderer.sprite = _sprite[2];
        }
        else if (direction.y < 0)
        {
            _spriteRenderer.sprite = _sprite[1];
        }
    }
}
