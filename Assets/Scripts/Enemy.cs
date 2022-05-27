using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health = 1;

    private Animator _animator;
    private Collider2D _colider;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _colider = GetComponent<Collider2D>();
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

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
