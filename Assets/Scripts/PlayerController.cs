using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _collisionOffset = 0.05f;
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

        PlayMovingAnimation();

        if (_direction.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if (_direction.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
    }

    private void PlayMovingAnimation()
    {
        if (_direction == Vector2.zero)
        {
            _animator.SetBool("isMoving", false);
            return;
        }

        bool isMoving = TryMove(_direction);
        _animator.SetBool("isMoving", isMoving);
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            return false;
        }

        _rigidbody.MovePosition
        (
            _rigidbody.position
            + _moveSpeed
            * Time.fixedDeltaTime
            * direction
        );

        return true;
    }

    private void OnAttack(InputAction.CallbackContext obj)
    {
        _animator.SetTrigger("SwordAttack");
    }

    public void SwordAttack()
    {
        LockMovement();

        if (_spriteRenderer.flipX)
        {
            _swordAttack.AttackLeft();
        }
        else
        {
            _swordAttack.AttackRight();
        }
    }

    public void EndSwordAttack()
    {
        UnlockMovement();
        _swordAttack.StopAttack();
    }

    public void LockMovement()
    {
        _isCanMove = false;
    }

    public void UnlockMovement()
    {
        _isCanMove = true;
    }
}
