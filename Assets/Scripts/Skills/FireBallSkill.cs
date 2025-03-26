using UnityEngine;

[CreateAssetMenu(fileName = "FireballSkill", menuName = "Skills/Fireball")]
public class FireballSkill : Skill
{
    public GameObject fireballPrefab;
    private float force = 20f;
    public int staminaCost = 20;

    public override void Activate(PlayerMovement player)
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();

        if (!CanUse() || stats == null || !stats.HasEnoughStamina(staminaCost)) 
            return; 

        stats.UseStamina(staminaCost);

        // Spawn fireball
        GameObject fireball = Instantiate(fireballPrefab, player.transform.position, Quaternion.identity);
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        Vector2 direction = (player.mousePosition - (Vector2)player.transform.position).normalized;
        rb.linearVelocity = direction * force;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        fireball.transform.rotation = Quaternion.Euler(0, 0, angle);

        StartCooldown(player);
    }
}

