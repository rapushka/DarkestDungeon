using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CircleCollider2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health = 1;
    [SerializeField] private int _damage = 10;
    [SerializeField] private int _atackRadius = 3;
    [SerializeField] private float _atackduration = 0.1f;
    [Space]
    [SerializeField] private HealthBar _healthBar;

    private Animator _animator;
    private CircleCollider2D _colider;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _colider = GetComponent<CircleCollider2D>();

        _healthBar.MaxHealth = _health;
    }

    public int Damage => _damage;

    public void Attack()
    {
        _colider.radius *= _atackRadius;
        Invoke(nameof(BackNormalRadius), _atackduration);
    }

    private void BackNormalRadius()
    {
        _colider.radius /= _atackRadius;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        _healthBar.Health = _health;

        if (_health <= 0)
        {
            Defeated();
        }
    }

    private void Defeated()
    {
        _animator.SetTrigger("Death");
        _colider.enabled = false;
        Invoke(nameof(Destroy), 0.75f);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
