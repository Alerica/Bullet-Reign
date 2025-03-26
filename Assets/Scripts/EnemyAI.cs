using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    Animator animator;
    [Header("Enemy Stats")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private int maxHealth = 60;
    
    private int currentHealth;
    private Transform player;
    private Room currentRoom;

    private void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (player != null)
        {
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        } 
        animator.SetTrigger("isHit");
    }

    private void Die()
    {
        if (currentRoom != null)
        {
            currentRoom.EnemyDefeated(gameObject);
        }
        Destroy(gameObject);
    }

    public void SetRoom(Room room)
    {
        currentRoom = room;
    }
}

