using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PoisonLogic : MonoBehaviour
{
    public float duration = 10f;
    public int poisonDamage = 3;
    public float tickRate = 1f; // Damage every second
    public float radius = 3f; // Size of poison area

    private List<EnemyAI> enemiesInRange = new List<EnemyAI>();

    private void Start()
    {
        StartCoroutine(PoisonEffect());
        Destroy(gameObject, duration); // Destroy the poison area after duration ends
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) 
        {
            EnemyAI enemy = other.GetComponent<EnemyAI>();
            if (enemy != null && !enemiesInRange.Contains(enemy))
            {
                enemiesInRange.Add(enemy);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) 
        {
            EnemyAI enemy = other.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemiesInRange.Remove(enemy);
            }
        }
    }

    private IEnumerator PoisonEffect()
    {
        while (duration > 0)
        {
            for (int i = enemiesInRange.Count - 1; i >= 0; i--)
            {
                if (enemiesInRange[i] != null)
                {
                    enemiesInRange[i].TakeDamage(poisonDamage);
                }
                else
                {
                    enemiesInRange.RemoveAt(i);
                }
            }

            yield return new WaitForSeconds(tickRate);
            duration -= tickRate;
        }
    }

    public void SetDuration(float newDuration)
    {
        duration = newDuration;
    }
}