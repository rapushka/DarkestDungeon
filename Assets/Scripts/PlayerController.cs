using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public SwordAttack swordAttack;
    private Vector2 movementInput;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    private bool canMove = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (canMove == false)
        {
            return;
        }

        PlayMovingAnimation();

        if (movementInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (movementInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void PlayMovingAnimation()
    {
        if (movementInput == Vector2.zero)
        {
            animator.SetBool("isMoving", false);
            return;
        }

        bool success = TryMove(movementInput);

        if (success == false)
        {
            success = TryMove(new Vector2(movementInput.x, 0));
        }

        if (success == false)
        {
            success = TryMove(new Vector2(0, movementInput.y));
        }

        animator.SetBool("isMoving", success);
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            return false;
        }

        int count = rb.Cast
        (
            direction,
            movementFilter,
            castCollisions,
            moveSpeed * Time.fixedDeltaTime + collisionOffset
        );

        if (count == 0)
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    private void OnFire()
    {
        animator.SetTrigger("swordAttack");
    }

    public void SwordAttack()
    {
        LockMovement();

        if (spriteRenderer.flipX == true)
        {
            swordAttack.AttackLeft();
        }
        else
        {
            swordAttack.AttackRight();
        }
    }

    public void EndSwordAttack()
    {
        UnlockMovement();
        swordAttack.StopAttack();
    }

    public void LockMovement()
    {
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }
}
