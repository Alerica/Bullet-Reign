using UnityEngine;

public class SlowBulletLogic : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private int damage = 10;
    [SerializeField] private float slowAmount = 0.5f;     
    [SerializeField] private float slowDuration = 2f; 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); 
                enemy.ApplySlow(slowAmount, slowDuration); 
            }
        }

        GameObject tempEffect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(tempEffect, 1f);
        Destroy(gameObject);
    }
}

