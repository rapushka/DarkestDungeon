using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();  
    }

    public int Health
    {
        set => _slider.value = value;
    }

    public int MaxHealth
    {
        set => _slider.maxValue = value;
    }
}
