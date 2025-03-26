using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    Animator animator;
    [SerializeField] private Skill[] possibleSkills;
    [SerializeField] private GameObject skillDropPrefab;
    [SerializeField] private GameObject coinPrefab;
    private SpriteRenderer spriteRenderer;
    [Header("Enemy Stats")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private int maxHealth = 60;
    [SerializeField] private float skillDropChance = 0.1f;
    [SerializeField] private float coinDropChance = 0.8f;
    private Color originalColor;
    private float originalSpeed;
    private int currentHealth;
    private Transform player;
    private Room currentRoom;



    private void Start()
    {
        currentHealth = maxHealth;
        originalSpeed = moveSpeed;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        originalColor = spriteRenderer.color;
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

        if (direction.x < 0)
            transform.localScale = new Vector3(-6, 6, 6); 
        else if (direction.x > 0)
            transform.localScale = new Vector3(6, 6, 6);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();

            // Chance to drop Item
            if (Random.value < skillDropChance)
            {
                Skill droppedSkill = possibleSkills[Random.Range(0, possibleSkills.Length)];
                GameObject skillDrop = Instantiate(skillDropPrefab, transform.position, Quaternion.identity);
                skillDrop.GetComponent<SkillPickup>().skill = droppedSkill;
            }

            // Chance to drop Coin
            if (Random.value < coinDropChance) // Make sure you define coinDropChance
            {
                GameObject coinDrop = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            }
        } 
        animator.SetTrigger(StringManager.isHit);
    }

    public void ApplySlow(float slowFactor, float duration)
    {
        moveSpeed = originalSpeed * slowFactor;
        StopAllCoroutines(); 
        StartCoroutine(RemoveSlowAfterDelay(duration));
    }

    private IEnumerator RemoveSlowAfterDelay(float duration)
    {
        spriteRenderer.color = Color.blue;
        yield return new WaitForSeconds(duration);
        moveSpeed = originalSpeed; 
        spriteRenderer.color = originalColor;
    }

    private void Die()
    {
        if (currentRoom != null)
        {
            currentRoom.EnemyDefeated(gameObject);
        }
        animator.SetTrigger("isDie"); 
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 0.25f);
    }

    public void SetRoom(Room room)
    {
        currentRoom = room;
    }
}

