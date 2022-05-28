using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SwordAttack : MonoBehaviour
{
    [SerializeField] private int _damage = 3;
    [SerializeField] private float _xOffset = 1f;

    private Collider2D _swordCollider;
    private Vector3 _initialPosition;
    private Vector3 _leftSideOfsetted;
    private Vector3 _rightSideOfsetted;
    
    private void Start()
    {
        _swordCollider = GetComponent<Collider2D>();

        _initialPosition = transform.localPosition;
        _leftSideOfsetted = Offset(_xOffset * -1);
        _rightSideOfsetted = Offset(_xOffset);
    }

    public event Action<int> EnemyKilled;
    
    public void StartAttackRight()
    {
        _swordCollider.enabled = true;
        transform.localPosition = _rightSideOfsetted;
    }

    public void StartAttackLeft()
    {
        _swordCollider.enabled = true;
        transform.localPosition = _leftSideOfsetted;
    }

    public void StopAttack()
    {
        _swordCollider.enabled = false;
        transform.localPosition = _initialPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            int reward = enemy.TakeDamage(_damage);
            if (reward > 0)
            {
                EnemyKilled?.Invoke(reward);
            }
        }
    }

    private Vector3 Offset(float offset)
    {
        Vector3 temp = _initialPosition;
        temp.x += offset;
        return temp;
    }
}
