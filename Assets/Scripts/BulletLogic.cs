using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject hitEffect;
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject tempEffect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(tempEffect, 1f);
        Destroy(gameObject);
    }
}
