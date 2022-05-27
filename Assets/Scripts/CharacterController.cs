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

        _spriteRenderer.sprite = GetSpriteByDirection(direction);
    }

    private Sprite GetSpriteByDirection(Vector2 direction)
    {
        return direction == Vector2.left ? _sprite[0]
            : direction == Vector2.right ? _sprite[3]
            : direction == Vector2.up ? _sprite[2] 
            : direction == Vector2.down ? _sprite[1] 
            : throw new System.Exception();
    }
}
