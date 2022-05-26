using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Collider2D SwordCollider;
    public int Damage = 3;

    private Vector2 _rightAttackOffset;
    private Vector2 _leftAttackOffset;

    private void Start()
    {
        _rightAttackOffset = transform.position;

        _leftAttackOffset = _rightAttackOffset;
        _leftAttackOffset.x *= -1;
    }

    public void AttackRight()
    {
        SwordCollider.enabled = true;
        transform.localPosition = _rightAttackOffset;
    }

    public void AttackLeft()
    {
        SwordCollider.enabled = true;
        transform.localPosition = _leftAttackOffset;
    }

    public void StopAttack()
    {
        SwordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(Damage);
        }
    }
}
