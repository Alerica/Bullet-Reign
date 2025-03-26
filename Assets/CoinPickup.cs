using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().AddGold(5);
            Destroy(gameObject);
        }
    }
}
