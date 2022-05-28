using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class Teleporter : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Player _player;
    [SerializeField] private Transform _spawnPlayerPosition;
    [SerializeField] private float _fadingDuration = 1f;
    [SerializeField] private Image _fade;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player _) == false)
        {
            return;
        }

        StartCoroutine(Fading());
    }

    private IEnumerator Fading()
    {
        _player.LockMovement();
        yield return FadeIn();

        Teleport();
        
        yield return FadeOut();
        _player.UnLockMovement();
    }

    private IEnumerator FadeOut()
    {
        yield return EnumeratorForAlpha(1f, (i) => i >= 0f, -0.01f);
    }

    private IEnumerator FadeIn()
    {
        yield return EnumeratorForAlpha(0f, (i) => i < 1f, 0.01f);
    }

    private IEnumerator EnumeratorForAlpha
        (float startValue, Predicate<float> endPredicate, float step)
    {
        float delay = _fadingDuration / 100;
        for (float i = startValue; endPredicate(i); i += step)
        {
            Color color = _fade.color;
            color.a = i;
            _fade.color = color;
            yield return new WaitForSeconds(delay);
        }
    }

    private void Teleport()
    {
        if (_camera.TryGetComponent(out PlayerChaser playerChaser))
        {
            playerChaser.enabled = false;
        }

        Vector3 position = _spawnPlayerPosition.position;
        _player.transform.position = position;
        position.z -= 10;
        _camera.transform.position = position;
    }
}
