using System;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBar;

    private int _health = 100;
    private PlayerController _playerController;
    private Animator _animator;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _animator = GetComponent<Animator>();

        _healthBar.MaxHealth = _health;
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
        _health -= damage;
        _healthBar.Health = _health;
        if (_health <= 0)
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
