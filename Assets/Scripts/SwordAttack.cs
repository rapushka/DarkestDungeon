using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    [SerializeField] private Collider2D _swordCollider;
    [SerializeField] private int _damage = 3;

    private Vector2 _rightAttackOffset;
    private Vector2 _leftAttackOffset;

    private void Start()
    {
        _rightAttackOffset = transform.localPosition;

        _leftAttackOffset = _rightAttackOffset;
        _leftAttackOffset.x *= -1;
    }

    public void AttackRight()
    {
        transform.localPosition = _rightAttackOffset;
        _swordCollider.enabled = true;
    }

    public void AttackLeft()
    {
        transform.localPosition = _leftAttackOffset;
        _swordCollider.enabled = true;
    }

    public void StopAttack()
    {
        _swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"OnTriggerEnter2D {other.name}");
        if (other.TryGetComponent(out Enemy enemy))
        {
            Debug.Log(">> TryGetComponent Enemy");
            enemy.TakeDamage(_damage);
        }
    }
}
