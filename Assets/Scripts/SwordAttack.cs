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

    public void StartAttackRight()
    {
        transform.localPosition = _rightAttackOffset;
        _swordCollider.enabled = true;
    }

    public void StartAttackLeft()
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
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_damage);
        }
    }
}
