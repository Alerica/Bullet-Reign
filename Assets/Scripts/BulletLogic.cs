using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject hitEffect;
    private int damage = PlayerStats.Instance.damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }

        GameObject tempEffect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(tempEffect, 1f);
        Destroy(gameObject);
    }
}
