using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;

    [Header("Attributes")]
    [SerializeField] private float bulletForce = 20f;

    void Update()
    {
        if(Input.GetButtonDown("Fire1")) 
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position,  firePoint.rotation);
        Rigidbody2D rb2d = bullet.GetComponent<Rigidbody2D>();

        Vector2 forceDirection = Quaternion.Euler(0, 0, -90f) * firePoint.up;
        rb2d.AddForce(forceDirection * bulletForce, ForceMode2D.Impulse);
    }

}
