using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform gun;

    [Header("Attributes")]
    [SerializeField] private float playerSpeed = 5f;

    Vector2 movement = Vector2.zero;
    Vector2 mousePosition = Vector2.zero;
    
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        Debug.Log("X: " + movement.x + " Y: " + movement.y);

        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + movement * playerSpeed * Time.fixedDeltaTime);  

        Vector2 lookDirection = mousePosition - rb2d.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        gun.rotation = Quaternion.Euler(0, 0, angle);
    }
}
