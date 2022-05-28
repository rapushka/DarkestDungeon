using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }

    public void LockMovement()
    {
        _playerController.enabled = false;
    }

    public void UnLockMovement()
    {
        _playerController.enabled = true;
    }
}
