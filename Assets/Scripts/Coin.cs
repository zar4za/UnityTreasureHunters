using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    public UnityEvent Collected { get; } = new UnityEvent();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            Collected.Invoke();
            Collected.RemoveAllListeners();
            Destroy(gameObject);
        }
    }
}
