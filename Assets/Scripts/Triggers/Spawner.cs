using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private GameObject _template;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        foreach (Transform spawnPoint in _spawnPoints)
        {
            if (spawnPoint.childCount == 0)
            {
                Instantiate(_template, spawnPoint);
            }
        }
    }
}
