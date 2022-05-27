using UnityEngine;

[RequireComponent(typeof(Animator))]
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
        Debug.Log("Damage Taked");

        if (_health <= 0)
        {
            Defeated();
        }
    }

    private void Defeated()
    {
        _animator.SetTrigger("Death");
        Destroy(gameObject);
    }
}
