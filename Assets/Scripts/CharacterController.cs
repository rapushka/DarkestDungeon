using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Sprite[] _sprite;

    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 10;
    }


    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 position = transform.position;

        position.x = position.x + _speed * horizontal * Time.deltaTime;
        position.y = position.y + _speed * vertical * Time.deltaTime;

        if (horizontal < 0)
            spriteRenderer.sprite = _sprite[0];
        else if (horizontal > 0)
            spriteRenderer.sprite = _sprite[3];
        else if (vertical > 0)
            spriteRenderer.sprite = _sprite[2];
        else if (vertical < 0)
            spriteRenderer.sprite = _sprite[1];

        transform.position = position;
    }
}
