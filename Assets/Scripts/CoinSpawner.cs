using System.Collections;
using System.Linq;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private bool _canSpawn = true;
    [SerializeField] private float _spawnDelay = 10f;

    private CoinSpawnPoint[] _spawnPoints;
    private WaitForSeconds _delay;

    private void Start()
    {
        _spawnPoints = GetComponentsInChildren<CoinSpawnPoint>();
        _delay = new WaitForSeconds(_spawnDelay);
        StartCoroutine(SpawnCoins());
    }

    private IEnumerator SpawnCoins()
    {
        while (_canSpawn)
        {
            SpawnAtRandomPoint();
            yield return _delay;
        }
    }

    private void SpawnAtRandomPoint()
    {
        var availablePoints = _spawnPoints.Where(x => x.CanSpawn);
        if (availablePoints.Any() == false)
            return;
        
        var index = Random.Range(0, availablePoints.Count());
        availablePoints.ElementAt(index).Spawn();
        Debug.Log("Spawned");
    }
}
