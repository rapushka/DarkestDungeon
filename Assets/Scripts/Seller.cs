using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Seller : MonoBehaviour
{
    [SerializeField] private GameObject _dialogWindow;
    [SerializeField] private Player _player;
    [SerializeField] private int _price;
    [SerializeField] private float _multiplier;

    private TextMeshProUGUI _dialogMessage;
    private bool _isNear;

    private void Start()
    {
        _dialogMessage = GetComponentInChildren<TextMeshProUGUI>();
        _dialogWindow.SetActive(false);

        _player.PlayerController.Actions.Player
            .Interact.performed += OnInteract;
    }

    private void OnDestroy()
    {
        _player.PlayerController.Actions.Player
            .Interact.performed -= OnInteract;
    }

    private void OnInteract(InputAction.CallbackContext obj)
    {
        if (_isNear == false)
        {
            return;
        }

        if (_player.TryUpgrade(_multiplier, _price) == false)
        {
            return;
        }

        _price *= 2;
        _dialogMessage.text = "Поздравляю с покупкой!";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player _))
        {
            _isNear = true;
            _dialogWindow.SetActive(true);

            _dialogMessage.text = _player.Coins < _price
                ? $"Ничего не могу предложить, вот когда принесёшь {_price} монет - тогда и поговорим"
                : $"Если ты дашь мне {_price} монет - я улучшу твой меч! (E)";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player _))
        {
            _dialogWindow.SetActive(false);
            _isNear = false;
        }
    }
}
