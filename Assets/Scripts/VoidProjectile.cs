using UnityEngine;

public class VoidProjectile : MonoBehaviour
{
    public int damage = 10;
    public float slowEffect = 0.5f; 
    public float slowDuration = 3f;
    public float lifetime = 5f; 

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) 
        {
            EnemyAI enemy = other.GetComponent<EnemyAI>();
            if (enemy != null) 
            {
                enemy.TakeDamage(damage);
                enemy.ApplySlow(slowEffect, slowDuration);
            }
        }
    }
}
