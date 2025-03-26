using UnityEngine;

[CreateAssetMenu(fileName = "VoidSkill", menuName = "Skills/Void")]
public class VoidSkill : Skill
{
    public GameObject voidProjectilePrefab;
    private float force = 25f; 
    public int staminaCost = 30;

    public override void Activate(PlayerMovement player)
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();

        if (!CanUse() || stats == null || !stats.HasEnoughStamina(staminaCost)) 
            return;

        stats.UseStamina(staminaCost);

        // Spawn void projectile
        GameObject voidProjectile = Instantiate(voidProjectilePrefab, player.transform.position, Quaternion.identity);
        Rigidbody2D rb = voidProjectile.GetComponent<Rigidbody2D>();
        Vector2 direction = (player.mousePosition - (Vector2)player.transform.position).normalized;
        rb.linearVelocity = direction * force;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        voidProjectile.transform.rotation = Quaternion.Euler(0, 0, angle);

        StartCooldown(player);
    }
}
