using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SwordAttack : MonoBehaviour
{
    [SerializeField] private int _damage = 3;
    [SerializeField] private float _xOffset = 1f;
    [Space]
    [SerializeField] private AudioSource _waveSound;

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

    public void MultiplyDamage(float multiplyer)
    {
        _damage = Mathf.RoundToInt(_damage * multiplyer);
    }

    public void StartAttackRight()
    {
        Attack(_rightSideOfsetted);
    }

    public void StartAttackLeft()
    {
        Attack(_leftSideOfsetted);
    }

    private void Attack(Vector3 vector)
    {
        _waveSound.Play();
        _swordCollider.enabled = true;
        transform.localPosition = vector;
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
