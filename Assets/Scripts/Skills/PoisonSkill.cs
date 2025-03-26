using UnityEngine;

[CreateAssetMenu(fileName = "PoisonSkill", menuName = "Skills/Poison")]
public class PoisonSkill : Skill
{
    public GameObject poisonAreaPrefab;
    public int staminaCost = 25;
    public float poisonDuration = 10f;

    public override void Activate(PlayerMovement player)
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();

        if (!CanUse() || stats == null || !stats.HasEnoughStamina(staminaCost)) 
            return; 

        stats.UseStamina(staminaCost);

        // Spawn poison field at player's position
        GameObject poisonArea = Instantiate(poisonAreaPrefab, player.transform.position, Quaternion.identity);
        PoisonLogic poisonScript = poisonArea.GetComponent<PoisonLogic>();
        if (poisonScript != null)
        {
            poisonScript.SetDuration(poisonDuration);
        }

        StartCooldown(player);
    }
}
