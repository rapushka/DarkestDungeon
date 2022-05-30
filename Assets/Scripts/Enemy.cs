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
    [SerializeField] private int _reward = 1;
    [Space]
    [SerializeField] private AudioSource _enemyHurtSound;

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

    public int TakeDamage(int damage)
    {
        _health -= damage;
        _healthBar.Health = _health;
        _enemyHurtSound.Play();

        if (_health <= 0)
        {
            Defeated();
            return _reward;
        }
        return 0;
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
