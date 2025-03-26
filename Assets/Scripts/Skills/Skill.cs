using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Scriptable Objects/Skill")]
public abstract class Skill : ScriptableObject
{
    [Header("Attributes")]
    public string skillName;
    public Sprite icon;
    public float cooldown;
    protected bool isOnCooldown = false;
    public abstract void Activate(PlayerMovement player);
    public bool CanUse() => !isOnCooldown;

    public void StartCooldown(MonoBehaviour host)
    {
        isOnCooldown = true;
        host.StartCoroutine(CooldownRoutine());
    }

    private System.Collections.IEnumerator CooldownRoutine()
    {
        yield return new WaitForSeconds(cooldown);
        isOnCooldown = false;
    }
}