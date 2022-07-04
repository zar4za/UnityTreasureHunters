using UnityEngine;

public class CoinSpawnPoint : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;

    public bool CanSpawn { get; private set; } = true;

    public void Spawn()
    {
        CanSpawn = false;
        Instantiate(_coinPrefab, transform.position, transform.rotation).Collected.AddListener(OnCoinCollected);
    }

    private void OnCoinCollected() => CanSpawn = true;
}
