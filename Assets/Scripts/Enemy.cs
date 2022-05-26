using UnityEngine;

[RequireComponent(typeof(Animation))]
public class Enemy : MonoBehaviour
{
    private Animator _animator;
    private float _health = 1;

    private void Start()
    {
        _animator = GetComponent<Animator>();
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
        _animator.SetTrigger("Defeated");
    }

    private void RemoveEnemy()
    {
        Destroy(gameObject);
    }
}
