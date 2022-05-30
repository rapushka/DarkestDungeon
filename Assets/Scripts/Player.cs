using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private TextMeshProUGUI _coinsHolder;
    [Space]
    [SerializeField] private AudioSource _hurtPlayer;
    [SerializeField] private AudioSource _coinPickup;

    private int _health = 100;
    private int _coinsAmount;
    private PlayerController _playerController;
    private Animator _animator;
    private SwordAttack _swordAttack;

    public PlayerController PlayerController => _playerController;
    public int Coins => _coinsAmount;

    private void Awake()
    {
        _swordAttack = GetComponentInChildren<SwordAttack>();
        _playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _healthBar.MaxHealth = _health;
    }

    private void OnEnable()
    {
        _swordAttack.EnemyKilled += OnEnemyKilled;
    }

    private void OnDisable()
    {
        _swordAttack.EnemyKilled -= OnEnemyKilled;
    }

    public void LockMovement()
    {
        _playerController.enabled = false;
    }

    public void UnLockMovement()
    {
        _playerController.enabled = true;
    }

    public bool TryUpgrade(float damageMultiplier, int price)
    {
        if (_coinsAmount < price)
        {
            return false;
        }

        _coinsAmount -= price;
        _swordAttack.MultiplyDamage(damageMultiplier);

        _coinsHolder.text = _coinsAmount.ToString();
        return true;
    }

    private void OnEnemyKilled(int reward)
    {
        _coinPickup.Play();
        _coinsAmount += reward;
        _coinsHolder.text = _coinsAmount.ToString();
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
        _hurtPlayer.Play();

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

        Invoke(nameof(GoToMainMenu), 1f);
    }

    private void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
