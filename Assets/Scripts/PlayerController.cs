using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _atackDuration = 0.5f;
    [SerializeField] private SwordAttack _swordAttack;

    private PlayerInputActions _input;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private bool _isCanMove = true;

    private Vector2 _direction;

    private void Awake()
    {
        _input = new PlayerInputActions();

        _input.Player.Attack
            .performed += OnAttack;
    }

    private void OnEnable()
    {
        _input.Enable();
    }
    private void OnDisable()
    {
        _input.Disable();
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (_isCanMove == false)
        {
            return;
        }

        _direction = _input.Player.Move.ReadValue<Vector2>();

        Move();

        if (_direction.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if (_direction.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
    }

    private void Move()
    {
        if (_direction == Vector2.zero)
        {
            _animator.SetBool("isMoving", false);
            return;
        }

        Vector2 scaledDirection = _moveSpeed * _direction;
        _rigidbody.MovePosition(_rigidbody.position 
            + scaledDirection * Time.fixedDeltaTime);

        _animator.SetBool("isMoving", true);
    }

    private void OnAttack(InputAction.CallbackContext obj)
    {
        _isCanMove = false;
        _animator.SetTrigger("SwordAttack");

        if (_spriteRenderer.flipX)
        {
            _swordAttack.StartAttackLeft();
        }
        else
        {
            _swordAttack.StartAttackRight();
        }
        Invoke(nameof(EndSwordAttack), _atackDuration);
    }

    private void EndSwordAttack()
    {
        _swordAttack.StopAttack();
        _isCanMove = true;
    }
}
