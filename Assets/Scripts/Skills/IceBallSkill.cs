using UnityEngine;

[CreateAssetMenu(fileName = "IceballSkill", menuName = "Skills/Iceball")]
public class IceBallSkill : Skill
{
    public GameObject iceBallPrefab;
    private float force = 20f;
    public int staminaCost = 20;

    public override void Activate(PlayerMovement player)
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();

        if (!CanUse() || stats == null || !stats.HasEnoughStamina(staminaCost)) 
            return; 

        stats.UseStamina(staminaCost);

        // Spawn iceBall
        GameObject iceBall = Instantiate(iceBallPrefab, player.transform.position, Quaternion.identity);
        Rigidbody2D rb = iceBall.GetComponent<Rigidbody2D>();
        Vector2 direction = (player.mousePosition - (Vector2)player.transform.position).normalized;
        rb.linearVelocity = direction * force;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        iceBall.transform.rotation = Quaternion.Euler(0, 0, angle);

        StartCooldown(player);
    }
}
