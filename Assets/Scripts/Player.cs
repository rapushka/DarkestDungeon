using System;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _healt = 100;
    private PlayerController _playerController;
    private Animator _animator;
    private Collider2D _collider;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
    }

    public void LockMovement()
    {
        _playerController.enabled = false;
    }

    public void UnLockMovement()
    {
        _playerController.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            LockMovement();

            enemy.Attack();
            TakeDamage(enemy.Damage);
        }
    }

    private void TakeDamage(int damage)
    {
        _healt -= damage;
        if (_healt <= 0)
        {
            Defeated();
            return;
        }
        UnLockMovement();
    }

    private void Defeated()
    {
        LockMovement();
        _animator.SetTrigger("Death");
        Debug.Log("Game Over");
    }
}
