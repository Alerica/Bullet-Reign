using UnityEngine;

public class FlyingAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float attackRange = 5f; 
    [SerializeField] private float attackCooldown = 2f;

    [Header("Projectile Settings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint; 

    private Animator animator;
    private bool canAttack = true;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange && canAttack)
        {
            Attack();
        }
    }

    private void Attack()
    {
        canAttack = false;
        animator.SetTrigger(StringManager.attack);

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        EnemyProjectile projectileScript = projectile.GetComponent<EnemyProjectile>();
        if (projectileScript != null)
        {
            Vector2 direction = (player.position - firePoint.position).normalized;
            projectileScript.SetDirection(direction);
        }

        Invoke(nameof(ResetAttack), attackCooldown);
    }

    private void ResetAttack()
    {
        canAttack = true;
    }
}
